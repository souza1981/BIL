
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BIL.Data;
using BIL.Logica.Manager.Interface;
using BIL.Logica.Manager;
using BIL.Data.Repository.Interface;
using BIL.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BIL.Data.Entidades;
using Microsoft.AspNetCore.Identity;
using BIL.Seguranca;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

namespace BIL_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            //var connection = @"Server=db;Database=bildb;User=sa;Password=P@assWord1054;";
            //var connection = @"Server=localhost,5434;Database=bildb;User=sa;Password=P@assWord1054;";
            //var connection = "Server=(localdb)\\mssqllocaldb;Database=BILDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            var redisConnection = Environment.GetEnvironmentVariable("REDIS_CONNECTION", EnvironmentVariableTarget.Process);


            var server = Environment.GetEnvironmentVariable("DB_SERVER", EnvironmentVariableTarget.Process);
            var port = Environment.GetEnvironmentVariable("DB_PORT", EnvironmentVariableTarget.Process);
            var user = Environment.GetEnvironmentVariable("DB_USER", EnvironmentVariableTarget.Process);
            var pass = Environment.GetEnvironmentVariable("DB_PASS", EnvironmentVariableTarget.Process);
            var database = Environment.GetEnvironmentVariable("DB_DATABASE", EnvironmentVariableTarget.Process);

            if (server == null)
            {
                server = Environment.GetEnvironmentVariable("DB_SERVER", EnvironmentVariableTarget.Machine);
                port = Environment.GetEnvironmentVariable("DB_PORT", EnvironmentVariableTarget.Machine);
                user = Environment.GetEnvironmentVariable("DB_USER", EnvironmentVariableTarget.Machine);
                pass = Environment.GetEnvironmentVariable("DB_PASS", EnvironmentVariableTarget.Machine);
                database = Environment.GetEnvironmentVariable("DB_DATABASE", EnvironmentVariableTarget.Machine);

            }

            var connection = "server=" + server + ";Port=" + port + ";User id=" + user + ";password=" + pass + ";database=" + database;

            /*Conexão virá do Heroku*/
            if (server == null && Environment.GetEnvironmentVariable("DATABASE_URL", EnvironmentVariableTarget.Process).Length > 0)
            {

                Uri dbUri = new Uri(Environment.GetEnvironmentVariable("DATABASE_URL", EnvironmentVariableTarget.Process));
                
                user = dbUri.UserInfo.Split(":")[0];
                pass = dbUri.UserInfo.Split(":")[1];
                server = dbUri.Host;
                port = dbUri.Port.ToString();
                database = dbUri.AbsolutePath.Substring(1);
                connection = "server=" + server + ";Port=" + port + ";User id=" + user + ";password=" + pass + ";database=" + database;

            }

            if (redisConnection == null && Environment.GetEnvironmentVariable("REDIS_URL", EnvironmentVariableTarget.Process).Length > 0)
            {

                Uri redisUri = new Uri(Environment.GetEnvironmentVariable("REDIS_URL", EnvironmentVariableTarget.Process));

                user = redisUri.UserInfo.Split(":")[0];
                pass = redisUri.UserInfo.Split(":")[1];
                server = redisUri.Host;
                port = redisUri.Port.ToString();
                redisConnection = server + ",port=" + port + ",password=" + pass;

            }

            //Contextos
            services.AddDbContext<BILContext>(options =>
                options.UseNpgsql(connection, b => b.MigrationsAssembly("BIL-API")));

            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseInMemoryDatabase("InMemoryDatabase"));


            //Cache do Redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisConnection;
                options.InstanceName = "BILAPI";
            });

            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Serviços
            services.AddScoped<ILivroManager, LivroManager>();
            services.AddScoped<IUsuarioManager, UsuarioManager>();
            
            
            //Serviços de segurança
            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(
                signingConfigurations, tokenConfigurations);

            services.AddCors();


            //Repositórios
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BIL API",
                    Version = "V1"
                });
            });

            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            ApplicationDbContext applicationDbContext, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Criação de estruturas, usuários e permissões
            // na base do ASP.NET Identity Core (caso ainda não
            // existam)
            new IdentityInitializer(applicationDbContext, userManager, roleManager)
                .Initialize();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","BILApi");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

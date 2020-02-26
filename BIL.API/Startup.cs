
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

            /*Conex�o vir� do Heroku*/
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

            services.AddDbContext<BILContext>(options =>
                options.UseNpgsql(connection, b => b.MigrationsAssembly("BIL-API")));

             
            //Servi�os
            services.AddScoped<ILivroManager, LivroManager>();
            services.AddScoped<IUsuarioManager, UsuarioManager>();

            //Reposit�rios
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

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


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BIL.Data;
using BIL.Logica.Manager.Interface;
using BIL.Logica.Manager;
using BIL.Data.Repository.Interface;
using BIL.Data.Repository;

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
            var connection = @"Server=db;Database=bildb;User=sa;Password=P@assWord1054;";
            //var connection = @"Server=localhost,5434;Database=bildb;User=sa;Password=P@assWord1054;";
            //var connection = "Server=(localdb)\\mssqllocaldb;Database=BILDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<BILContext>(options =>
                options.UseSqlServer(connection)); ;

            services.AddControllers();

            //Serviços
            services.AddScoped<ILivroManager, LivroManager>();
            services.AddScoped<IUsuarioManager, UsuarioManager>();

            //Repositórios
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

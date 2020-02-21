
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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


            var source = Environment.GetEnvironmentVariable("DB_CONNECTION", EnvironmentVariableTarget.Process);
            var user = Environment.GetEnvironmentVariable("DB_USER", EnvironmentVariableTarget.Process);
            var pass = Environment.GetEnvironmentVariable("DB_PASS", EnvironmentVariableTarget.Process);
             

            var connection = "Data Source=" + source + ";User Id=" + user + ";Password=" + pass + ";";
            services.AddDbContext<BILContext>(options =>
                options.UseOracle(connection, b => b.MigrationsAssembly("BIL-API")));
            //services.AddControllers();

             
            //Serviços
            services.AddScoped<ILivroManager, LivroManager>();
            services.AddScoped<IUsuarioManager, UsuarioManager>();

            //Repositórios
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*
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
            */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();



        }
    }
}

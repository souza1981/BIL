
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

            services.AddDbContext<BILContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BILDb;Trusted_Connection=True;MultipleActiveResultSets=true")); ;

            services.AddControllers();

            //Serviços
            services.AddScoped<ILivroManager, LivroManager>();

            //Repositórios
            services.AddScoped<ILivroRepository, LivroRepository>();
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

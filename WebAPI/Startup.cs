using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace WebAPI
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
            services.AddDbContext<AgrupamentoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<AlunoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<AvaliacaoContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<DisciplinaContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<EscolaContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<MensagemContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<PaiContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<ProfessorContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<SumarioContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddDbContext<TurmaContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

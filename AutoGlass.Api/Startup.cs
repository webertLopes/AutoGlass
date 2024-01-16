using AutoGlass.Application.Repositories;
using AutoGlass.Application.Services;
using AutoGlass.Application.Services.Interfaces;
using AutoGlass.Infrastructure.Factory;
using AutoGlass.Infrastructure.Mapping;
using AutoGlass.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data;

namespace AutoGlass.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddTransient<IDbConnection>(provider => SqlFactory.CreateDatabase());

            LoadServices(services);

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoGlass.Api", Version = "v1" });
            });
        }

        private static void LoadServices(IServiceCollection services)
        {
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IProdutoServices, ProdutoServices>();
            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<IFornecedorServices, FornecedorServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
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

using Desafio.AMcom.Application;
using Desafio.AMcom.Domain;
using Desafio.AMcom.Infra;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using System;

namespace Desafio.AMcom
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddScoped<IPaisRepository, PaisRepository>();

            services.AddMediatR(typeof(AppHandlerBase<>));

            services.AddAutoMapper(typeof(AppHandlerBase<>));

            services.AddMemoryCache();

            services.AddHttpClient("reqres", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://reqres.in/");
            })
            .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600))
            );

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio.AMcom", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio.AMcom v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

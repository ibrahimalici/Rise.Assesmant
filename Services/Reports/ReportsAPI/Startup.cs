using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReportsAPI.Application.Communications;
using ReportsAPI.Data;
using ReportsAPI.Mappings;
using ReportsAPI.Repositories;

namespace ReportsAPI
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
            string queeHost = Configuration.GetValue<string>("DatabaseSettings:RabbitMQHost");
            string queeUserName = Configuration.GetValue<string>("DatabaseSettings:RabbitMQUserName");
            string queePass = Configuration.GetValue<string>("DatabaseSettings:RabbitMQPass");

            services.AddMassTransit(o =>
            {
                o.AddConsumer<ReportMessageConsumer>();

                o.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(queeHost, "/", h =>
                    {
                        h.Username(queeUserName);
                        h.Password(queePass);
                    });

                    cfg.ReceiveEndpoint("report-prepare", e =>
                    {
                        e.ConfigureConsumer<ReportMessageConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddTransient<IDataRepository, DataRepository>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AutoMapperConfigurations());
            }, typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReportsAPI", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportsAPI v1"));
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

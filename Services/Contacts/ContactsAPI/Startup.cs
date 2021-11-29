using ContactsAPI.Application.Communications;
using ContactsAPI.Mappings;
using ContactsAPI.Persistance;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedLibrary.Messages;
using System;

namespace ContactsAPI
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
            string cnn = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            string queeHost = Configuration.GetValue<string>("DatabaseSettings:RabbitMQHost");
            string queeUserName = Configuration.GetValue<string>("DatabaseSettings:RabbitMQUserName");
            string queePass = Configuration.GetValue<string>("DatabaseSettings:RabbitMQPass");

            services.AddMassTransit(o=>
            {
                o.AddConsumer<ReportResultMessageConsumer>();

                o.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(queeHost, "/", h =>
                      {
                          h.Username(queeUserName);
                          h.Password(queePass);
                      });

                    cfg.ReceiveEndpoint("queue:report-prepare", c =>
                    {
                        c.Handler<ReportMessage>(ctx =>
                        {
                            return Console.Out.WriteLineAsync("Mesaj Gönderildi");
                        });
                    });
                });
            });

            services.AddMassTransitHostedService();
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(cnn));
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AutoMapperConfigurations());
            });
            services.AddMediatR(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactsAPI", Version = "v1" });
            });
            services.AddScoped<IMassTransitHelper, MassTransitHelper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DatabaseContext db)
        {
            db.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactsAPI v1"));
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

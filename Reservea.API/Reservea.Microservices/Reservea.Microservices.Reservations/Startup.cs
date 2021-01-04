using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Reservea.Common.Helpers;
using Reservea.Common.Interfaces;
using Reservea.Microservices.Reservations.Interfaces.Services;
using Reservea.Microservices.Reservations.Services;
using Reservea.Persistance;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.UnitsOfWork;
using System.Reflection;

namespace Reservea.Microservices.Reservations
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
            services.AddControllers();

            services.AddScoped<IReservationsService, ReservationsService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IMailSendingService, MailSendingService>();
            services.AddSingleton<CannonService>();
            services.AddScoped<IMailTemplatesHelper, MailTemplatesHelper>();
            services.AddScoped<IReservationsUnitOfWork, ReservationsUnitOfWork>();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
               options =>
               {
                   var publicAuthorizationKey = Configuration.GetSection("AppSettings:PublicKey").Value;
                   var key = JwtTokenHelper.BuildRsaSigningKey(publicAuthorizationKey);
                   options.TokenValidationParameters = JwtTokenHelper.GetTokenValidationParameters(key);
               });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Configuration.GetValue<string>("ApplicationName"), Version = "v1" });
                var xmlFilePath = "Swagger_doc.xml";
                c.IncludeXmlComments(xmlFilePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Reservea.Common.Helpers;
using Reservea.Common.Interfaces;
using Reservea.Common.Middleware;
using Reservea.Microservices.Users.Interfaces.Services;
using Reservea.Microservices.Users.Services;
using Reservea.Persistance;
using Reservea.Persistance.Models;
using System;
using System.Reflection;

namespace Reservea.Microservices.Users
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

            #region Depndency Injection
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IMailSendingService, MailSendingService>();
            services.AddSingleton<CannonService>();
            services.AddScoped<IMailTemplatesHelper, MailTemplatesHelper>();
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IRolesService, RolesService>();
            #endregion

            #region IdentityConfiguration
            IdentityBuilder builder = services.AddIdentityCore<User>();
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddDefaultTokenProviders();
            #endregion
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

            app.UseMiddleware<ExceptionMiddleware>();

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

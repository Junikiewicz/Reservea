using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Reservea.Microservices.Resources.Helpers;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Microservices.Resources.Services;
using Reservea.Persistance;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.UnitsOfWork;
using System.Reflection;

namespace Reservea.Microservices.Resources
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
            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new TimeSpanConverter()));

            AddDependencyInjection(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = Configuration.GetValue<string>("ApplicationName"), Version = "v1" });
                var xmlFilePath = "Swagger_doc.xml";
                c.IncludeXmlComments(xmlFilePath);
            });
        }

        public void AddDependencyInjection(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IResourcesUnitOfWork, ResourcesUnitOfWork>();

            services.AddScoped<IResourcesService, ResourcesService>();
            services.AddScoped<IAttributesService, AttributesService>();
            services.AddScoped<IResourceTypesService, ResourceTypesService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
        }
    }
}

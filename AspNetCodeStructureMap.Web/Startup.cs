using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCodeStructureMap.Web.Controllers;
using AspNetCodeStructureMap.Web.DataAccess;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace AspNetCodeStructureMap.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddControllersAsServices();
            return this.ConfigureIoc(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IServiceProvider ConfigureIoc(IServiceCollection services)
        {
            IContainer container = new Container();

            container.Configure(configure: config =>
            {
                
                config.Scan(scanner =>
                {
                    scanner.AssemblyContainingType(typeof(Startup));
                    scanner.WithDefaultConventions();
                    
                });

                config.For<IRepository>().Add(new StaffDataRepository()).Named("StaffDataRepository");
                config.For<IRepository>().Add(new CustomerDataRepository()).Named("CustomerDataRepository");
           

                config.Populate(services);
            });


            return container.GetInstance<IServiceProvider>();
        }
    }
}

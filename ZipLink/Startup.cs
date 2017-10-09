using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ZipLink.BusinessLogic;
using ZipLink.DataAccessLayer;

namespace ZipLink
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetSection("ConnectionString").Value;
            if (conn.IsNullOrEmpty()) throw new ApplicationException(conn);
            
            
            services.AddTransient<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
                conn, PostgreSqlDialect.Provider));

            services.AddTransient<IZippedLinksRepository>(c =>
                new OrmLiteZippedLinksRepository(c.GetService<IDbConnectionFactory>()));
            
            
            services.AddTransient<IUrlGenerator>(f => new GuidUrlGenerator());
            services.AddTransient<ZippedLinkFacade>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }

            app.UseStaticFiles();

            app.UseServiceStack(new AppHost(Configuration));
            app.UseMvc(/*routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{hash}", 
                    defaults: new {controller = "Home", action = "Index"});
//                routes.MapRoute(
//                    name: "default1",
//                    template: "{*pathInfo}", 
//                    defaults: new {controller = "Home", action = "Index"});
            }*/
            );
        }
    }
}

namespace Web
{
    using System;
    using System.IO;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Modules;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer Container { get; set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterConfiguredModulesFromAssemblyContaining<ServiceModule>(
                Configuration as IConfigurationRoot);
            Container = containerBuilder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "node_modules")
                ),
                RequestPath = "/node_modules",
                EnableDirectoryBrowsing = false
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Speaker}/{action=List}/{id?}");
            });
        }
    }
}
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<IOhMyRepository>(new OhMySampleRepository());

            services.AddControllersWithViews(mvc => mvc.EnableEndpointRouting = false);

            var model = OhMyModelBuilder.GetEdmModel();
            services.AddControllers()
                .AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5)
                    .AddRouteComponents("odata", model)
                    .Conventions.Add(new OhMyConvention())
                );

            services.AddRazorPages();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseODataRouteDebug("odata");
            app.UseODataQueryRequest();

            app.Use(next => context => {
                var endpoint = context.GetEndpoint();
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (endpoint == null) {
                    return next(context);
                }

                return next(context);
            });

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
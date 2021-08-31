using System;
using System.IO;
using System.Linq;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Controllers;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Routing;
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

            var model = OhMyModelBuilder.GetEdmModel();
            services.AddControllers()
                .AddOData(opt => opt.Count().Filter().Expand().Select().OrderBy().SetMaxTop(5)
                    .AddRouteComponents("odata", model)
                    .Conventions.Add(new OhMyConvention())
                );

             services.AddRazorPages(); // for MapRazorPages()

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

            app.UseRouting(); // for UseEndpoints()

            app.UseODataRouteDebug("odata"); // for display of end points at http://localhost:56027/odata
            app.UseODataQueryRequest();

            app.Use(next => context => {
                var endpoint = context.GetEndpoint();
                // ReSharper disable once ConvertIfStatementToReturnStatement
                if (endpoint == null) {
                    return next(context);
                }

                return next(context);
            });

            app.UseEndpoints(ConfigureEndpoints);
        }

        private static void ConfigureEndpoints(IEndpointRouteBuilder endpoints) {
            endpoints.MapControllers();
            endpoints.MapDefaultControllerRoute(); // for http://localhost:56027/
            endpoints.MapRazorPages(); // for e.g. return View("Index");
            var dataSources = endpoints.DataSources;
            if (dataSources.Count != 2) {
                throw new Exception($"Unexpected number of data sources: {dataSources.Count}");
            }

            var displayNames = dataSources.SelectMany(e => e.Endpoints.Select(e => e.DisplayName)).ToList();
            if (displayNames.Contains(null)) {
                throw new Exception(nameof(displayNames));
            }
            if (displayNames.Count != 8) {
                throw new Exception($"Unexpected number of display names: {displayNames.Count}");
            }

            var displayName = displayNames.First();
            if (!displayName.Contains(nameof(HomeController))) {
                throw new Exception($"Unexpected display name: {displayName}");
            }

            displayNames = displayNames.Where(IsRelatedToOhMyEntitiesODataController).ToList();
            if (displayNames.Count != 2) {
                throw new Exception($"Unexpected number of display names related to {nameof(OhMyEntitiesODataController)}: {displayNames.Count}");
            }

            File.WriteAllLines(@"c:\Temp\ohMyDisplayNames.txt", displayNames);
        }

        private static bool IsRelatedToOhMyEntitiesODataController(string displayName) {
            return displayName.Contains(nameof(OhMyEntitiesODataController))
                   && displayName.Contains(nameof(OhMyEntitiesODataController.Get));
        }
    }
}
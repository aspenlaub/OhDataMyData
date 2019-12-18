using Aspenlaub.Net.GitHub.CSharp.DvinCore.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;
using Aspenlaub.Net.GitHub.CSharp.Pegh.Components;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddOData();

            services.AddControllersWithViews(mvc => mvc.EnableEndpointRouting = false);

            services.UseDvinAndPegh(new DummyCsArgumentPrompter());

            services.AddSingleton<IOhMyRepository>(new OhMySampleRepository());
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

            app.UseAuthorization();

            app.UseMvc(routebuilder => {
                routebuilder.Select().Expand().Filter().OrderBy(QueryOptionSetting.Allowed).MaxTop(null).Count();
                routebuilder.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel());
                routebuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static IEdmModel GetEdmModel() {
            var builder = new ODataConventionModelBuilder {
                Namespace = "Aspenlaub.Net.GitHub.CSharp.OhDataMyData",
                ContainerName = "DefaultContainer"
            };
            builder.EntitySet<OhMyEntity>("OhMyEntities");

            return builder.GetEdmModel();
        }
    }
}

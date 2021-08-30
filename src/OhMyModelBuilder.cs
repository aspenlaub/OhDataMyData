using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData {
    public static class OhMyModelBuilder {
        public static IEdmModel GetEdmModel() {
            var builder = new ODataConventionModelBuilder {
                Namespace = "Aspenlaub.Net.GitHub.CSharp.OhDataMyData",
                ContainerName = "DefaultContainer"
            };
            builder.EntitySet<OhMyEntity>("OhMyEntities");

            return builder.GetEdmModel();
        }
    }
}

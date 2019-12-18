using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components {
    public class OhMySampleRepository : OhMyRepository {
        public const string FirstEntityName = "Solution", SecondEntityName = "Project";

        public OhMySampleRepository() {
            Add(new OhMyEntity { Name = FirstEntityName });
            Add(new OhMyEntity { Name = SecondEntityName });
        }
    }
}

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    [TestClass]
    public class OhMyEntitiesControllerTest {
        [TestMethod]
        public async Task CanGetEntities() {
            var client = ControllerTestHelpers.CreateHttpClient();
            var response = await client.GetAsync("odata/OhMyEntities");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<ODataResponse<OhMyEntity>>(responseString).Value.ToList();
            Assert.AreEqual(2, entities.Count);
            Assert.AreEqual(OhMySampleRepository.FirstEntityName, entities[0].Name);
            Assert.AreEqual(OhMySampleRepository.SecondEntityName, entities[1].Name);
        }

        [TestMethod]
        public async Task CanGetEntity() {
            var client = ControllerTestHelpers.CreateHttpClient();
            var response = await client.GetAsync("odata/OhMyEntities");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<ODataResponse<OhMyEntity>>(responseString).Value.ToList();
            Assert.IsTrue(entities.Count != 0);

            response = await client.GetAsync($"odata/OhMyEntities('{entities[0].Id}')");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            responseString = await response.Content.ReadAsStringAsync();
            var entity = JsonConvert.DeserializeObject<OhMyEntity>(responseString);
            Assert.AreEqual(entities[0].Name, entity.Name);
        }
    }
}

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Components;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Test {
    [TestClass]
    public class OhMyEntitiesControllerTest {
        private HttpClient Sut;

        [TestInitialize]
        public void Initialize() {
            Sut = ControllerTestHelpers.CreateHttpClient();
        }

        [TestMethod]
        public async Task GetAsync_WithOhMyEntities_ReturnsTwoEntities() {
            var response = await Sut.GetAsync("odata/OhMyEntities");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<ODataResponse<OhMyEntity>>(responseString).Value.ToList();
            Assert.AreEqual(2, entities.Count);
            Assert.AreEqual(OhMySampleRepository.FirstEntityName, entities[0].Name);
            Assert.AreEqual(OhMySampleRepository.SecondEntityName, entities[1].Name);
        }

        [TestMethod]
        public async Task GetAsync_WithEntityIdInParentheses_ReturnsTheEntity() {
            var entity = await GetEntityAsync();
            var response = await Sut.GetAsync($"odata/OhMyEntities('{entity.Id}')");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<OhMyEntity>(responseString);
            Assert.AreEqual(entity.Name, responseEntity.Name);
        }

        [TestMethod]
        public async Task GetAsync_WithEntityIdAsSubPath_ReturnsTheEntity() {
            var entity = await GetEntityAsync();
            var response = await Sut.GetAsync($"odata/OhMyEntities/{entity.Id}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseEntity = JsonConvert.DeserializeObject<OhMyEntity>(responseString);
            Assert.AreEqual(entity.Name, responseEntity.Name);
        }

        protected async Task<OhMyEntity> GetEntityAsync() {
            var response = await Sut.GetAsync("odata/OhMyEntities");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var entities = JsonConvert.DeserializeObject<ODataResponse<OhMyEntity>>(responseString).Value.ToList();
            Assert.IsTrue(entities.Count != 0);
            return entities.First();
        }
    }
}

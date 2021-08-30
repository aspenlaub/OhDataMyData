using System.Linq;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Controllers {
    public class OhMyEntitiesODataController : ControllerBase {
        private readonly IOhMyRepository Repository;

        public OhMyEntitiesODataController(IOhMyRepository repository) {
            Repository = repository;
        }

        [HttpGet, EnableQuery]
        public IActionResult Get() {
            return Ok(Repository.GetEntities());
        }

        [HttpGet, EnableQuery]
        public IActionResult Get(string key) {
            var entity = Repository.GetEntities().FirstOrDefault(e => e.Id == key);
            if (entity == null) { return NotFound(); }

            return Ok(entity);
        }

    }
}

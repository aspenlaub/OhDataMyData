using System.Linq;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Controllers {
    public class OhMyEntitiesODataController : ODataController {
        private readonly IOhMyRepository vRepository;

        public OhMyEntitiesODataController(IOhMyRepository repository) {
            vRepository = repository;
        }

        [HttpGet, ODataRoute("OhMyEntities"), EnableQuery]
        public IActionResult Get() {
            return Ok(vRepository.GetEntities());
        }

        [HttpGet, ODataRoute("OhMyEntities({key})"), EnableQuery]
        public IActionResult Get([FromODataUri] string key) {
            var entity = vRepository.GetEntities().FirstOrDefault(e => e.Id == key);
            if (entity == null) { return NotFound(); }

            return Ok(entity);
        }

    }
}

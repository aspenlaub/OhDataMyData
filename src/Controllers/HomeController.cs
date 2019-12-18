using System.Diagnostics;
using Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aspenlaub.Net.GitHub.CSharp.OhDataMyData.Controllers {
    public class HomeController : Controller {
        // ReSharper disable once NotAccessedField.Local
        private readonly ILogger<HomeController> vLogger;

        public HomeController(ILogger<HomeController> logger) {
            vLogger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using IllinoisSiteScannerWeb.Emergency;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IllinoisSiteScannerWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class EmergencyController(EmergencyContainer container) : ControllerBase {

        private readonly EmergencyContainer _container = container;

        [AllowAnonymous]
        [DisableCors]
        public IActionResult Index() {
            var results = _container.Get();
            return string.IsNullOrWhiteSpace(results.Title) && string.IsNullOrWhiteSpace(results.Description) ?
                new JsonResult("") :
                new JsonResult(results);
        }

        [HttpGet("full")]
        [AllowAnonymous]
        [DisableCors]
        public IActionResult Time() => new JsonResult(_container.Get());


        [HttpGet("test")]
        [AllowAnonymous]
        [DisableCors]
        public IActionResult Test() => new JsonResult(new Alert {
            Title = "Illini-Alert. TEST ALERT. Do not cross Wright Street at Daniel or Chalmers while crews address gas leak.",
            Description = "Illini-Alert. TEST ALERT.  Do not cross Wright Street at Daniel or Chalmers while crews address gas leak. Pedestrians may cross Wright Street at Green, John or Armory. Want more ways to be notified by Illini - Alert? Follow us on Twitter at http://twitter.com/illinialert and facebook at http://www.facebook.com/illinialert."
        });

    }
}

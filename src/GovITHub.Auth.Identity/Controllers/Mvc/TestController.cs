using GovITHub.Auth.Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace GovITHub.Auth.Common.Controllers.Mvc
{
    public class TestController : Controller
    {
        IConfigRepository configRepository;
        public TestController(IConfigRepository configRepository)
        {
            this.configRepository = configRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Scopes()
        {
            return Json(configRepository.GetScopesAsync());
        }

        public IActionResult Clients(string id)
        {
            return Json(configRepository.GetClient(id));
        }
    }
}
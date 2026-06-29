using Microsoft.AspNetCore.Mvc;

namespace MovieManager.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            //لأننا نريد أن نرجع نصًا عاديًا إلى المتصفح.
            return Content ("Hello from Test Controller");
        }
        public IActionResult About()
        {
            return Content("About Movie Manager");

        }
        public IActionResult Welcome(string name)
        {
            return Content($"Welcome {name}");
        }
    }
}

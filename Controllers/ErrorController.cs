using Microsoft.AspNetCore.Mvc;

namespace nicasource_netcore.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            return View("NotFound");
        }
    }
}
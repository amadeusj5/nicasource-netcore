using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nicasource_netcore.Interfaces;
using nicasource_netcore.Models;

namespace nicasource_netcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComicService _comicService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IComicService comicService)
        {
            _logger = logger;
            _comicService = comicService;
        }

        public async Task<IActionResult> Index()
        {
            return View("~/Views/Comic/Index.cshtml", await _comicService.getAsync());
        }

        [Route("Home")]
        public IActionResult Home()
        {
            return RedirectToAction("index");
        }
    }
}

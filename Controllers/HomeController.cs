using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nicasource_netcore.Interfaces;

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
            return View(await _comicService.getAsync());
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nicasource_netcore.Interfaces;

namespace nicasource_netcore.Controllers
{
    public class ComicController : Controller
    {
         private readonly IComicService _comicService;
        private readonly ILogger<HomeController> _logger;

        public ComicController(ILogger<HomeController> logger, IComicService comicService)
        {
            _logger = logger;
            _comicService = comicService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("index", "Home");
        }

        [Route("comic/{id:int}")]
        public async Task<IActionResult> get(int id)
        {
            return View("~/Views/Comic/Index.cshtml", await _comicService.getAsync(id));
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nicasource_netcore.Interfaces;
using nicasource_netcore.Models;

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

        [Route("Comic/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            ComicViewModel comic;

            try
            {
                comic = await _comicService.getAsync(id);
            }
            catch (ArgumentNullException e)
            {
                return Redirect(e.ParamName);
            }

            return View("~/Views/Comic/Index.cshtml", comic);
        }
    }
}
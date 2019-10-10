using System.Threading.Tasks;
using nicasource_netcore.Interfaces;
using nicasource_netcore.Models;

namespace nicasource_netcore.Services
{
    public class ComicService : IComicService
    {
        private readonly IHttpClientService _httpClientService;
        private const string XKCD_URL = "https://xkcd.com";

        public ComicService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<ComicViewModel> getAsync(int? id = null)
        {
            if (!id.HasValue)
            {
                return transform(await getComicOfTheDay());
            }

            return transform(new ComicModel());
        }

        public async Task<ComicModel> getComicOfTheDay() => await _httpClientService.get<ComicModel>(XKCD_URL + "/info.0.json");

        public ComicViewModel transform(ComicModel comic, int? comicId = null)
        {
            return new ComicViewModel
            {
                Comic = comic
            };
        }
    }
}
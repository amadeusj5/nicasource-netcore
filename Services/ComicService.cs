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
                return await transformAsync(await getComicOfTheDay());
            }

            var result = await _httpClientService.get<ComicModel>(string.Format("{0}/{1}/info.0.json", XKCD_URL, id));

            return await transformAsync(result, id);
        }

        public async Task<ComicModel> getComicOfTheDay() => await _httpClientService.get<ComicModel>(XKCD_URL + "/info.0.json");

        public async Task<ComicViewModel> transformAsync(ComicModel comic, int? comicId = null)
        {
            return new ComicViewModel
            {
                Comic = comic,
                Previous = previousComicUrl(comic, comicId),
                Next = await nextComicUrlAsync(comicId)
            };
        }

        public string previousComicUrl(ComicModel comic, int? comicId = null)
        {
            if (comicId.HasValue && comicId.Equals(1))
            {
                return string.Empty;
            }

            return string.Format("/comic/{0}", comic.Num - 1);
        }

        public async Task<string> nextComicUrlAsync(int? comicId = null)
        {
            if (!comicId.HasValue)
            {
                return string.Empty;
            }

            var todayComic = await getComicOfTheDay();

            if (comicId.Equals(todayComic.Num))
            {
                return string.Empty;
            }

            return string.Format("/comic/{0}", comicId + 1);
        }
    }
}
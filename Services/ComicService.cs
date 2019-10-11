using System;
using System.Linq;
using System.Threading.Tasks;
using nicasource_netcore.Interfaces;
using nicasource_netcore.Models;

namespace nicasource_netcore.Services
{
    public class ComicService : IComicService
    {
        private readonly HttpClientService _httpClientService;
        private const string XKCD_URL = "https://xkcd.com";

        public ComicService()
        {
            _httpClientService = new HttpClientService();
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


        /// <exception cref="System.ArgumentNullException">Thrown when Comic is null. Return the redirect Url as exception message</exception>
        public async Task<ComicViewModel> transformAsync(ComicModel comic, int? comicId = null)
        {
            if (!comic.Num.HasValue)
            {
                ComicModel todayComic = await getComicOfTheDay();

                if (comicId > todayComic.Num)
                {
                    throw new ArgumentNullException("/");
                }

                var previousComicId = getPreviouslyRequestedComicId();

                if (!previousComicId.HasValue || previousComicId < comicId)
                {
                    throw new ArgumentNullException(string.Format("/comic/{0}", comicId + 1));
                }

                throw new ArgumentNullException(string.Format("/comic/{0}", comicId - 1));
            }

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

        public int? getPreviouslyRequestedComicId()
        {
            string url = _httpClientService.getPreviousRequest();

            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var path = url.Split('/');

            return int.Parse(path.Last());
        }
    }
}
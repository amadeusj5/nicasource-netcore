using System.Threading.Tasks;
using nicasource_netcore.Models;

namespace nicasource_netcore.Interfaces
{
    public interface IComicService
    {
        Task<ComicViewModel> getAsync(int? id = null);

        Task<ComicModel> getComicOfTheDay();
        
        Task<ComicViewModel> transformAsync(ComicModel comic, int? comicId = null);

        string previousComicUrl(ComicModel comic, int? comicId = null);

        Task<string> nextComicUrlAsync(int? comicId = null);
        
        int? getPreviouslyRequestedComicId();
    }
}
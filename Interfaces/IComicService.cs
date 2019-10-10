using System.Threading.Tasks;
using nicasource_netcore.Models;

namespace nicasource_netcore.Interfaces
{
    public interface IComicService
    {
        Task<ComicViewModel> getAsync(int? id = null);

        Task<ComicModel> getComicOfTheDay();
        
        ComicViewModel transform(ComicModel comic, int? comicId = null);
    }
}
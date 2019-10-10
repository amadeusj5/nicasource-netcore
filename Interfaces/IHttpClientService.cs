using System.Threading.Tasks;

namespace nicasource_netcore.Interfaces
{
    public interface IHttpClientService
    {
        Task<T> get<T>(string url);
    }
}
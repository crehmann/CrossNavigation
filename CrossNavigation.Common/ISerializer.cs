using System.Threading.Tasks;

namespace CrossNavigation.Common
{
    public interface ISerializer
    {
        Task<T> DeserializeAsync<T>(string jsonString);

        Task<string> SerializeAsync(object obj);
    }
}
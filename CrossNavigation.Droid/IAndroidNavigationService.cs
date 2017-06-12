using System.Threading.Tasks;
using Android.Content;
using CrossNavigation.Common;

namespace CrossNavigation.Droid
{
    public interface IAndroidNavigationService : INavigationService
    {
        Task<T> GetParameterFromIntentAsync<T>(Intent intent);
    }
}
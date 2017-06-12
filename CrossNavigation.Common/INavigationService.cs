using System.Threading.Tasks;

namespace CrossNavigation.Common
{
    public interface INavigationService
    {
        Task CloseModalAsync();
        Task GoBackAsync();

        Task GoBackWithResultAsync(ResultCode result, object parameter);

        Task NavigateToAsync(string pageKey);

        Task NavigateToAsync(string pageKey, object parameter);

        Task NavigateToForResultAsync(string pageKey, int requestCode, object parameter);

        Task NavigateToForResultAsync(string pageKey, int requestCode);

        Task PopToAsync(string pageKey);

        Task PopToAndNavigateToAsync(string popToPageKey, string navigateToPageKey, object parameter);

        Task ShowModalAsync(string pageKey);
        
    }
}
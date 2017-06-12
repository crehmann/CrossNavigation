using System.Threading.Tasks;

namespace CrossNavigation.Common
{
    public interface INavigationInterceptor
    {
        Task<bool> OnCloseModalAsync(INavigationService navigationService);

        Task<bool> OnGoBackAsync(INavigationService navigationService);

        Task<bool> OnGoBackWithResultAsync(INavigationService navigationService, ResultCode result, object parameter);

        Task<bool> OnNavigateToAsync(INavigationService navigationService, string pageKey);

        Task<bool> OnNavigateToAsync(INavigationService navigationService, string pageKey, object parameter);

        Task<bool> OnNavigateToForResultAsync(INavigationService navigationService, string pageKey, int requestCode, object parameter);

        Task<bool> OnNavigateToForResultAsync(INavigationService navigationService, string pageKey, int requestCode);

        Task<bool> OnPopToHomeAsync(INavigationService navigationService, string homePageKey);

        Task<bool> OnPopToHomeAndNavigateToAsync(INavigationService navigationService, string homePageKey, string pageKey, object parameter);

        Task<bool> OnShowModalAsync(INavigationService navigationService, string pageKey);
    }
}
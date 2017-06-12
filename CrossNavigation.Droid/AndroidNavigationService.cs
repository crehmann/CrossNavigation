using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using CrossNavigation.Common;

namespace CrossNavigation.Droid
{
    internal class AndroidNavigationService : IAndroidNavigationService
    {
        private readonly ActivityTypeConfiguration _activityTypeConfiguration;
        private readonly INavigationInterceptor _navigationInterceptor;
        private readonly IIntentInterceptor _intentInterceptor;
        private readonly Context _applicationContext;

        public AndroidNavigationService(ActivityTypeConfiguration activityTypeConfiguration, INavigationInterceptor navigationInterceptor
            , IIntentInterceptor intentInterceptor)
        {
            if (activityTypeConfiguration == null) throw new ArgumentNullException(nameof(activityTypeConfiguration));
            if (navigationInterceptor == null) throw new ArgumentNullException(nameof(navigationInterceptor));
            if (intentInterceptor == null) throw new ArgumentNullException(nameof(intentInterceptor));
            _activityTypeConfiguration = activityTypeConfiguration;
            _navigationInterceptor = navigationInterceptor;
            _intentInterceptor = intentInterceptor;
        }

        public async Task CloseModalAsync()
        {
            if (await _navigationInterceptor.OnCloseModalAsync(this).ConfigureAwait(false)) return;
            //TODO implement
            throw new NotImplementedException();
        }

        public async Task GoBackAsync()
        {
            if (await _navigationInterceptor.OnGoBackAsync(this).ConfigureAwait(false)) return;
            //TODO implement
            throw new NotImplementedException();
        }

        public Task GoBackWithResultAsync(ResultCode result, object parameter)
        {
            throw new NotImplementedException();
        }

        public async Task NavigateToAsync(string pageKey)
        {
            if (await _navigationInterceptor.OnNavigateToAsync(this, pageKey).ConfigureAwait(false)) return;
            var intent = _activityTypeConfiguration.CreateIntent(pageKey);
            intent = _intentInterceptor.OnIntent(intent, currentPageKey, pageKey);
            _applicationContext.StartActivity(intent);
        }

        public Task NavigateToAsync(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToForResultAsync(string pageKey, int requestCode, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToForResultAsync(string pageKey, int requestCode)
        {
            throw new NotImplementedException();
        }

        public Task PopToAsync(string pageKey)
        {
            throw new NotImplementedException();
        }

        public Task PopToAndNavigateToAsync(string popToPageKey, string navigateToPageKey, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task ShowModalAsync(string pageKey)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetParameterFromIntentAsync<T>(Intent intent)
        {
            return await _activityTypeConfiguration.GetParameterFromIntent<T>(intent).ConfigureAwait(false);
        }
    }
}
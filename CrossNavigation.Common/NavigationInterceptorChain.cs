using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossNavigation.Common
{
    public sealed class NavigationInterceptorChain : INavigationInterceptor
    {
        private readonly IList<INavigationInterceptor> _interceptors = new List<INavigationInterceptor>();

        private NavigationInterceptorChain()
        {
        }

        public async Task<bool> OnCloseModalAsync(INavigationService navigationService)
        {
            return await LoopThroughInterceptors(x => x.OnCloseModalAsync(navigationService))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnGoBackAsync(INavigationService navigationService)
        {
            return await LoopThroughInterceptors(x => x.OnGoBackAsync(navigationService))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnGoBackWithResultAsync(INavigationService navigationService, ResultCode result, object parameter)
        {
            return await LoopThroughInterceptors(x => x.OnGoBackWithResultAsync(navigationService, result, parameter))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnNavigateToAsync(INavigationService navigationService, string pageKey)
        {
            return await LoopThroughInterceptors(x => x.OnNavigateToAsync(navigationService, pageKey))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnNavigateToAsync(INavigationService navigationService, string pageKey, object parameter)
        {
            return await LoopThroughInterceptors(x => x.OnNavigateToAsync(navigationService, pageKey, parameter))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnNavigateToForResultAsync(INavigationService navigationService, string pageKey, int requestCode, object parameter)
        {
            return await LoopThroughInterceptors(x => x.OnNavigateToForResultAsync(navigationService, pageKey, requestCode, parameter))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnNavigateToForResultAsync(INavigationService navigationService, string pageKey, int requestCode)
        {
            return await LoopThroughInterceptors(x => x.OnNavigateToAsync(navigationService, pageKey, requestCode))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnPopToHomeAsync(INavigationService navigationService, string homePageKey)
        {
            return await LoopThroughInterceptors(x => x.OnNavigateToAsync(navigationService, homePageKey))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnPopToHomeAndNavigateToAsync(INavigationService navigationService, string homePageKey, string pageKey, object parameter)
        {
            return await LoopThroughInterceptors(x => x.OnPopToHomeAndNavigateToAsync(navigationService, homePageKey, pageKey, parameter))
                .ConfigureAwait(false);
        }

        public async Task<bool> OnShowModalAsync(INavigationService navigationService, string pageKey)
        {
            return await LoopThroughInterceptors(x => x.OnShowModalAsync(navigationService, pageKey))
                .ConfigureAwait(false);
        }

        private async Task<bool> LoopThroughInterceptors(Func<INavigationInterceptor, Task<bool>> func)
        {
            foreach (var navigationInterceptor in _interceptors)
            {
                if (await func(navigationInterceptor).ConfigureAwait(false)) return true;
            }

            return false;
        }

        public class Builder
        {
            private readonly NavigationInterceptorChain _chain = new NavigationInterceptorChain();

            public Builder AddInterceptor(INavigationInterceptor interceptor)
            {
                if (interceptor == null) throw new ArgumentNullException(nameof(interceptor));
                _chain._interceptors.Add(interceptor);
                return this;
            }

            public Builder AddInterceptors(IEnumerable<INavigationInterceptor> interceptors)
            {
                if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
                foreach (var navigationInterceptor in interceptors)
                {
                    _chain._interceptors.Add(navigationInterceptor);
                }
                return this;
            }

            public INavigationInterceptor Build()
            {
                return _chain;
            }
        }
    }
}
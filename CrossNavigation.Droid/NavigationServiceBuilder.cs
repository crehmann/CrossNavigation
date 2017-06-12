using System;
using Android.Content;
using CrossNavigation.Common;

namespace CrossNavigation.Droid
{
    public sealed class NavigationServiceBuilder
    {
        private readonly ActivityTypeConfiguration _activityTypeConfiguration;
        private readonly NavigationInterceptorChain.Builder _interceptorChainBuilder;
        private readonly IntentInterceptorChain.Builder _intentInterceptorChainBuilder;

        private NavigationServiceBuilder(Context applicationContext, ISerializer serializer)
        {
            _activityTypeConfiguration = new ActivityTypeConfiguration(applicationContext, serializer);
            _interceptorChainBuilder = new NavigationInterceptorChain.Builder();
            _intentInterceptorChainBuilder = new IntentInterceptorChain.Builder();
        }

        public NavigationServiceBuilder AddNavigationInterceptor(INavigationInterceptor interceptor)
        {
            _interceptorChainBuilder.AddInterceptor(interceptor);
            return this;
        }

        public IAndroidNavigationService Build()
        {
            var interceptorChain = _interceptorChainBuilder.Build();
            var intentInterceptorChain = _intentInterceptorChainBuilder.Build();
            return new AndroidNavigationService(_activityTypeConfiguration, interceptorChain, intentInterceptorChain);
        }

        public static NavigationServiceBuilder Create(Context applicationContext)
        {
            return Create(applicationContext, new ParameterSerializer());
        }

        public static NavigationServiceBuilder Create(Context applicationContext, ISerializer serializer)
        {
            if (applicationContext == null) throw new ArgumentNullException(nameof(applicationContext));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            return new NavigationServiceBuilder(applicationContext, serializer);
        }

        public NavigationServiceBuilder RegisterActivityType(Type activityType, string pageKey)
        {
            _activityTypeConfiguration.RegisterActivityType(activityType, pageKey);
            return this;
        }
    }
}
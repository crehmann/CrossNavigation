using System.Collections.Generic;
using Android.Content;

namespace CrossNavigation.Droid
{
    internal class IntentInterceptorChain : IIntentInterceptor
    {
        private readonly IList<IIntentInterceptor> _intentInterceptors;

        private IntentInterceptorChain()
        {
            _intentInterceptors = new List<IIntentInterceptor>();
        }

        public Intent OnIntent(Intent intent, string currentPageKey, string destinationPageKey)
        {
            var result = intent;
            foreach (var intentInterceptor in _intentInterceptors)
            {
                result = intentInterceptor.OnIntent(intent, currentPageKey, destinationPageKey);
            }
            return result;
        }

        internal class Builder
        {
            private readonly IntentInterceptorChain _intentInterceptorChain;

            public Builder()
            {
                _intentInterceptorChain = new IntentInterceptorChain();
            }

            public IIntentInterceptor Build()
            {
                return _intentInterceptorChain;
            }

            public Builder Intercept(IIntentInterceptor interceptor)
            {
                _intentInterceptorChain._intentInterceptors.Add(interceptor);
                return this;
            }

            public Builder InterceptFromOrigin(IIntentInterceptor interceptor, string originKey)
            {
                var conditionalInterceptor = ConditionalIntentInterceptor.FromOriginKeyCondition(interceptor, originKey);
                _intentInterceptorChain._intentInterceptors.Add(conditionalInterceptor);
                return this;
            }

            public Builder InterceptFromOriginToDesintaion(IIntentInterceptor interceptor, string originKey, string destinationKey)
            {
                var conditionalInterceptor = ConditionalIntentInterceptor.FromOriginToDestinationKeyCondition(interceptor, originKey, destinationKey);
                _intentInterceptorChain._intentInterceptors.Add(conditionalInterceptor);
                return this;
            }

            public Builder InterceptToDesintaion(IIntentInterceptor interceptor, string destinationKey)
            {
                var conditionalInterceptor = ConditionalIntentInterceptor.ToDestinationKeyCondition(interceptor, destinationKey);
                _intentInterceptorChain._intentInterceptors.Add(conditionalInterceptor);
                return this;
            }
        }
    }
}
using Android.Content;

namespace CrossNavigation.Droid
{
    internal sealed class ConditionalIntentInterceptor : IIntentInterceptor
    {
        private readonly string _currentKeyCondition;
        private readonly string _destinationKeyCondition;
        private readonly IIntentInterceptor _intentInterceptor;

        private ConditionalIntentInterceptor(IIntentInterceptor intentInterceptor, string currentKeyCondition, string destinationKeyCondition)
        {
            _intentInterceptor = intentInterceptor;
            _currentKeyCondition = currentKeyCondition;
            _destinationKeyCondition = destinationKeyCondition;
        }

        public Intent OnIntent(Intent intent, string currentPageKey, string destinationPageKey)
        {
            if (IsNotSetOrEqual(_currentKeyCondition, currentPageKey) && IsNotSetOrEqual(_destinationKeyCondition, destinationPageKey))
            {
                return _intentInterceptor.OnIntent(intent, currentPageKey, destinationPageKey);
            }

            return intent;
        }


        public static IIntentInterceptor FromOriginToDestinationKeyCondition(IIntentInterceptor intentInterceptor, string currentKey, string destinationKey)
        {
            return new ConditionalIntentInterceptor(intentInterceptor, currentKey, destinationKey);
        }

        public static IIntentInterceptor FromOriginKeyCondition(IIntentInterceptor intentInterceptor, string currentKey)
        {
            return new ConditionalIntentInterceptor(intentInterceptor, currentKey, string.Empty);
        }

        public static IIntentInterceptor ToDestinationKeyCondition(IIntentInterceptor intentInterceptor, string destinationKey)
        {
            return new ConditionalIntentInterceptor(intentInterceptor, string.Empty, destinationKey);
        }

        private static bool IsNotSetOrEqual(string condition, string actual)
        {
            if (string.IsNullOrEmpty(condition)) return true;
            return condition == actual;
        }
    }
}
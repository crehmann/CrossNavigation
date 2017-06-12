using Android.Content;

namespace CrossNavigation.Droid
{
    public interface IIntentInterceptor
    {
        Intent OnIntent(Intent intent, string currentPageKey, string destinationPageKey);
    }
}
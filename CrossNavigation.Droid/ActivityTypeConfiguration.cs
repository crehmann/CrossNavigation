using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using CrossNavigation.Common;

namespace CrossNavigation.Droid
{
    internal class ActivityTypeConfiguration
    {
        private readonly Dictionary<string, Type> _activityTypesByKey = new Dictionary<string, Type>();
        private readonly Context _context;
        private readonly ISerializer _serializer;

        public ActivityTypeConfiguration(Context context, ISerializer serializer)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _context = context;
            _serializer = serializer;
        }

        public async Task<Intent> CreateIntent(string pageKey, object parameter)
        {
            if (string.IsNullOrEmpty(pageKey)) throw new ArgumentException("Parameter must not be null or empty.", nameof(pageKey));
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));

            var activityType = GetActivityTypeForKey(pageKey);
            var intent = new Intent(_context, activityType);

            var serializedParameter = await _serializer.SerializeAsync(parameter).ConfigureAwait(false);
            intent.PutExtra(Constants.ParameterKeyName, serializedParameter);

            return intent;
        }

        public Intent CreateIntent(string pageKey)
        {
            if (string.IsNullOrEmpty(pageKey)) throw new ArgumentException("Parameter must not be null or empty.", nameof(pageKey));
            var activityType = GetActivityTypeForKey(pageKey);
            return new Intent(_context, activityType);
        }

        public async Task<T> GetParameterFromIntent<T>(Intent intent)
        {
            if (intent == null) throw new ArgumentNullException(nameof(intent), @"This method must be called with a valid Activity intent");
            var serializedObject = intent.GetStringExtra(Constants.ParameterKeyName);
            if (serializedObject == null) return default(T);
            var deserializedObject = await _serializer.DeserializeAsync<T>(serializedObject).ConfigureAwait(false);
            return deserializedObject;
        }

        public Type GetActivityTypeForKey(string pageKey)
        {
            if (string.IsNullOrEmpty(pageKey)) throw new ArgumentException("Parameter must not be null or empty.", nameof(pageKey));

            lock (_activityTypesByKey)
            {
                if (!_activityTypesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to register the page key with an activity type?", nameof(pageKey));
                }

                return _activityTypesByKey[pageKey];
            }
        }

        public void RegisterActivityType(Type activityType, string pageKey)
        {
            if (activityType == null) throw new ArgumentNullException(nameof(activityType));
            if (string.IsNullOrEmpty(pageKey)) throw new ArgumentException("Parameter must not be null or empty.", nameof(pageKey));

            lock (_activityTypesByKey)
            {
                if (_activityTypesByKey.ContainsKey(pageKey))
                {
                    _activityTypesByKey[pageKey] = activityType;
                }
                else
                {
                    _activityTypesByKey.Add(pageKey, activityType);
                }
            }
        }
    }
}
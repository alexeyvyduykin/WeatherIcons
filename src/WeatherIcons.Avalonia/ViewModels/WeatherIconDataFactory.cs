using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using WeatherIcons.Avalonia.Enums;
using WeatherIcons.Avalonia.Extensions;

namespace WeatherIcons.Avalonia.ViewModels
{
    internal static class WeatherIconDataFactory
    {
        private static readonly Dictionary<WeatherIconKey, IList<Geometry?>> _cache = new();
        private static readonly Dictionary<ThemedWeatherKey, IList<(WeatherObjectType, Geometry?)>> _themedCache = new();

        public static IList<Geometry?> Get(WeatherIconKey key)
        {
            var res = _cache.ContainsKey(key);

            if (res == true)
            {
                return _cache[key];
            }

            var style = CreateStyle("avares://WeatherIcons.Avalonia/App.xaml");

            style.TryGetResource($"{key}", out var value);

            if (value != null)
            {
                _cache.Add(key, new List<Geometry?>() { (Geometry?)value, null, null, null });
                return _cache[key];
            }

            style.TryGetResource($"{key}_1", out var value1);
            style.TryGetResource($"{key}_2", out var value2);
            style.TryGetResource($"{key}_3", out var value3);
            style.TryGetResource($"{key}_4", out var value4);

            _cache.Add(key, new List<Geometry?>()
            {
                (Geometry?)value1,
                (Geometry?)value2,
                (Geometry?)value3,
                (Geometry?)value4
            });

            return _cache[key];
        }

        public static IList<(WeatherObjectType, Geometry?)> Get(ThemedWeatherKey key)
        {
            var res = _themedCache.ContainsKey(key);

            if (res == true)
            {
                return _themedCache[key];
            }

            var style = CreateStyle("avares://WeatherIcons.Avalonia/App.xaml");

            style.TryGetResource($"{key}Keys", out var value);

            if (value == null || value is not string str)
            {
                throw new Exception();
            }

            var types = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var list = new List<(WeatherObjectType, Geometry?)>();

            for (int i = 0; i < types.Length; i++)
            {
                if (Enum.TryParse<WeatherObjectType>(types[i].Trim().FirstCharToUpper(), out var type) == true)
                {
                    style.TryGetResource($"{key}_{i + 1}", out var valueRes);

                    list.Add((type, (Geometry?)valueRes));
                }
            }

            _themedCache.Add(key, list);

            return _themedCache[key];
        }

        private static StyleInclude CreateStyle(string url)
        {
            var self = new Uri("resm:Styles?assembly=GisIocns.Avalonia");

            return new StyleInclude(self)
            {
                Source = new Uri(url)
            };
        }
    }
}

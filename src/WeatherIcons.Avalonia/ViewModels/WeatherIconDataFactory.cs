using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherIcons.Avalonia.Enums;
using WeatherIcons.Avalonia.Extensions;

namespace WeatherIcons.Avalonia.ViewModels
{
    internal static class WeatherIconDataFactory
    {
        private static readonly Dictionary<WeatherIconKey, IList<Geometry?>> _cache = new();
        private static readonly Dictionary<ThemedWeatherKey, IList<(WeatherObjectType, Geometry?)>> _themedCache = new();
        private static readonly Dictionary<UnitSpeedType, IList<double>> _units = new()
        {
            {
                UnitSpeedType.Knot,
                new List<double>()
                {
                    0, 3, 6, 10, 16, 21, 27, 33, 40, 47, 55, 63
                }
            },
            {
                UnitSpeedType.MilesPerHour,
                new List<double>()
                {
                    0, 3, 7, 12, 18, 24, 31, 38, 46, 54, 63, 72
                }
            },
            {
                UnitSpeedType.KilometresPerHour,
                new List<double>()
                {
                    1, 5, 11, 19, 28, 38, 49, 61, 74, 88, 102, 117
                }
            },
            {
                UnitSpeedType.MetrePerSecond,
                new List<double>()
                {
                    0.4, 1.5, 3.3, 5.5, 7.9, 10.7, 13.8, 17.1, 20.7, 24.4, 28.4, 32.6
                }
            },
        };

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

        public static IList<Geometry?> GetBeaufortScale(int scale)
        {
            var sc = Math.Max(Math.Min(scale, 12), 0);

            var key = (WeatherIconKey)Enum.Parse(typeof(WeatherIconKey), $"WindBeaufort{sc}");

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

        public static int GetBeaufortScale(double speed, UnitSpeedType unit)
        {
            double sp = (unit != UnitSpeedType.MetrePerSecond) ? Math.Floor(speed) : Math.Floor(speed * 10) / 10;

            return _units[unit].Where(s => s < sp).Count();
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

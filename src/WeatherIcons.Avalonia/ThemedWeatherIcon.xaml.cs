using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherIcons.Avalonia.Enums;
using WeatherIcons.Avalonia.ViewModels;

namespace WeatherIcons.Avalonia
{
    public class ThemedWeatherIcon : TemplatedControl
    {
        private IList<PathItem> _pathItems = new List<PathItem>();

        public ThemedWeatherIcon()
        {
            KeyProperty.Changed.Subscribe(_ => UpdateData());
        }

        public static readonly AvaloniaProperty<IBrush> CloudColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(CloudColor), defaultValue: Brushes.White);

        public IBrush CloudColor
        {
            get => (IBrush)GetValue(CloudColorProperty)!;
            set => SetValue(CloudColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> SunColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(SunColor), defaultValue: Brushes.Yellow);

        public IBrush SunColor
        {
            get => (IBrush)GetValue(SunColorProperty)!;
            set => SetValue(SunColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> MoonColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(MoonColor), defaultValue: Brushes.Violet);

        public IBrush MoonColor
        {
            get => (IBrush)GetValue(MoonColorProperty)!;
            set => SetValue(MoonColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> RainColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(RainColor), defaultValue: Brushes.LightSeaGreen);

        public IBrush RainColor
        {
            get => (IBrush)GetValue(RainColorProperty)!;
            set => SetValue(RainColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> HailColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(HailColor), defaultValue: Brushes.Aqua);

        public IBrush HailColor
        {
            get => (IBrush)GetValue(HailColorProperty)!;
            set => SetValue(HailColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> SnowColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(SnowColor), defaultValue: Brushes.LightBlue);

        public IBrush SnowColor
        {
            get => (IBrush)GetValue(SnowColorProperty)!;
            set => SetValue(SnowColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> FogColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(FogColor), defaultValue: Brushes.Orange);

        public IBrush FogColor
        {
            get => (IBrush)GetValue(FogColorProperty)!;
            set => SetValue(FogColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> WindColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(WindColor), defaultValue: Brushes.LightGreen);

        public IBrush WindColor
        {
            get => (IBrush)GetValue(WindColorProperty)!;
            set => SetValue(WindColorProperty, value);
        }

        public static readonly AvaloniaProperty<IBrush> LightningColorProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, IBrush>(nameof(LightningColor), defaultValue: Brushes.OrangeRed);

        public IBrush LightningColor
        {
            get => (IBrush)GetValue(LightningColorProperty)!;
            set => SetValue(LightningColorProperty, value);
        }

        public static readonly AvaloniaProperty<IList<PathItem>> PathItemsProperty =
            AvaloniaProperty.RegisterDirect<ThemedWeatherIcon, IList<PathItem>>(nameof(PathItems), icon => icon.PathItems);

        public IList<PathItem> PathItems
        {
            get => _pathItems;
            private set => SetAndRaise(PathItemsProperty, ref _pathItems, value);
        }

        public static readonly AvaloniaProperty<ThemedWeatherKey> KeyProperty =
            AvaloniaProperty.Register<ThemedWeatherIcon, ThemedWeatherKey>(nameof(Key), defaultValue: ThemedWeatherKey.Cloud);

        public ThemedWeatherKey Key
        {
            get => (ThemedWeatherKey)GetValue(KeyProperty)!;
            set => SetValue(KeyProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            var res = WeatherIconDataFactory.Get(Key);

            var list = res.Select(s => new PathItem()
            {
                Color = ConvertToColor(s.Item1),
                Data = s.Item2,
            });

            PathItems = new List<PathItem>(list);
        }

        private IBrush ConvertToColor(WeatherObjectType type)
        {
            return type switch
            {
                WeatherObjectType.Cloud => CloudColor,
                WeatherObjectType.Sun => SunColor,
                WeatherObjectType.Moon => MoonColor,
                WeatherObjectType.Rain => RainColor,
                WeatherObjectType.Hail => HailColor,
                WeatherObjectType.Snow => SnowColor,
                WeatherObjectType.Fog => FogColor,
                WeatherObjectType.Wind => WindColor,
                WeatherObjectType.Lightning => LightningColor,
                _ => throw new Exception(),
            };
        }
    }
}

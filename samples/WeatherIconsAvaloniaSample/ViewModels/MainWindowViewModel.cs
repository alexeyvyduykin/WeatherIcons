using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherIcons.Avalonia.Enums;

namespace WeatherIconsAvaloniaSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            var arr = Enum.GetValues(typeof(ThemedWeatherKey));

            ThemedWeatherKeys = new List<ThemedWeatherKey>(arr.Cast<ThemedWeatherKey>());

            SelectedThemedWeatherKey = ThemedWeatherKeys.FirstOrDefault();

            SelectedTime = DateTime.Now.TimeOfDay;

            WindDirectionAngle = 90.0;

            WindCardinalDirections = new List<WindCardinalDirection>(Enum.GetValues(typeof(WindCardinalDirection)).Cast<WindCardinalDirection>());

            WindDirections = new List<WindDirection>(Enum.GetValues(typeof(WindDirection)).Cast<WindDirection>());

            SelectedWindDirection = WindDirections.FirstOrDefault();

            UnitSpeedTypes = new List<UnitSpeedType>(Enum.GetValues(typeof(UnitSpeedType)).Cast<UnitSpeedType>());

            SelectedUnitSpeedType = UnitSpeedType.MetrePerSecond;
        }

        public List<ThemedWeatherKey> ThemedWeatherKeys { get; set; }

        [Reactive]
        public ThemedWeatherKey? SelectedThemedWeatherKey { get; set; }

        [Reactive]
        public TimeSpan SelectedTime { get; set; }

        [Reactive]
        public double WindDirectionAngle { get; set; }

        [Reactive]
        public List<WindCardinalDirection> WindCardinalDirections { get; set; }

        [Reactive]
        public WindCardinalDirection? SelectedWindCardinalDirection { get; set; }

        [Reactive]
        public List<WindDirection> WindDirections { get; set; }

        [Reactive]
        public WindDirection SelectedWindDirection { get; set; }

        [Reactive]
        public int BeaufortWindScale { get; set; }

        [Reactive]
        public double BeaufortWindSpeed { get; set; }

        [Reactive]
        public List<UnitSpeedType> UnitSpeedTypes { get; set; }

        [Reactive]
        public UnitSpeedType SelectedUnitSpeedType { get; set; }
    }
}

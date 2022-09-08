using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using WeatherIcons.Avalonia.Enums;

namespace WeatherIcons.Avalonia
{
    public class TimeIcon : TemplatedControl
    {
        private double _hourAngle;
        private double _minuteAngle;

        public static readonly StyledProperty<IBrush?> PrimaryProperty =
            AvaloniaProperty.Register<TimeIcon, IBrush?>(nameof(Primary), defaultValue: null);

        public IBrush? Primary
        {
            get { return GetValue(PrimaryProperty); }
            set { SetValue(PrimaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> SecondaryProperty =
            AvaloniaProperty.Register<TimeIcon, IBrush?>(nameof(Secondary), defaultValue: null);

        public IBrush? Secondary
        {
            get { return GetValue(SecondaryProperty); }
            set { SetValue(SecondaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> TertiaryProperty =
            AvaloniaProperty.Register<TimeIcon, IBrush?>(nameof(Tertiary), defaultValue: null);

        public IBrush? Tertiary
        {
            get { return GetValue(TertiaryProperty); }
            set { SetValue(TertiaryProperty, value); }
        }

        public static readonly AvaloniaProperty<TimeSpan> TimeProperty =
            AvaloniaProperty.Register<TimeIcon, TimeSpan>(nameof(Time), defaultValue: TimeSpan.Zero);

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty)!;
            set => SetValue(TimeProperty, value);
        }

        public static readonly AvaloniaProperty<double> HourAngleProperty =
            AvaloniaProperty.RegisterDirect<TimeIcon, double>(nameof(HourAngle), icon => icon.HourAngle);

        public double HourAngle
        {
            get => _hourAngle;
            private set => SetAndRaise(HourAngleProperty, ref _hourAngle, value);
        }

        public static readonly AvaloniaProperty<double> MinuteAngleProperty =
            AvaloniaProperty.RegisterDirect<TimeIcon, double>(nameof(MinuteAngle), icon => icon.MinuteAngle);

        public double MinuteAngle
        {
            get => _minuteAngle;
            private set => SetAndRaise(MinuteAngleProperty, ref _minuteAngle, value);
        }

        public static readonly AvaloniaProperty<WindDirection> DirectionProperty =
            AvaloniaProperty.Register<TimeIcon, WindDirection>(nameof(Direction), defaultValue: WindDirection.Towards);

        public WindDirection Direction
        {
            get => (WindDirection)GetValue(DirectionProperty)!;
            set => SetValue(DirectionProperty, value);
        }

        public static readonly AvaloniaProperty<WindCardinalDirection?> CardinalDirectionProperty =
            AvaloniaProperty.Register<TimeIcon, WindCardinalDirection?>(nameof(CardinalDirection));

        public WindCardinalDirection? CardinalDirection
        {
            get => (WindCardinalDirection?)GetValue(CardinalDirectionProperty);
            set => SetValue(CardinalDirectionProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            var hours = Time.Hours % 12;
            var minutes = Time.Minutes % 60;

            MinuteAngle = 360.0 * minutes / 60.0;
            HourAngle = 360.0 * hours / 12 + 360.0 * minutes / (60.0 * 12);

            Primary ??= Foreground;
            Secondary ??= Primary;
            Tertiary ??= Secondary;
        }
    }
}

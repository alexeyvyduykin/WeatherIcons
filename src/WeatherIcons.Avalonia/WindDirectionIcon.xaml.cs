using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using WeatherIcons.Avalonia.Enums;

namespace WeatherIcons.Avalonia
{
    public class WindDirectionIcon : TemplatedControl
    {
        public static readonly StyledProperty<IBrush?> PrimaryProperty =
            AvaloniaProperty.Register<WindDirectionIcon, IBrush?>(nameof(Primary), defaultValue: null);

        public IBrush? Primary
        {
            get { return GetValue(PrimaryProperty); }
            set { SetValue(PrimaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> SecondaryProperty =
            AvaloniaProperty.Register<WindDirectionIcon, IBrush?>(nameof(Secondary), defaultValue: null);

        public IBrush? Secondary
        {
            get { return GetValue(SecondaryProperty); }
            set { SetValue(SecondaryProperty, value); }
        }


        public static readonly AvaloniaProperty<double> AngleProperty =
            AvaloniaProperty.Register<WindDirectionIcon, double>(nameof(Angle), defaultValue: 0.0);

        public double Angle
        {
            get => (double)GetValue(AngleProperty)!;
            set => SetValue(AngleProperty, value);
        }

        public static readonly AvaloniaProperty<WindDirection> DirectionProperty =
            AvaloniaProperty.Register<WindDirectionIcon, WindDirection>(nameof(Direction), defaultValue: WindDirection.Towards);

        public WindDirection Direction
        {
            get => (WindDirection)GetValue(DirectionProperty)!;
            set => SetValue(DirectionProperty, value);
        }

        public static readonly AvaloniaProperty<WindCardinalDirection?> CardinalDirectionProperty =
            AvaloniaProperty.Register<WindDirectionIcon, WindCardinalDirection?>(nameof(CardinalDirection));

        public WindCardinalDirection? CardinalDirection
        {
            get => (WindCardinalDirection?)GetValue(CardinalDirectionProperty);
            set => SetValue(CardinalDirectionProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            if (CardinalDirection != null)
            {
                Angle = (int)CardinalDirection;
            }

            if (Direction == WindDirection.From)
            {
                Angle += 180.0;

                if (Angle >= 360.0)
                {
                    Angle -= 360.0;
                }
            }

            Primary ??= Foreground;
            Secondary ??= Primary;
        }
    }
}

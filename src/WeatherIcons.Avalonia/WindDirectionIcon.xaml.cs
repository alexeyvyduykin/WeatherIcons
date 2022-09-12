using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using WeatherIcons.Avalonia.Enums;

namespace WeatherIcons.Avalonia
{
    public class WindDirectionIcon : TemplatedControl
    {
        private ITransform? _angleTransform;

        public WindDirectionIcon()
        {
            AngleProperty.Changed.Subscribe(_ => ChangedAngle());
            CardinalDirectionProperty.Changed.Subscribe(_ => ChangedCardinalDirection());
            DirectionProperty.Changed.Subscribe(_ => ChangedAngle());
        }

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

        public static readonly AvaloniaProperty<ITransform?> AngleTransformProperty =
            AvaloniaProperty.RegisterDirect<WindDirectionIcon, ITransform?>(nameof(AngleTransform), icon => icon.AngleTransform);

        public ITransform? AngleTransform
        {
            get => _angleTransform;
            private set => SetAndRaise(AngleTransformProperty, ref _angleTransform, value);
        }

        private void ChangedCardinalDirection()
        {
            if (CardinalDirection != null)
            {
                Angle = (int)CardinalDirection;
            }
        }

        private void ChangedAngle()
        {
            var angle = Angle;

            if (Direction == WindDirection.From)
            {
                angle += 180.0;

                if (angle >= 360.0)
                {
                    angle -= 360.0;
                }
            }

            AngleTransform = new RotateTransform(angle);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            ChangedCardinalDirection();
            ChangedAngle();

            Primary ??= Foreground;
            Secondary ??= Primary;
        }
    }
}

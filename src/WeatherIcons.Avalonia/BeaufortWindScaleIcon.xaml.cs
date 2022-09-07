using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using WeatherIcons.Avalonia.Enums;
using WeatherIcons.Avalonia.ViewModels;

namespace WeatherIcons.Avalonia
{
    public class BeaufortWindScaleIcon : TemplatedControl
    {
        private Geometry? _data1;
        private Geometry? _data2;

        public static readonly AvaloniaProperty<Geometry?> DataProperty1 =
            AvaloniaProperty.RegisterDirect<BeaufortWindScaleIcon, Geometry?>(nameof(Data1), icon => icon.Data1);

        public Geometry? Data1
        {
            get => _data1;
            private set => SetAndRaise(DataProperty1, ref _data1, value);
        }

        public static readonly AvaloniaProperty<Geometry?> DataProperty2 =
            AvaloniaProperty.RegisterDirect<BeaufortWindScaleIcon, Geometry?>(nameof(Data2), icon => icon.Data2);

        public Geometry? Data2
        {
            get => _data2;
            private set => SetAndRaise(DataProperty2, ref _data2, value);
        }

        public static readonly StyledProperty<IBrush?> PrimaryProperty =
            AvaloniaProperty.Register<BeaufortWindScaleIcon, IBrush?>(nameof(Primary), defaultValue: null);

        public IBrush? Primary
        {
            get { return GetValue(PrimaryProperty); }
            set { SetValue(PrimaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> SecondaryProperty =
            AvaloniaProperty.Register<BeaufortWindScaleIcon, IBrush?>(nameof(Secondary), defaultValue: null);

        public IBrush? Secondary
        {
            get { return GetValue(SecondaryProperty); }
            set { SetValue(SecondaryProperty, value); }
        }

        public static readonly AvaloniaProperty<int> ScaleProperty =
            AvaloniaProperty.Register<BeaufortWindScaleIcon, int>(nameof(Scale), defaultValue: 0);

        public int Scale
        {
            get => (int)GetValue(ScaleProperty)!;
            set => SetValue(ScaleProperty, value);
        }

        public static readonly AvaloniaProperty<double?> SpeedProperty =
            AvaloniaProperty.Register<BeaufortWindScaleIcon, double?>(nameof(Speed));

        public double? Speed
        {
            get => (double?)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        public static readonly AvaloniaProperty<UnitSpeedType> UnitProperty =
            AvaloniaProperty.Register<BeaufortWindScaleIcon, UnitSpeedType>(nameof(Unit), defaultValue: UnitSpeedType.MetrePerSecond);

        public UnitSpeedType Unit
        {
            get => (UnitSpeedType)GetValue(UnitProperty)!;
            set => SetValue(UnitProperty, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            if (Speed != null)
            {
                Scale = WeatherIconDataFactory.GetBeaufortScale((double)Speed, Unit);
            }

            var list = WeatherIconDataFactory.GetBeaufortScale(Scale);

            Data1 = list[0];
            Data2 = list[1];

            Primary ??= Foreground;
            Secondary ??= Primary;
        }
    }
}

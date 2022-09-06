using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using WeatherIcons.Avalonia.Enums;
using WeatherIcons.Avalonia.ViewModels;

namespace WeatherIcons.Avalonia
{
    public class WeatherIcon : TemplatedControl
    {
        private Geometry? _data1;
        private Geometry? _data2;
        private Geometry? _data3;

        public static readonly AvaloniaProperty<Geometry?> DataProperty1 =
            AvaloniaProperty.RegisterDirect<WeatherIcon, Geometry?>(nameof(Data1), icon => icon.Data1);

        public Geometry? Data1
        {
            get => _data1;
            private set => SetAndRaise(DataProperty1, ref _data1, value);
        }

        public static readonly AvaloniaProperty<Geometry?> DataProperty2 =
            AvaloniaProperty.RegisterDirect<WeatherIcon, Geometry?>(nameof(Data2), icon => icon.Data2);

        public Geometry? Data2
        {
            get => _data2;
            private set => SetAndRaise(DataProperty2, ref _data2, value);
        }

        public static readonly AvaloniaProperty<Geometry?> DataProperty3 =
            AvaloniaProperty.RegisterDirect<WeatherIcon, Geometry?>(nameof(Data3), icon => icon.Data3);

        public Geometry? Data3
        {
            get => _data3;
            private set => SetAndRaise(DataProperty3, ref _data3, value);
        }

        public static readonly AvaloniaProperty<WeatherIconKey> KeyProperty =
            AvaloniaProperty.Register<WeatherIcon, WeatherIconKey>(nameof(Key), defaultValue: WeatherIconKey.Na);

        public WeatherIconKey Key
        {
            get => (WeatherIconKey)GetValue(KeyProperty)!;
            set => SetValue(KeyProperty, value);
        }

        public static readonly StyledProperty<IBrush?> PrimaryProperty =
            AvaloniaProperty.Register<Shape, IBrush?>(nameof(Primary), defaultValue: null);

        public IBrush? Primary
        {
            get { return GetValue(PrimaryProperty); }
            set { SetValue(PrimaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> SecondaryProperty =
            AvaloniaProperty.Register<Shape, IBrush?>(nameof(Secondary), defaultValue: null);

        public IBrush? Secondary
        {
            get { return GetValue(SecondaryProperty); }
            set { SetValue(SecondaryProperty, value); }
        }

        public static readonly StyledProperty<IBrush?> TertiaryProperty =
            AvaloniaProperty.Register<Shape, IBrush?>(nameof(Tertiary), defaultValue: null);

        public IBrush? Tertiary
        {
            get { return GetValue(TertiaryProperty); }
            set { SetValue(TertiaryProperty, value); }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateData();
        }

        private void UpdateData()
        {
            var list = WeatherIconDataFactory.Get(Key);

            Data1 = list[0];
            Data2 = list[1];
            Data3 = list[2];

            Primary ??= Foreground;
            Secondary ??= Primary;
            Tertiary ??= Secondary;
        }
    }
}

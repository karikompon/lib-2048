using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace _2048ForWpf;

public partial class DefaultBlock : BlockBase, INotifyPropertyChanged
{
    private Brush? _color;
    private int? _number;
    private int _colorCode;
    private Brush[] _colors;

    public DefaultBlock(ICustomizer customizer)
    {
        InitializeComponent();
        DataContext = this;

        _colors = customizer.BlockColors;
        Padding = customizer.BlockPadding;
        Margin = customizer.BlockMargin;
        BorderThickness = customizer.BlockBorderThickness;
        BorderBrush = customizer.BlockBorderBrush;
        FontSize = customizer.BlockFontSize;
        Foreground = customizer.BlockNumberColor;

        ColorCode = _colors.Length - 1;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public override Brush? Color
    {
        get { return _color; }
        set { _color = value; OnPropertyChanged(); }
    }

    public override int? Number
    {
        get { return _number; }
        set
        {
            if (value != null) ColorCode++;
            _number = value;

            OnPropertyChanged();
        }
    }

    public override int ColorCode
    {
        get { return _colorCode; }
        set
        {
            if (value > _colors.Length - 1) _colorCode = 0;
            else _colorCode = value;
            Color = _colors[_colorCode];
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

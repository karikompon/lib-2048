using System.Windows;
using System.Windows.Media;

namespace _2048ForWpf;

public interface ICustomizer
{
    public Brush BackgroundColor { get; set; }
    public Brush[] BlockColors { get; set; }
    public Thickness BlockPadding { get; set; }
    public Thickness BlockMargin { get; set; }
    public Thickness BlockBorderThickness { get; set; }
    public Brush BlockBorderBrush { get; set; }
    public double BlockFontSize { get; set; }
    public Brush BlockNumberColor { get; set; }
}

internal class Customizer : ICustomizer
{
    public Brush BackgroundColor { get; set; } = Brushes.Black;
    public Brush[] BlockColors { get; set; } =
    {
        Brushes.RoyalBlue,
        Brushes.SlateBlue,
        Brushes.MediumSlateBlue,
        Brushes.MediumPurple,
        Brushes.MediumOrchid,
        Brushes.Orchid,
        Brushes.Violet,
        Brushes.HotPink,
        Brushes.DeepPink,
        Brushes.MediumVioletRed,
    };
    public Thickness BlockPadding { get; set; } = new Thickness(0);
    public Thickness BlockMargin { get; set; } = new Thickness(0);
    public Thickness BlockBorderThickness { get; set; } = new Thickness(0.5);
    public Brush BlockBorderBrush { get; set; } = Brushes.White;
    public double BlockFontSize { get; set; } = 15;
    public Brush BlockNumberColor { get; set; } = Brushes.White;
}

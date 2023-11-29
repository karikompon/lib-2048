using System.Windows.Controls;
using System.Windows.Media;

namespace _2048ForWpf;

public abstract class BlockBase : UserControl
{
    public abstract Brush? Color { get; set; }

    public abstract int ColorCode { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public abstract int? Number { get; set; }

    public bool Combined { get; set; }
}

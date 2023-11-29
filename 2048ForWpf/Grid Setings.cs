namespace _2048ForWpf;

public interface IGridSettings
{
    public int BlockSize { get; set; }
    public int VerticalBlockCount { get; set; }
    public int HorizontalBlockCount { get; set; }
}

public class GridSettings : IGridSettings
{
    public int BlockSize { get; set; } = 50;
    public int VerticalBlockCount { get; set; } = 5;
    public int HorizontalBlockCount { get; set; } = 5;
}
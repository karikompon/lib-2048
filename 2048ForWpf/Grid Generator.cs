using System.Windows.Controls;
using System.Windows.Media;

namespace _2048ForWpf;

public interface IGridGenerator
{
    Grid GenerateGrid();
}

internal class GridGenerator : IGridGenerator
{
    private readonly IGridSettings _gridSettings;
    private readonly IBlockPositionWriter _blockPositionWriter;
    private readonly ICustomizer _customizer;

    public GridGenerator(IGridSettings gridSettings, IBlockPositionWriter blockPositionWriter, ICustomizer customizer)
    {
        _gridSettings = gridSettings;
        _blockPositionWriter = blockPositionWriter;
        _customizer = customizer;
    }

    public Grid GenerateGrid()
    {
        Grid grid = new Grid();

        grid.Height = _gridSettings.VerticalBlockCount * _gridSettings.BlockSize;
        grid.Width = _gridSettings.HorizontalBlockCount * _gridSettings.BlockSize;

        for (int i = 0; i < _gridSettings.VerticalBlockCount; i++)
        {
            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);
        }
        for (int i = 0; i < _gridSettings.HorizontalBlockCount; i++)
        {
            ColumnDefinition column = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column);
        }

        grid.Background = _customizer.BackgroundColor;

        for (int row = 0; row < _gridSettings.VerticalBlockCount; row++)
        {
            for (int column = 0; column < _gridSettings.HorizontalBlockCount; column++)
            {
                BlockBase block = new DefaultBlock(_customizer)
                {
                    Number = null,
                    Row = row,
                    Column = column,
                    Color = Brushes.Transparent,
                };

                grid.Children.Add(block);
                _blockPositionWriter.SetBlockPosition(block, row, column);
            }
        }

        return grid;
    }
}

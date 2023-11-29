using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _2048ForWpf;

public partial class GameWindow : Window
{
    private readonly Grid _grid;
    private readonly IGridSettings _gridSettings;
    private readonly IBlockPositionWriter _blockPositionWriter;
    private readonly IGridGenerator _gridGenerator;
    private readonly IBlocksCollection _blocks;
    private readonly IGameOutcomesHandler _gameOutcomesHandler;
    private readonly ICustomizer _customizer;
    private readonly IUndoHandler _undoHandler;

    public GameWindow(IGridSettings gridSettings, IBlockPositionWriter blockPositionWriter, IGridGenerator gridGenerator, IBlocksCollection blocks, IGameOutcomesHandler gameOutcomesHandler, ICustomizer customizer, IUndoHandler undoHandler)
    {
        InitializeComponent();

        _gridSettings = gridSettings;
        _blockPositionWriter = blockPositionWriter;
        _gridGenerator = gridGenerator;
        _blocks = blocks;
        _gameOutcomesHandler = gameOutcomesHandler;
        _customizer = customizer;
        _undoHandler = undoHandler;

        _grid = _gridGenerator.GenerateGrid();
        _containerGrid.Children.Add(_grid);
        Grid.SetRow(_grid, 1);

        CreateBlock();
    }

    private void CreateBlock()
    {
        BlockBase block = new DefaultBlock(_customizer) { Number = 2 };
        _blocks.Add(block);
        _grid.Children.Add(block);
        _blockPositionWriter.SetRandomBlockPosition(block);
    }

    private void SwipeLine(BlockBase[] blocksInLine, int rowOffset, int columnOffset)
    {
        for (int i = 0; i < blocksInLine.Length; i++)
        {
            while (_blockPositionWriter.CheckPositionAvailability(blocksInLine[i].Row + rowOffset, blocksInLine[i].Column + columnOffset))
            {
                _blockPositionWriter.SetBlockPosition(blocksInLine[i], blocksInLine[i].Row + rowOffset, blocksInLine[i].Column + columnOffset);
            }

            if (i > 0 && blocksInLine[i].Number == blocksInLine[i - 1].Number
                && !blocksInLine[i - 1].Combined)
            {
                blocksInLine[i].Number *= 2;
                _blockPositionWriter.SetBlockPosition(blocksInLine[i], blocksInLine[i].Row + rowOffset, blocksInLine[i].Column + columnOffset);
                blocksInLine[i].Combined = true;

                _blocks.Remove(blocksInLine[i - 1]);
                _grid.Children.Remove(blocksInLine[i - 1]);
                i--;
            }
        }

        foreach (BlockBase block in blocksInLine)
            block.Combined = false;
    }

    private void SwipeBlocks_Up()
    {
        for (int column = 0; column < _gridSettings.HorizontalBlockCount; column++)
        {
            var blocksInColumn = _blocks
                .Where(block => block.Column == column)
                .OrderBy(block => block.Row)
                .ToArray();

            SwipeLine(blocksInColumn, rowOffset: -1, columnOffset: 0);
        }
        _gameOutcomesHandler.TryWin();
        CreateBlock();
    }

    private void SwipeBlocks_Down()
    {
        for (int column = 0; column < _gridSettings.HorizontalBlockCount; column++)
        {
            var blocksInColumn = _blocks
                .Where(block => block.Column == column)
                .OrderByDescending(block => block.Row)
                .ToArray();

            SwipeLine(blocksInColumn, rowOffset: +1, columnOffset: 0);
        }
        _gameOutcomesHandler.TryWin();
        CreateBlock();
    }

    private void SwipeBlocks_Left()
    {
        for (int row = 0; row < _gridSettings.HorizontalBlockCount; row++)
        {
            var blocksInRow = _blocks
                .Where(block => block.Row == row)
                .OrderBy(block => block.Column)
                .ToArray();

            SwipeLine(blocksInRow, rowOffset: 0, columnOffset: -1);
        }
        _gameOutcomesHandler.TryWin();
        CreateBlock();
    }

    private void SwipeBlocks_Right()
    {
        for (int row = 0; row < _gridSettings.HorizontalBlockCount; row++)
        {
            var blocksInRow = _blocks
                .Where(block => block.Row == row)
                .OrderByDescending(block => block.Column)
                .ToArray();

            SwipeLine(blocksInRow, rowOffset: 0, columnOffset: +1);
        }
        _gameOutcomesHandler.TryWin();
        CreateBlock();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
                _undoHandler.GetReadyForUndo();
                SwipeBlocks_Up();
                break;
            case Key.Down:
                _undoHandler.GetReadyForUndo();
                SwipeBlocks_Down();
                break;
            case Key.Left:
                _undoHandler.GetReadyForUndo();
                SwipeBlocks_Left();
                break;
            case Key.Right:
                _undoHandler.GetReadyForUndo();
                SwipeBlocks_Right();
                break;
            case Key.Back:
                _undoHandler.Undo(_grid);
                break;
        }
    }

    private void DragTheWindow(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void CloseTheWindow(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Are you sure you want to quit?", "", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
            Close();
    }

    private void MinimizeTheWindow(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}

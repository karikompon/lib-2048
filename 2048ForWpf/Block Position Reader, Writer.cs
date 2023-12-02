using System;
using System.Linq;
using System.Windows.Controls;

namespace _2048ForWpf;

public interface IBlockPositionReader
{
    bool CheckPositionAvailability(int row, int column);
}

public interface IBlockPositionWriter : IBlockPositionReader
{
    void SetBlockPosition(BlockBase block, int row, int column);

    void SetRandomBlockPosition(BlockBase block);
}

internal class BlockPositionWriter : IBlockPositionWriter
{
    private readonly Random _random;
    private readonly IBlockRepository _blocks;
    private readonly IGridSettings _gridSettings;
    private readonly IGameOutcomesHandler _gameOutcomesHandler;

    public BlockPositionWriter(IBlockRepository blocks, IGridSettings gridSettings, IGameOutcomesHandler gameOutcomesHandler)
    {
        _random = new Random();
        _blocks = blocks;
        _gridSettings = gridSettings;
        _gameOutcomesHandler = gameOutcomesHandler;
    }

    public bool CheckPositionAvailability(int row, int column)
    {
        return
            row >= 0 && row < _gridSettings.VerticalBlockCount &&
            column >= 0 && column < _gridSettings.HorizontalBlockCount &&
            !_blocks.Any(block => block.Row == row && block.Column == column);
    }

    public void SetBlockPosition(BlockBase block, int row, int column)
    {
        block.Row = row;
        block.Column = column;

        Grid.SetRow(block, row);
        Grid.SetColumn(block, column);
    }

    public void SetRandomBlockPosition(BlockBase block)
    {
        if (_gameOutcomesHandler.TryLose())
        {
            return;
        }

        int row = _random.Next(_gridSettings.VerticalBlockCount);
        int column = _random.Next(_gridSettings.HorizontalBlockCount);

        while (!CheckPositionAvailability(row, column))
        {
            row = _random.Next(_gridSettings.VerticalBlockCount);
            column = _random.Next(_gridSettings.HorizontalBlockCount);
        }

        SetBlockPosition(block, row, column);
    }
}

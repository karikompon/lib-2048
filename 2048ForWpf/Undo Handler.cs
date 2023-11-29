using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace _2048ForWpf;

public interface IUndoHandler
{
    void GetReadyForUndo();

    void Undo(Grid grid);
}

public class UndoHandler : IUndoHandler
{
    private readonly IBlockCollection _blocks;
    private readonly IBlockPositionWriter _blockPositionWriter;
    private readonly ICustomizer _customizer;

    private List<(int Row, int Column, int? Number, int ColorCode, Brush? Color)> _savedInfo;
    private bool UndoWasDone;

    public UndoHandler(IBlockCollection blocks, IBlockPositionWriter blockPositionWriter, ICustomizer customizer)
    {
        _blocks = blocks;
        _blockPositionWriter = blockPositionWriter;
        _customizer = customizer;

        _savedInfo = new();
    }

    public void GetReadyForUndo()
    {
        _savedInfo.Clear();

        foreach (var block in _blocks)
            _savedInfo.Add((block.Row, block.Column, block.Number, block.ColorCode, block.Color));

        UndoWasDone = false;
    }

    public void Undo(Grid grid)
    {
        if (UndoWasDone)
            return;

        foreach (var block in _blocks)
            grid.Children.Remove(block);
        _blocks.Clear();

        foreach (var info in _savedInfo)
        {
            BlockBase block = new DefaultBlock(_customizer)
            { Number = info.Number, ColorCode = info.ColorCode, Color = info.Color };
            _blocks.Add(block);
            grid.Children.Add(block);
            _blockPositionWriter.SetBlockPosition(block, info.Row, info.Column);
        }

        UndoWasDone = true;
    }
}
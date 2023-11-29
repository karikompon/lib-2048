using System.Collections;
using System.Collections.Generic;

namespace _2048ForWpf;

public interface IBlocksCollection : IEnumerable<BlockBase>
{
    void Add(BlockBase block);

    void Remove(BlockBase block);

    int Count();
}

public class BlocksCollection : IBlocksCollection
{
    private readonly List<BlockBase> _blocks;

    public BlocksCollection()
    {
        _blocks = new List<BlockBase>();
    }

    public void Add(BlockBase block)
    {
        _blocks.Add(block);
    }

    public void Remove(BlockBase block)
    {
        _blocks.Remove(block);
    }

    public int Count()
    {
        return _blocks.Count;
    }

    public IEnumerator<BlockBase> GetEnumerator()
    {
        return _blocks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
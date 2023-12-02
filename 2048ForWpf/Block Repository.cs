﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace _2048ForWpf;

public interface IBlockRepository : IEnumerable<BlockBase>
{
    void Add(BlockBase block);

    void Remove(BlockBase block);

    int Count();

    void Clear();
}

internal class BlockRepository : IBlockRepository
{
    private readonly List<BlockBase> _blocks;

    public BlockRepository()
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

    public void Clear()
    {
        _blocks.Clear();
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
using UnityEngine;
using System.Collections;
using System;

public class Wall : Block
{
    public Wall(GameObject g) : base(g)
    {
        if(g.tag != "Wall")
            throw new ArgumentException();
        blockType = BlockType.Wall;
    }
}

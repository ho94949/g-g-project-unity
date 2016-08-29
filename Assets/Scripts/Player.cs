using UnityEngine;
using System.Collections;
using System;

public class Player : Block
{
    public Player(GameObject g) : base(g)
    {
        if (g.tag != "Player")
            throw new ArgumentException();
        blockType = BlockType.Player;
    }
    
}

using UnityEngine;
using System.Collections;
using System;

public class Block
{
    protected const int scaleConstant = 10000;
    public int positionX
    {
        get;
        set;
    }
    public int positionY
    {
        get;
        set;
    }
    public int width
    {
        get;
        set;
    }
    public int height
    {
        get;
        set;
    }

    protected GameObject gameObject
    {
        get;
        set;
    }

    public enum BlockType
    {
        Void,
        Wall,
        Player,
        Box,
    };

    public BlockType blockType
    {
        get;
        set;
    }

    protected Block(GameObject g)
    {
        Vector3 gameObjectPosition = g.GetComponent<Transform>().position;
        Vector3 gameObjectSize = g.GetComponent<Renderer>().bounds.size;
        width = (int)(scaleConstant * gameObjectSize.x + 0.5);
        height = (int)(scaleConstant * gameObjectSize.y + 0.5);
        if(gameObjectPosition.x >= 0)
            positionX = (int)(scaleConstant * gameObjectPosition.x + 0.5) - width / 2;
        else
            positionX = (int)(scaleConstant * gameObjectPosition.x - 0.5) - width / 2;
        if (gameObjectPosition.y >= 0)
            positionY = (int)(scaleConstant * gameObjectPosition.y + 0.5) - width / 2;
        else
            positionY = (int)(scaleConstant * gameObjectPosition.y - 0.5) - width / 2;
        gameObject = g;
        Debug.Log(width);
        Debug.Log(height);
        Debug.Log(positionX);
        Debug.Log(positionY);
    }
    
    public void RawMoveX(int m)
    {
        positionX += m;
    }

    public void RawMoveY(int m)
    {
        positionY += m;
    }

    private int RealMovePositiveX(Block b, int distance)
    {

        return distance;


    }

    public int RealMoveX(Block b, int distance)
    {
        if (this == b)
            throw new ArgumentException();
        if (positionY + height <= b.positionY) return distance;
        if (b.positionY + b.height <= positionY) return distance;
        return distance;
    }

}

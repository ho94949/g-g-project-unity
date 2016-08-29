using UnityEngine;
using System.Collections.Generic;
using System;

public class PartRight : Part
{
    private static PartRight instance;
    public static PartRight Instance
    {
        get
        {
            if (instance == null)
                instance = new PartRight();
            return instance;
        }
    }

    private static List<Sprite> spriteList;
    private static Sprite standingSprite;

    private bool prevMove;
    private int spritePriority = 1000;

    private PartRight()
    {
        spriteList = new List<Sprite>();
        spriteList.Add(Resources.Load<Sprite>("robot_walking_01"));
        spriteList.Add(Resources.Load<Sprite>("robot_walking_02"));
        spriteList.Add(Resources.Load<Sprite>("robot_walking_03"));
        spriteList.Add(Resources.Load<Sprite>("robot_walking_04"));
        standingSprite = Resources.Load<Sprite>("robot_standing_01");
    }
    
    public override void MovePlayer(PlayerController pc)
    {
        float LRMove = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = pc.velocity;

        if (LRMove <= 1e-5)
        {
            if (velocity.x > 0)
                pc.velocity = new Vector2(0, velocity.y);

            if (prevMove)
                if (pc.spriteDisplayPriority < 500)
                {
                    pc.spriteDisplay = standingSprite;
                    pc.spriteDisplayPriority = 500;

                }

            prevMove = false;
            return;
        }

        prevMove = true;
        pc.velocity = new Vector2(LRMove * 3.0f, velocity.y);
        if (LRMove > 0.5f)

            if (pc.spriteDisplayPriority < spritePriority)
            {
                pc.spriteDisplay = spriteList[(pc.frame % 60) / 15];
                pc.spriteDisplayPriority = spritePriority;
            }
    }

    public override string Name()
    {
        return "PartRight";
    }

    public override string Type()
    {
        return "PartRight";
    }
    public override PartIndicator.PartType getEnum()
    {
        return PartIndicator.PartType.Right;
    }
}

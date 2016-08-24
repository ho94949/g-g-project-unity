using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public  class MoveRight : Part
{
    private static MoveRight instance;
    private static List<Sprite> SList;
    private static Sprite standing;
    private int spritePriority = 1000;
    private bool prevmove;
    private MoveRight()
    {
        SList = new List<Sprite>();
        SList.Add(Resources.Load<Sprite>("robot_walking_01"));
        SList.Add(Resources.Load<Sprite>("robot_walking_02"));
        SList.Add(Resources.Load<Sprite>("robot_walking_03"));
        SList.Add(Resources.Load<Sprite>("robot_walking_04"));
        standing = Resources.Load<Sprite>("robot_standing_01");
    }

    public static MoveRight Instance
    {
        get
        {
            if (instance == null)
                instance = new MoveRight();
            return instance;
        }
    }

    public override void MovePlayer(MoveitMoveit m)
    {
        float LRMove = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = m.GetComponent<Rigidbody2D>().velocity;
        if (LRMove <= 1e-5)
        {
           if (velocity.x > 0 )
                m.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity.y);
            if (prevmove)
                if (m.spriteDisplayPriority < 500)
                {
                    m.spriteDisplay = standing;
                    m.spriteDisplayPriority = 500;
                }
            prevmove = false;
            return;
        }
        prevmove = true;
        if (m.currentCollisionLeftDirection.Count != 0) return;
        m.GetComponent<Rigidbody2D>().velocity = new Vector2(LRMove * 3.0f, velocity.y);

        if(LRMove > 0.5f)

            if(m.spriteDisplayPriority < spritePriority)
            {
                m.spriteDisplay = SList[(m.frame %60) / 15];
                m.spriteDisplayPriority = spritePriority;

            }

    }
    public override string Name()
    {
        return "MoveRight";
    }
}

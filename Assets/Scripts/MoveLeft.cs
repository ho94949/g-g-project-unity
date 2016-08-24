using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveLeft : Part
{
    private static MoveLeft instance;
    private static List<Sprite> SList;
    private static Sprite standing;
    private int spritePriority = 1000;
    private bool prevmove;
    private MoveLeft()
    {
        SList = new List<Sprite>();
        SList.Add(Resources.Load<Sprite>("robot_walking_05"));
        SList.Add(Resources.Load<Sprite>("robot_walking_06"));
        SList.Add(Resources.Load<Sprite>("robot_walking_07"));
        SList.Add(Resources.Load<Sprite>("robot_walking_08"));
        standing = Resources.Load<Sprite>("robot_standing_02");
    }

    public static MoveLeft Instance
    {
        get
        {
            if (instance == null)
                instance = new MoveLeft();
            return instance;
        }
    }

    public override void MovePlayer(MoveitMoveit m)
    {
        float LRMove = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = m.GetComponent<Rigidbody2D>().velocity;
        if (LRMove >= -1e-5)
        {
            if (velocity.x < 0)
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
        if (m.currentCollisionRightDirection.Count != 0) return;
        m.GetComponent<Rigidbody2D>().velocity = new Vector2(LRMove * 3.0f, velocity.y);
        if (LRMove < -0.5f)

            if (m.spriteDisplayPriority < spritePriority)
            {
                m.spriteDisplay = SList[(m.frame % 60) / 15];
                m.spriteDisplayPriority = spritePriority;
            }
    }
    public override string Name()
    {
        return "MoveLeft";
    }
    public override string Type()
    {
        return "MoveLeft";
    }
}

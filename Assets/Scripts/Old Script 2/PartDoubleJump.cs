using UnityEngine;
using System.Collections.Generic;
using System;

public class PartDoubleJump : Part
{
    private static PartDoubleJump instance;
    public static PartDoubleJump Instance
    {
        get
        {
            if (instance == null)
                instance = new PartDoubleJump();
            return instance;
        }
    }

    private PartDoubleJump()
    {
    }

    public override void MovePlayer(PlayerController pc)
    {
        bool isJump = Input.GetAxisRaw("Jump") > 0.5f;
        Vector2 velocity = pc.velocity;
        if (isJump && pc.jumpCount < 2 && !pc.isJump )
        {
            pc.velocity = new Vector2(velocity.x, pc.gravityDirection * 3.5f);
            pc.jumpCount++;
        }
    }

    public override string Name()
    {
        return "PartDoubleJump";
    }

    public override string Type()
    {
        return "PartJump";
    }
    public override PartIndicator.PartType getEnum()
    {
        return PartIndicator.PartType.DoubleJump;
    }
}

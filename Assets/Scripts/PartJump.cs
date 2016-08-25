using UnityEngine;
using System.Collections.Generic;
using System;

public class PartJump : Part
{
    private static PartJump instance;
    public static PartJump Instance
    {
        get
        {
            if (instance == null)
                instance = new PartJump();
            return instance;
        }
    }

    private PartJump()
    {
    }

    public override void MovePlayer(PlayerController pc)
    {
        bool isJump = Input.GetAxisRaw("Jump") > 0.5f;
        Vector2 velocity = pc.velocity;
        if (isJump && pc.jumpCount == 0)
        {
            pc.velocity = new Vector2(velocity.x, pc.gravityDirection * 6.6f);
            pc.jumpCount++;
        }
    }

    public override string Name()
    {
        return "PartJump";
    }

    public override string Type()
    {
        return "PartJump";
    }
    public override PartIndicator.PartType getEnum()
    {
        return PartIndicator.PartType.Jump;
    }
}

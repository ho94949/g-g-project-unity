using UnityEngine;
using System.Collections;
using System;

public class MoveJump : Part
{
    private static MoveJump instance;
    private MoveJump()
    {

    }

    public static MoveJump Instance
    {
        get
        {
            if (instance == null)
                instance = new MoveJump();
            return instance;
        }
    }

    public override void MovePlayer(MoveitMoveit m)
    {
        bool isJump = Input.GetAxis("Jump") > 0.5f;
        if (isJump && 0==m.jumpCount)
        {
            m.jumpCount = 1;
            Vector2 velocity = m.GetComponent<Rigidbody2D>().velocity;
            m.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, 6.6f);

        }
    }
    public override string Name()
    {
        return "MoveJump";
    }
    public override string Type()
    {
        return "MoveJump";
    }
}

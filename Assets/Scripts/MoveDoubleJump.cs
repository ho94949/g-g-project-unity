using UnityEngine;
using System.Collections;
using System;

public class MoveDoubleJump : Part
{
    private static MoveDoubleJump instance;
    private static bool prevJump;
    private MoveDoubleJump()
    {
        prevJump = false;
    }

    public static MoveDoubleJump Instance
    {
        get
        {
            if (instance == null)
                instance = new MoveDoubleJump();
            return instance;
        }
    }

    public override void MovePlayer(MoveitMoveit m)
    {
        Debug.Log(m.jumpCount);
        bool isJump = Input.GetAxisRaw("Jump") > 0.5f;
        if (!prevJump &&  isJump && 2 > m.jumpCount) 
        {
            m.jumpCount++;
            Vector2 velocity = m.GetComponent<Rigidbody2D>().velocity;
            m.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, 3.5f);

        }
        prevJump = isJump;
    }
    public override string Name()
    {
        return "MoveDoubleJump";
    }
}

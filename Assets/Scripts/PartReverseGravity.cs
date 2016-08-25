using UnityEngine;
using System.Collections;
using System;

public class PartReverseGravity : Part {

    private static PartReverseGravity instance;
    public static PartReverseGravity Instance
    {
        get
        {
            if (instance == null)
                instance = new PartReverseGravity();
            return instance;
        }
    }

    private PartReverseGravity()
    {
        pinput = false;
    }

    private bool pinput = false;

    public override void MovePlayer(PlayerController pc)
    {
        bool input = Input.GetAxis("ReverseGravity") > 0.5f;
        if (!pinput && input)
        {
            pc.gravityDirection *= -1;
            pc.jumpCount = 999;
            GameObject g = GameObject.FindGameObjectWithTag("Player");
            if (g)
            {
                Vector3 v = g.transform.localScale;
                g.transform.localScale = new Vector3(v.x, -v.y, v.z);
            }
        }
        pinput = input;
    }

    public override string Type()
    {
        return "PartReverseGravity";
    }

    public override string Name()
    {
        return "PartReverseGravity";
    }

    public override PartIndicator.PartType getEnum()
    {
        return PartIndicator.PartType.ReverseGravity;
    }
}

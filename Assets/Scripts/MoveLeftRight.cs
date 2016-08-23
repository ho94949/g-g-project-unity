using UnityEngine;
using System.Collections;

public class MoveLeftRight : Part {

    private static MoveLeftRight instance;
    private MoveLeftRight()
    {

    }

    public static MoveLeftRight Instance
    {
        get
        {
            if (instance == null)
                instance = new MoveLeftRight();
            return instance;
        }
    }

    public override void MovePlayer(MoveitMoveit m)
    {
        MoveRight.Instance.MovePlayer(m);
        MoveLeft.Instance.MovePlayer(m);
    }
    public override string Name()
    {
        return "MoveLeftRight";
    }
}

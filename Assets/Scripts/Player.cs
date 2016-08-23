using UnityEngine;
using System.Collections;

public class Player
{
    private static Player instance;
    private Player()
    {
        P1 = MoveRight.Instance;
        P2 = null;
        P3 = null;
    }

    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = new Player();
            return instance;
        }
    }
    public Part P1
    {
        get; set;
    }
    public Part P2
    {
        get; set;
    }
    public Part P3
    {
        get; set;
    }
};

using UnityEngine;
using System.Collections;
using System;

public static class PartManager
{
	public static Part getPart(PartIndicator.PartType p )
    {
        switch (p)
        {

            case PartIndicator.PartType.Right:
                return PartRight.Instance;

            case PartIndicator.PartType.Jump:
                return PartJump.Instance;

            case PartIndicator.PartType.Left:
                return PartLeft.Instance;

            case PartIndicator.PartType.DoubleJump:
                return PartDoubleJump.Instance;

        }
        /*
        if (partName == "MoveLeft")
        {
            return MoveLeft.Instance;
        }
        if (partName == "MoveLeftRight")
        {
            return MoveLeftRight.Instance;
        }
        if (partName == "MoveJump")
        {
            return MoveJump.Instance;
        }
        if (partName == "MoveDoubleJump")
        {
            return MoveDoubleJump.Instance;
        }
        throw new ArgumentException();
        //fuck return*/
        return null;
        
    }
}

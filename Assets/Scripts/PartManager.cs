using UnityEngine;
using System.Collections;

public static class PartManager {

	public static Part getPart(string partName)
    {
        if(partName == "MoveRight")
        {
            return MoveRight.Instance;
        }
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
        return null;
    }
}

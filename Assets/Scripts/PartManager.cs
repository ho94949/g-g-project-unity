using UnityEngine;
using System.Collections;

public static class PartManager {

	public static Part getPart(string partName)
    {
        if(partName.Substring(2) == "MoveRight")
        {
            return MoveRight.Instance;
        }
        if (partName.Substring(2) == "MoveLeft")
        {
            return MoveLeft.Instance;
        }
        if (partName.Substring(2) == "MoveLeftRight")
        {
            return MoveLeftRight.Instance;
        }
        if (partName.Substring(2) == "MoveJump")
        {
            return MoveJump.Instance;
        }
        if (partName.Substring(2) == "MoveDoubleJump")
        {
            return MoveDoubleJump.Instance;
        }
        return null;
    }
}

using UnityEngine;

public class PartIndicator : MonoBehaviour
{
    public enum PartType
    {
        Left,
        Right,
        Jump,
        DoubleJump,
    }

    public PartType Type;
}

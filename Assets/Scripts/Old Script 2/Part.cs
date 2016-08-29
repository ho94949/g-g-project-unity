using UnityEngine;
using System.Collections;

public abstract class Part
{
    public abstract void MovePlayer(PlayerController m);
    public abstract string Name();
    public abstract string Type();
    public abstract PartIndicator.PartType getEnum();
}

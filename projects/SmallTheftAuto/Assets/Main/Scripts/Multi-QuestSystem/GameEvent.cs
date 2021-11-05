using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent 
{
    public string eventDescription;
}

public class DestroyGameEvent : GameEvent
{
    public string destroyableName;
    public DestroyGameEvent(string name)
    {
        destroyableName = name;
    }
}

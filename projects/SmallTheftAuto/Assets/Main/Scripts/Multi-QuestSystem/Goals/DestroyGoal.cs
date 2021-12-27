using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGoal : Quest.QuestGoal
{
    public string Destroyable;
    public override string GetDescription()
    {
        return $" Destroy the {Destroyable}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<DestroyGameEvent>(OnDestroyObject);
    }

    // TODO: I like how this system works. Maybe, Evaluate() could be called anytime `CurrentAmount` changes?
    // Instead of having to call it manually. Reduces cognitive load.
    private void OnDestroyObject(DestroyGameEvent eventInfo)
    {
        if(eventInfo.destroyableName == Destroyable)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}

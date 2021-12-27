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

    private void OnDestroyObject(DestroyGameEvent eventInfo)
    {
        if(eventInfo.destroyableName == Destroyable)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
}

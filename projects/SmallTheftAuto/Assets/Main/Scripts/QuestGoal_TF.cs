using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal_TF 
{
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool isComplete()
    {
        return (currentAmount >= requiredAmount);
    }
}

public enum GoalType
{
    Kill,
    Collect,
    Objective,
    Destroy
}

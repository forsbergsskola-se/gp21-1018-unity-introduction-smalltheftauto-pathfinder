using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public string Icon;
        public string Description;
    }

    [Header("Quest Info")]
    public Info Information;

    [System.Serializable]
    public struct stat
    {
        public int Currency;
        public int XP;
    }

    [Header("Reward")]
    public stat Reward = new stat { Currency = 10, XP = 10 };
    public bool completed { get; protected set; }
    public QuestCompletedEvent questCompleted;

    
    
    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;

        public bool completed { get; protected set; }
        [HideInInspector]
        public UnityEvent GoalCompleted;


        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Evaluate()
        {
            if(CurrentAmount >= RequiredAmount)
            {
                Complete();
            }
        }

        private void Complete()
        {
            completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {

            Complete();
        }
    }

    public List<QuestGoal> goals;

    public void Intialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();

        foreach(var goal in goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        completed = goals.All(g => g.completed );
        if (completed)
        {
            questCompleted.Invoke(this);
            questCompleted.RemoveAllListeners();
        }
    }

}

public class QuestCompletedEvent : UnityEvent<Quest> { }

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{

}
#endif

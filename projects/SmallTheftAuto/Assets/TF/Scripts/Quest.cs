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
            if (CurrentAmount >= RequiredAmount)
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

    public void Inýtialize()
    {
        completed = false;
        questCompleted = new QuestCompletedEvent();

        foreach (var goal in goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    private void CheckGoals()
    {
        completed = goals.All(g => g.completed);
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
    //serialized property fields for quest infos & rewards
    SerializedProperty m_QuestInfoProperty;
    SerializedProperty m_QuestStatProperty;

    
    List<string> m_QuestGoalType;               // List of strings for storing the names different goals 

    SerializedProperty m_QuestGoalListProperty; // a serialize feiled for goal list it self

    [MenuItem("Assets/Quest", priority = 0)]    // for showing the menu Item for a quest
    public static void CreateQuest()           
    {
        var newQuest = CreateInstance<Quest>();
        ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");

    }

    /// <summary>
    /// Onenable while be activate right after an instance of a quesrt
    /// In the methode we gather all the properties and Initialize the field that we created.
    /// to do this we use .FindProperty method in the serializedObject and pass a parameter to it!
    /// param -> nameof(the name of the feild we want. ex: Quest.goals);
    /// </summary>
    void OnEnable()
    {
        
        m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
        m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.Reward));

        m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.goals));

        var lookup = typeof(Quest.QuestGoal);

        //Getting the names of all of our goals
        m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()           //load all the classes that inherits form QuestGoal Class
            .SelectMany(assembly => assembly.GetTypes())                           // and check if the class is not abstarct and get their names and saves them
            .Where(q => q.IsClass && !q.IsAbstract && q.IsSubclassOf(lookup))      // In to the List;
            .Select(type => type.Name).ToList();                                   // The list will be desplayed in a dropDown menu so we can add different goal to a quest
        
    }

    public override void OnInspectorGUI()
    {
        var child = m_QuestInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        while(child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        child = m_QuestStatProperty.Copy();
        depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Reward", EditorStyles.boldLabel);
        while(child.depth> depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        int choice = EditorGUILayout.Popup("Add a new Quest Goal", -1, m_QuestGoalType.ToArray());
        if(choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);
            AssetDatabase.AddObjectToAsset(newInstance, target);
            m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
            m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1).objectReferenceValue = newInstance;
        }

        Editor editor = null;
        int toDelete = -1;
        for (int i = 0; i < m_QuestGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject SO = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref editor);
            editor.OnInspectorGUI();
            EditorGUILayout.EndVertical();
            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }
            EditorGUILayout.EndHorizontal();

        }

        if(toDelete != -1)
        {
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
            DestroyImmediate(item, true);
            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);     // for some reason we need to do it twice first is null the entry and second actually remove it!
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

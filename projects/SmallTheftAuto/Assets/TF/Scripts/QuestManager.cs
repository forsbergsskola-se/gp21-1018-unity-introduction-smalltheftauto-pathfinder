using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;
    [SerializeField]
    private GameObject questHolder;
    [SerializeField]
    private Transform questContent;

    public List<Quest> currentQuests;

    private void Awake()
    {
        foreach(var quest in currentQuests)
        {
            quest.Inýtialize();
            quest.questCompleted.AddListener(OnQuestCompleted);
            GameObject questObj = Instantiate(questPrefab, questContent);
            questObj.transform.Find("Icon").GetComponent<Image>().sprite = quest.Information.Icon;

            questObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                questHolder.GetComponent<QuestWindow>().Initialize(quest);
                questHolder.SetActive(true);
            });

        }
    }

    public void Destroy(string objectNameToDestroy)
    {
        EventManager.Instance.QueueEvent(new DestroyGameEvent(objectNameToDestroy));
    }
    private void OnQuestCompleted(Quest quest)
    {
        questContent.GetChild(currentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
    }
}

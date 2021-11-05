using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager_TF : MonoBehaviour
{

    public Quest_TF quest;
    public Player_TF player;
    //public GameObject questPanel;
    public GameObject questDetailPanel;
    public GameObject FXPrefab;
    public Text title;
    public Text description;
    public Text exp;
    public Text money;


    public void OpenQuestPanel()
    {
       // questPanel.SetActive(false);
        questDetailPanel.SetActive(true);
        title.text = quest.title;
        description.text = quest.description;
        exp.text = quest.experienceReward.ToString();
        money.text = quest.moneyReward.ToString();
    }

    public void CancelQuestOanel()
    {
        //questPanel.SetActive(false);
        questDetailPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ThePlayer"))
        {
            questDetailPanel.SetActive(true);
            OpenQuestPanel();
            this.gameObject.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        
        questDetailPanel.SetActive(false);
        quest.isActive = true;
        player.quest = quest;
        FXPrefab.SetActive(false);
    }


}

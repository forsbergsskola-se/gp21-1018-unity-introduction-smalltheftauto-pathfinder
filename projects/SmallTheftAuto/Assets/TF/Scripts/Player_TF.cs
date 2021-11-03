using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TF : MonoBehaviour
{
    public int health = 100;
    public int experience = 0;
    public int gold = 0;

    public Quest_TF quest;


    public void DoQuests()
    {
        if (quest.isActive)
        {

        }
    }

}

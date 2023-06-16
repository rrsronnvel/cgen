using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPop : MonoBehaviour
{
    [SerializeField] private GameObject goal3; // Reference to your Goal 3 object
    [SerializeField] private GameObject goal2; // Reference to your Goal 2 object
    [SerializeField] private TextMeshProUGUI[] goalTexts3; // Array of goal texts for Goal 3
    [SerializeField] private TextMeshProUGUI[] goalTexts2; // Array of goal texts for Goal 2
    [SerializeField] private Image[] checkImages3; // Array of check images for Goal 3
    [SerializeField] private Image[] checkImages2; // Array of check images for Goal 2

    public void UpdateQuestPop(NPCController[] requiredInteractions)
    {
        for (int i = 0; i < requiredInteractions.Length; i++)
        {
            NPCController obj = requiredInteractions[i];

            // Update the goal texts and check images based on the interaction status
            if (requiredInteractions.Length == 3)
            {
                goal3.SetActive(true);
                goalTexts3[i].color = obj.hasBeenInteractedWith ? Color.green : Color.red;
                checkImages3[i].enabled = obj.hasBeenInteractedWith;
            }
            else if (requiredInteractions.Length == 2)
            {
                goal2.SetActive(true);
                goalTexts2[i].color = obj.hasBeenInteractedWith ? Color.green : Color.red;
                checkImages2[i].enabled = obj.hasBeenInteractedWith;
            }
        }
    }
}


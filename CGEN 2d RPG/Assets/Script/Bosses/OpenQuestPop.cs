using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenQuestPop : MonoBehaviour
{
    [SerializeField] private Button openButton; // Button to open the QuestPop panel
    [SerializeField] private GameObject questPopPanel; // QuestPop panel
    [SerializeField] private BossPortal bossPortal; // BossPortal object
    [SerializeField] private QuestPop questPop; // QuestPop script

    public void Start()
    {
        // Add listener for the open button
        openButton.onClick.AddListener(OpenPanel);
    }

    public void OpenPanel()
    {
        // Show the QuestPop panel
        questPopPanel.SetActive(true);

        // Update the QuestPop content
        questPop.UpdateQuestPop(bossPortal.GetRequiredInteractions());
    }
}


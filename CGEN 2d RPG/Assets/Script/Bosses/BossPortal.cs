using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossPortal : MonoBehaviour, Interactable
{
    [SerializeField] private QuestPop questPop; // Reference to your QuestPop script
    [SerializeField] private GameObject controlButtons;

    [SerializeField] private string sceneName;
    [SerializeField] private GameObject bossPortalUI;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private NPCController[] requiredInteractions; // Array of objects that need to be interacted with
    [SerializeField] private GameObject goal3; // Reference to your Goal 3 object
    [SerializeField] private GameObject goal2; // Reference to your Goal 2 object
    [SerializeField] private TextMeshProUGUI[] goalTexts3; // Array of goal texts for Goal 3
    [SerializeField] private TextMeshProUGUI[] goalTexts2; // Array of goal texts for Goal 2
    [SerializeField] private Image[] checkImages3; // Array of check images for Goal 3
    [SerializeField] private Image[] checkImages2; // Array of check images for Goal 2

   

    public NPCController[] GetRequiredInteractions()
    {
        return requiredInteractions;
    }

    private void Start()
    {
        // Add listeners for your buttons
        enterButton.onClick.AddListener(OnEnterButton);
        closeButton.onClick.AddListener(OnCloseButton);

        // Initially hide the BossPortal UI and the goal objects
        bossPortalUI.SetActive(false);
        goal3.SetActive(false);
        goal2.SetActive(false);

        // Initialize the QuestPop
        questPop.InitializeQuestPop(requiredInteractions.Length);
    }



    public void Interact()
    {
        Debug.Log("interacting");

        controlButtons.SetActive(false);
        // Show the BossPortal UI when player interacts with the BossPortal
        bossPortalUI.SetActive(true);

        // Check if all required interactions have been completed
        bool allInteractionsCompleted = true;
        for (int i = 0; i < requiredInteractions.Length; i++)
        {
            NPCController obj = requiredInteractions[i];
            if (!obj.hasBeenInteractedWith)
            {
                allInteractionsCompleted = false;
            }

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

        // Disable the Enter button if not all required interactions have been completed
        enterButton.interactable = allInteractionsCompleted;

        // Update the QuestPop
        questPop.UpdateQuestPop(requiredInteractions);
    }

    private void OnEnterButton()
    {
        // Reset the interaction status of all required interactions
        foreach (NPCController obj in requiredInteractions)
        {
            obj.ResetInteraction();
        }

        // Call RestartGame method from TestingRestart class
        TestingRestart.instance.RestartGame();

        SceneManager.LoadScene(sceneName);
    }

    private void OnCloseButton()
    {
        bossPortalUI.SetActive(false);
        controlButtons.SetActive(true);
    }
}



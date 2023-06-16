using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossPortal : MonoBehaviour, Interactable
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject bossPortalUI;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private NPCController[] requiredInteractions; // Array of objects that need to be interacted with

    private void Start()
    {
        // Add listeners for your buttons
        enterButton.onClick.AddListener(OnEnterButton);
        closeButton.onClick.AddListener(OnCloseButton);

        // Initially hide the BossPortal UI
        bossPortalUI.SetActive(false);
    }

    public void Interact()
    {
        Debug.Log("interacting");
        // Show the BossPortal UI when player interacts with the BossPortal
        bossPortalUI.SetActive(true);

        // Check if all required interactions have been completed
        bool allInteractionsCompleted = true;
        foreach (NPCController obj in requiredInteractions)
        {
            if (!obj.hasBeenInteractedWith)
            {
                allInteractionsCompleted = false;
                break;
            }
        }

        // Disable the Enter button if not all required interactions have been completed
        enterButton.interactable = allInteractionsCompleted;
    }

    private void OnEnterButton()
    {
        // Reset the interaction status of all required interactions
        foreach (NPCController obj in requiredInteractions)
        {
            obj.ResetInteraction();
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OnCloseButton()
    {
        bossPortalUI.SetActive(false);
    }
}


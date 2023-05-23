using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialMessageArchive : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject archivePanel;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject controlButtons;

    private Dictionary<string, SpecialMessage> archivedMessages = new Dictionary<string, SpecialMessage>();

    public bool IsInteracting { get; private set; } = false;

    public void Interact()
    {
        ToggleArchivePanel(true);
        controlButtons.SetActive(false);
        IsInteracting = true; // Set IsInteracting to true
    }

    public void ClosePanel()
    {
        ToggleArchivePanel(false);
        // Only re-enable controlButtons if no longer interacting with this SpecialMessageArchive
        if (!IsInteracting) controlButtons.SetActive(true);
    }

    public void EndInteraction()
    {
        IsInteracting = false; // Set IsInteracting to false
        ClosePanel(); // Close panel (which will now re-enable controlButtons since IsInteracting is false)
    }


    private void ToggleArchivePanel(bool show)
    {
        archivePanel.SetActive(show);
    }

    public void AddMessage(SpecialMessage message)
    {
        if (!archivedMessages.ContainsKey(message.Id))
        {
            archivedMessages[message.Id] = message;
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = message.Title;

            // Pass reference to this SpecialMessageArchive object
            newButton.GetComponent<Button>().onClick.AddListener(() => message.ShowMessage(this));
        }
    }

}
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

    private Dictionary<string, SpecialMessage> archivedMessages = new Dictionary<string, SpecialMessage>();

    public void Interact()
    {
        ToggleArchivePanel(true);
    }



    public void ClosePanel()
    {
        ToggleArchivePanel(false);
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
            newButton.GetComponent<Button>().onClick.AddListener(() => message.ShowMessage());
        }
    }

}
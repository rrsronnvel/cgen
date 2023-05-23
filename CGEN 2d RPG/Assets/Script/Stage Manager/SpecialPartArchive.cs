using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialPartArchive : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject archivePanel;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject controlButtons;

    private Dictionary<string, SpecialPart> archivedParts = new Dictionary<string, SpecialPart>();

    public bool IsInteracting { get; private set; } = false;

    public void Interact()
    {
        ToggleArchivePanel(true);
        controlButtons.SetActive(false);
        IsInteracting = true;
    }

    public void ClosePanel()
    {
        ToggleArchivePanel(false);
        if (!IsInteracting) controlButtons.SetActive(true);
    }

    public void EndInteraction()
    {
        IsInteracting = false;
        ClosePanel();
    }

    private void ToggleArchivePanel(bool show)
    {
        archivePanel.SetActive(show);
    }

    public void AddPart(SpecialPart part)
    {
        if (!archivedParts.ContainsKey(part.Id))
        {
            archivedParts[part.Id] = part;
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = part.Title;

            // Pass reference to this SpecialPartArchive object
            newButton.GetComponent<Button>().onClick.AddListener(() => part.ShowPart(this, false));
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMessage : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private GameObject messagePanel; // Add a reference to the UI panel
    [SerializeField] private GameObject controlButtons; // Add a reference to the parent object of all UI control buttons

    [SerializeField] private string title;

    [SerializeField] private SpecialMessageArchive specialMessageArchive;


    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;

    public string Id => id;
    public string Title => title;
    // Add this property
    public bool Collected => collected;




    public void LoadData(GameData data, bool isRestarting)
    {
        data.messagesCollected.TryGetValue(id, out collected);

        if (collected)
        {
            gameObject.SetActive(false);
            specialMessageArchive.AddMessage(this);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.messagesCollected.ContainsKey(id))
        {
            data.messagesCollected.Remove(id);
        }
        data.messagesCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            collected = true;
            ShowMessage();
            specialMessageArchive.AddMessage(this);
        }
    }


    public void ShowMessage(SpecialMessageArchive archive = null)
    {
        messagePanel.SetActive(true);
        controlButtons.SetActive(false); // Disable all control buttons

        if (archive != null)
        {
            // Hide the archive panel
            archive.ClosePanel();
        }

        messagePanel.AddComponent<PopupCloseOnClick>().Initialize(() => CloseMessage(archive));
    }

    private void CloseMessage(SpecialMessageArchive archive = null)
    {
        messagePanel.SetActive(false);
        // Don't re-enable controlButtons here - let SpecialMessageArchive handle it
        if (archive != null)
        {
            // End interaction with the archive (will show the archive panel and re-enable controlButtons if necessary)
            archive.EndInteraction();
        }
        else
        {
            // No archive, so re-enable controlButtons
            controlButtons.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
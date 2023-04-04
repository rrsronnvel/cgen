using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MessageData
{
    public string title;
    public string content;
    public Sprite image;
}

public class SpecialNotesArchive : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject messageListPrefab;
    [SerializeField] private GameObject messageDetailsPrefab;
    [SerializeField] private Button messageButtonPrefab;

    private Canvas canvas;
    private List<MessageData> messages;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        messages = new List<MessageData>();
    }

    public void AddMessage(MessageData message)
    {
        messages.Add(message);
    }

    public void Interact()
    {
        ShowMessageList();
    }

    private void ShowMessageList()
    {
        GameObject messageListInstance = Instantiate(messageListPrefab, canvas.transform);
        messageListInstance.SetActive(true);

        // Hide the placeholder button
        messageListInstance.transform.Find("Notes Container/Notes").gameObject.SetActive(false);

        // Add a listener to the Close Button
        Button closeButton = messageListInstance.transform.Find("Close Button").GetComponent<Button>();
        closeButton.onClick.AddListener(() => CloseMessageList(messageListInstance));

        // Add buttons for each message
        for (int i = 0; i < messages.Count; i++)
        {
            MessageData messageData = messages[i];
            Button messageButton = InstantiateButtonForMessage(messageListInstance, messageData.title);
            messageButton.onClick.AddListener(() =>
            {
                ShowMessageDetails(messageListInstance, messageData);
            });
        }
    }

    private Button InstantiateButtonForMessage(GameObject parent, string message)
    {
        // Get the MessageButtonsContainer GameObject
        GameObject messageButtonsContainer = parent.transform.Find("Notes Container").gameObject;

        // Instantiate the button prefab and set its text to the message
        Button buttonInstance = Instantiate(messageButtonPrefab, messageButtonsContainer.transform);
        buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = message; // Update this line

        // Set the button's RectTransform
        RectTransform buttonRect = buttonInstance.GetComponent<RectTransform>();
        RectTransform prefabRect = messageButtonPrefab.GetComponent<RectTransform>();

        buttonRect.anchorMin = new Vector2(0, 1);
        buttonRect.anchorMax = new Vector2(0, 1);
        buttonRect.pivot = new Vector2(0.5f, 1);
        buttonRect.sizeDelta = prefabRect.sizeDelta;
        float yOffset = (buttonRect.sizeDelta.y + 10) * (messageButtonsContainer.transform.childCount - 1);
        buttonRect.anchoredPosition = new Vector2(prefabRect.anchoredPosition.x, -yOffset);

        return buttonInstance;
    }

    private void ShowMessageDetails(GameObject messageListInstance, MessageData messageData)
    {
        GameObject messageDetailsInstance = Instantiate(messageDetailsPrefab, canvas.transform);
        messageDetailsInstance.SetActive(true);

        // Assign the title, content, and image
        messageDetailsInstance.transform.Find("Title Text").GetComponent<TextMeshProUGUI>().text = messageData.title;
        messageDetailsInstance.transform.Find("Content Text").GetComponent<TextMeshProUGUI>().text = messageData.content;
        messageDetailsInstance.transform.Find("Image").GetComponent<Image>().sprite = messageData.image;

        Button closeButton = messageDetailsInstance.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() =>
        {
            Destroy(messageDetailsInstance);
            messageListInstance.SetActive(true);
        });

        messageListInstance.SetActive(false);
    }


    public void CloseMessageList(GameObject messageListInstance)
    {
        Destroy(messageListInstance);
    }
}

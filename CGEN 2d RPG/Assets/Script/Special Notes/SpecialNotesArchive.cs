using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MessageData
{
    public string id;
    public string title;
    public string content;
    public string imageBase64;

    // Use this property to get or set the Sprite for the MessageData object
    [XmlIgnore]
    public Sprite Image
    {
        get
        {
            return SpriteFromTexture2D(Base64ToTexture(imageBase64));
        }
        set
        {
            imageBase64 = TextureToBase64(value.texture);
        }
    }

    // Helper methods for converting between Sprite, Texture2D, and Base64 string

    private string TextureToBase64(Texture2D texture)
    {
        return Convert.ToBase64String(texture.EncodeToPNG());
    }

    private Texture2D Base64ToTexture(string base64)
    {
        byte[] imageData = Convert.FromBase64String(base64);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imageData);
        return tex;
    }

    private Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}




public class SpecialNotesArchive : MonoBehaviour, Interactable, IDataPersistence
{
    [SerializeField] private GameObject messageListPrefab;
    [SerializeField] private GameObject messageDetailsPrefab;
    [SerializeField] private Button messageButtonPrefab;

    private Canvas canvas;
    private List<MessageData> messages = new List<MessageData>();


    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        //messages = new List<MessageData>();
    }

    public void LoadData(GameData data, bool isRestarting)
    {
        messages.Clear();
        foreach (KeyValuePair<string, MessageData> pair in data.specialNotesData)
        {
            messages.Add(pair.Value);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.specialNotesData.Clear();
        foreach (MessageData message in messages)
        {
            data.specialNotesData.Add(message.id, message);
        }
    }


    public void AddMessage(MessageData messageData)
    {
        if (!messages.Exists(x => x.id == messageData.id))
        {
            MessageData newMessage = new MessageData
            {
                id = messageData.id,
                title = messageData.title,
                content = messageData.content,
                Image = messageData.Image
            };
            messages.Add(newMessage);
        }
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
        messageDetailsInstance.transform.Find("Image").GetComponent<Image>().sprite = messageData.Image;

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

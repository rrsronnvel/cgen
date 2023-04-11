using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this line to include the TextMeshPro namespace

public class FactsMessage : MonoBehaviour, IDataPersistence
{

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;

    [SerializeField] private string messageTitle;
    [SerializeField] private string messageContent;
    [SerializeField] private Sprite messageImage;
    [SerializeField] private GameObject popupPrefab;
    private Canvas canvas;
    private SpecialNotesArchive messageArchive;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        messageArchive = FindObjectOfType<SpecialNotesArchive>();
    }

    public void LoadData(GameData data)
    {
        data.notesCollected.TryGetValue(id, out collected);

        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.notesCollected.ContainsKey(id))
        {
            data.notesCollected.Remove(id);
        }
        data.notesCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopup();
            messageArchive.AddMessage(new MessageData { id = id, title = messageTitle, content = messageContent, Image = messageImage });
            collected = true;
            gameObject.SetActive(false);
        }
    }


    private void ShowPopup()
    {
        GameObject popupInstance = Instantiate(popupPrefab, canvas.transform);
        popupInstance.SetActive(true);

        // Assign the title, content, and image
        popupInstance.transform.Find("Title Text").GetComponent<TextMeshProUGUI>().text = messageTitle; // Update this line
        popupInstance.transform.Find("Content Text").GetComponent<TextMeshProUGUI>().text = messageContent; // Update this line
        popupInstance.transform.Find("Image").GetComponent<Image>().sprite = messageImage;

        Button closeButton = popupInstance.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() => ClosePopup(popupInstance));
    }

    private void ClosePopup(GameObject popupInstance)
    {
        Destroy(popupInstance);
    }
}

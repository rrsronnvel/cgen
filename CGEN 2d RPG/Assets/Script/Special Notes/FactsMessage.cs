using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this line to include the TextMeshPro namespace

public class FactsMessage : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPopup();
            messageArchive.AddMessage(new MessageData { title = messageTitle, content = messageContent, image = messageImage });
            Destroy(gameObject);
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

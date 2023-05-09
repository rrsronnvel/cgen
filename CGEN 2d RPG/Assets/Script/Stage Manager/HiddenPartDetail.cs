using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HiddenPartDetail : MonoBehaviour, Interactable, IDataPersistence
{
    [SerializeField] private string id;

    [SerializeField] private GameObject puzzlePopupPrefab;

    [SerializeField] private string stageIdentifier; 
    [SerializeField] private List<StageMiniGamePair> stageMiniGamePairs; 


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
    private HiddenPartArchive messageArchive;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        messageArchive = FindObjectOfType<HiddenPartArchive>();
    }

    public void LoadData(GameData data, bool isRestarting)
    {
        data.hiddenPartsCollected.TryGetValue(id, out collected);

        if (collected)
        {
            // change to false if want to remove if already pick up
            gameObject.SetActive(true);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.hiddenPartsCollected.ContainsKey(id))
        {
            data.hiddenPartsCollected.Remove(id);
        }
        data.hiddenPartsCollected.Add(id, collected);
    }

    public void Interact()
    {
        Debug.Log("interacting");
        ShowPopup();
        messageArchive.AddMessage(new PartData { id = id, title = messageTitle, content = messageContent, Image = messageImage });
        collected = true;
        //gameObject.SetActive(false);
    }


    private void ShowPopup()
    {
        GameObject popupInstance = Instantiate(popupPrefab, canvas.transform);
        popupInstance.SetActive(true);

        // Assign the title, content, and image
        popupInstance.transform.Find("Title Text").GetComponent<TextMeshProUGUI>().text = messageTitle; // Update this line
        popupInstance.transform.Find("Content Text").GetComponent<TextMeshProUGUI>().text = messageContent; // Update this line
        popupInstance.transform.Find("Image").GetComponent<Image>().sprite = messageImage;

        popupInstance.AddComponent<PopupCloseOnClick>().Initialize(() => ClosePopup(popupInstance));
    }

    private void ClosePopup(GameObject popupInstance)
    {
        Destroy(popupInstance);
        //ShowPuzzlePopup();
    }
    /*
    private void ShowPuzzlePopup()
    {
        GameObject puzzlePopupInstance = Instantiate(puzzlePopupPrefab, canvas.transform);
        puzzlePopupInstance.SetActive(true);
        puzzlePopupInstance.AddComponent<PopupCloseOnClick>().Initialize(() => ClosePuzzlePopup(puzzlePopupInstance));
    }

    private void ClosePuzzlePopup(GameObject puzzlePopupInstance)
    {
        Destroy(puzzlePopupInstance);
        LoadMiniGameScene();
    }

    private void LoadMiniGameScene()
    {
        // Find the mini-game scene name for the current stage
        StageMiniGamePair pair = stageMiniGamePairs.Find(pair => pair.StageIdentifier == stageIdentifier);

        if (!string.IsNullOrEmpty(pair.MiniGameScene))
        {
            SceneManager.LoadScene(pair.MiniGameScene);
        }
        else
        {
            Debug.LogError("No mini-game scene defined for the current stage.");
        }
    }
    */


}
/*
[System.Serializable]
public struct StageMiniGamePair
{
    public string StageIdentifier;
    public string MiniGameScene;
}*/

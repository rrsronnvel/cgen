using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class SpecialPart : MonoBehaviour, IDataPersistence, Interactable
{
    [SerializeField] private string id;
    [SerializeField] private GameObject partPanel;
    [SerializeField] private GameObject controlButtons;

    [SerializeField] private string title;

    [SerializeField] private SpecialPartArchive specialPartArchive;

    [SerializeField] private GameObject puzzlePopupPrefab;

    [SerializeField] private string stageIdentifier;
    [SerializeField] private List<StageMiniGamePair> stageMiniGamePairs;

    private Canvas canvas;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;

    public string Id => id;
    public string Title => title;
    public bool Collected => collected;


    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        
    }

        public void LoadData(GameData data, bool isRestarting)
    {
        data.partsCollected.TryGetValue(id, out collected);

        if (collected)
        {
            //gameObject.SetActive(false);
            specialPartArchive.AddPart(this);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.partsCollected.ContainsKey(id))
        {
            data.partsCollected.Remove(id);
        }
        data.partsCollected.Add(id, collected);
    }


    public void Interact()
    {
        Debug.Log("interacting");
        ShowPart(null);
        specialPartArchive.AddPart(this);
        collected = true;
        //gameObject.SetActive(false);
    }


    public void ShowPart(SpecialPartArchive archive, bool showPuzzlePopup = true)
    {
        partPanel.SetActive(true);
        controlButtons.SetActive(false);

        if (archive != null)
        {
            // Hide the archive panel
            archive.ClosePanel();
        }

        partPanel.AddComponent<PopupCloseOnClick>().Initialize(() => ClosePart(archive, showPuzzlePopup));
    }

    private void ClosePart(SpecialPartArchive archive, bool showPuzzlePopup = true)
    {
        partPanel.SetActive(false);

        if (archive != null)
        {
            archive.EndInteraction();
        }
        else
        {
            controlButtons.SetActive(true);
        }

        if (showPuzzlePopup)
        {
            ShowPuzzlePopup();
        }
    }


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
}


[System.Serializable]
public struct StageMiniGamePair
{
    public string StageIdentifier;
    public string MiniGameScene;
}
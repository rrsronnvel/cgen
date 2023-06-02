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

   
    public int stageNumber;

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
            StageManager.instance.CompleteCurrentStage(stageNumber);
            SceneManager.LoadScene("BOSS1");
            controlButtons.SetActive(true);
            
        }
       
    }

   

   
}


[System.Serializable]
public struct StageMiniGamePair
{
    public string StageIdentifier;
    public string MiniGameScene;
}
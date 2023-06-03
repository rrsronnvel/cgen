using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TBCTrigger : MonoBehaviour, IDataPersistence, Interactable
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public GameObject panel;  // Assign this in Unity inspector
    [SerializeField] private GameObject controlButtons;

    public string sceneName;


    private bool isWon = false;

    public void LoadData(GameData data, bool isRestarting)
    {
        data.TBCWon.TryGetValue(id, out isWon);

        if (isWon)
        {
            gameObject.SetActive(false);
           
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.TBCWon.ContainsKey(id))
        {
            data.TBCWon.Remove(id);
        }
        data.TBCWon.Add(id, isWon);
    }

    public void Interact()
    {
        panel.SetActive(true);
        controlButtons.SetActive(false); 

        // Save the game after collecting the object
        DataPersistenceManager.instance.SaveGame();
    }

    public void OnEnterButton()
    {
        // Hide the panel
        panel.SetActive(false);
        controlButtons.SetActive(true);

        // Store the ID of the TBCTrigger and the name of the current scene
        if (DataPersistenceManager.instance != null)
        {
            GameData gameData = DataPersistenceManager.instance.GetGameData();
            if (gameData != null)
            {
                gameData.previousScene = SceneManager.GetActiveScene().name;
                gameData.activeTBC = id; // Store the ID of the TBCTrigger
            }
        }

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCutscene : MonoBehaviour, IDataPersistence, Interactable
{
    [SerializeField] private string id;
    public GameObject objectToActivate;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }


    private bool collected = false;


    public void LoadData(GameData data, bool isRestarting)
    {
        data.triggerCollected.TryGetValue(id, out collected);

        if (collected)
        {
            gameObject.SetActive(false);
        }
        else
        {
            // Move the object to 0, 0, 0 if it's not collected yet
            // and if the first stage is completed
            if (StageManager.instance.stageCompletionStatus[0])
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }


    public void SaveData(ref GameData data)
    {
        if (data.triggerCollected.ContainsKey(id))
        {
            data.triggerCollected.Remove(id);
        }
        data.triggerCollected.Add(id, collected);
    }


    public void Interact()
    {
        objectToActivate.SetActive(true);
        collected = true;
        gameObject.SetActive(false);

        // Save the game after collecting the object
        DataPersistenceManager.instance.SaveGame();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStageCutscene : MonoBehaviour, IDataPersistence
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
        data.triggerStageCollected.TryGetValue(id, out collected);

        if (collected)
        {
            gameObject.SetActive(false);
        
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.triggerStageCollected.ContainsKey(id))
        {
            data.triggerStageCollected.Remove(id);
        }
        data.triggerStageCollected.Add(id, collected);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            objectToActivate.SetActive(true);
            collected = true;
            gameObject.SetActive(false);

            // Save the game after collecting the object
            DataPersistenceManager.instance.SaveGame();
        }
    }
}
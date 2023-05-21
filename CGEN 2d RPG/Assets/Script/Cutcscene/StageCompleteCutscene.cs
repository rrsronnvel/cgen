using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCompleteCutscene : MonoBehaviour, IDataPersistence
{
    public enum StageComplete
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    [SerializeField] private string id;
    public GameObject objectToActivate;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public StageComplete stageComplete;


    private bool collected = false;


    public void LoadData(GameData data, bool isRestarting)
    {
        data.triggerCompleteCollected.TryGetValue(id, out collected);

        if (collected)
        {
            gameObject.SetActive(false);
        }
        else
        {
     
            switch (stageComplete)
            {
                case StageComplete.Stage3:
                    if (StageManager.instance.stageCompletionStatus[2])
                    {
                        transform.position = new Vector3(-4.54f, -3.61f, 0);
                    }
                    break;
                case StageComplete.Stage5:
                    if (StageManager.instance.stageCompletionStatus[4])
                    {
                        transform.position = new Vector3(-4.54f, -3.61f, 0);
                    }
                    break;
                    // Add similar cases for other parts here...
            }
        }
    }


    public void SaveData(ref GameData data)
    {
        if (data.triggerCompleteCollected.ContainsKey(id))
        {
            data.triggerCompleteCollected.Remove(id);
        }
        data.triggerCompleteCollected.Add(id, collected);
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

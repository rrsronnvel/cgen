using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private float healthValue;

    private bool collected = false;


    public void LoadData(GameData data, bool isRestarting)
    {
        data.healthsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            if (isRestarting)
            {
                collected = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        /*else if (isRestarting && !collected)
        {
            gameObject.SetActive(true);
        } */
    }


    public void SaveData(ref GameData data)
    {
        if (data.healthsCollected.ContainsKey(id))
        {
            data.healthsCollected.Remove(id);
        }
        data.healthsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            collected = true;
            gameObject.SetActive(false);

            // Save the game after collecting health
            DataPersistenceManager.instance.SaveGame();
        }
    }
}

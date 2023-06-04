using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectiblesBoss : MonoBehaviour, IDataPersistence, ICollectible
{
    [SerializeField] private string id;

    public void SetId(string newId)
    {
        id = newId;
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

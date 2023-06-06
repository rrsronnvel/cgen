using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpCollectiblesBoss : MonoBehaviour, ICollectible
{
    [SerializeField] private string id;

    public void SetId(string newId)
    {
        id = newId;
    }

    private bool collected = false;


    public void LoadData(GameData data, bool isRestarting)
    {
        data.speedsCollected.TryGetValue(id, out collected);
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

    }


    public void SaveData(ref GameData data)
    {
        if (data.speedsCollected.ContainsKey(id))
        {
            data.speedsCollected.Remove(id);
        }
        data.speedsCollected.Add(id, collected);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MoveSpeed moveSpeed = other.gameObject.GetComponent<MoveSpeed>();
            if (moveSpeed != null)
            {
                collected = true;
                moveSpeed.BoostSpeed();
                gameObject.SetActive(false);
            }
        }
    }
}

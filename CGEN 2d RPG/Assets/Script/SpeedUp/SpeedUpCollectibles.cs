using UnityEngine;

public class SpeedUpCollectibles : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
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

using UnityEngine;

public class ImmunityCollectibles : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float immunityDuration = 10f;

    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;

    public void LoadData(GameData data, bool isRestarting)
    {
        data.immunityCollected.TryGetValue(id, out collected);
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
        if (data.immunityCollected.ContainsKey(id))
        {
            data.immunityCollected.Remove(id);
        }
        data.immunityCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                collected = true;
                health.CollectImmunity(immunityDuration);
                gameObject.SetActive(false);
            }
        }
    }
}

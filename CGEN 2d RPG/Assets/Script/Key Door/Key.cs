using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] private KeyType keyType;

    public enum KeyType
    {
        Red,
        Green,
        Blue
    }

    private bool collected = false;

    public KeyType GetKeyType()
    {
        return keyType;
    }

    public void LoadData(GameData data)
    {
        data.keysCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.keysCollected.ContainsKey(id))
        {
            data.keysCollected.Remove(id);
        }
        data.keysCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<KeyHolder>().AddKey(keyType);
            collected = true;
            gameObject.SetActive(false);
        }
    }

}

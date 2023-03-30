using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public float currentHealth;

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> healthsCollected;

    public SerializableDictionary<string, bool> keysCollected;

    public List<Key.KeyType> keyList; // Add this line

    public SerializableDictionary<string, bool> openDoors;

    // the values defined in this constructor will be the default values
    // the game  starts with when there's no data to load



    public GameData()
    {
        this.currentHealth = 3;
       // playerPosition = Vector3.zero;
        healthsCollected = new SerializableDictionary<string, bool>();
        keysCollected = new SerializableDictionary<string, bool>();

        keyList = new List<Key.KeyType>();

        openDoors = new SerializableDictionary<string, bool>();

        playerPosition = new Vector3(-12.0f, 1.0f, 0f);
    }

}

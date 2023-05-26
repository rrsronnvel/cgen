using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData 
{
    public float currentHealth;

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> healthsCollected;

    public SerializableDictionary<string, bool> speedsCollected;

    public SerializableDictionary<string, bool> keysCollected;

    public List<Key.KeyType> keyList; // Add this line

    public SerializableDictionary<string, bool> openDoors;

    public SerializableDictionary<string, bool> notesCollected;
    public SerializableDictionary<string, MessageData> specialNotesData;

    public SerializableDictionary<string, bool> hiddenPartsCollected;

    public SerializableDictionary<string, PartData> hiddenPartsData;

    public List<bool> stageCompletionStatus;

    public string currentScene;

    public SerializableDictionary<string, bool> messagesCollected;

    public SerializableDictionary<string, bool> partsCollected;

    public SerializableDictionary<string, bool> triggerCollected;

    public SerializableDictionary<string, bool> triggerStageCollected;

    public SerializableDictionary<string, bool> triggerCompleteCollected;



    public bool cutscenePlayed;




    // the values defined in this constructor will be the default values
    // the game  starts with when there's no data to load
    public GameData()
    {
        this.currentHealth = 5;
       // playerPosition = Vector3.zero;
        healthsCollected = new SerializableDictionary<string, bool>();
        speedsCollected = new SerializableDictionary<string, bool>();
        keysCollected = new SerializableDictionary<string, bool>();

        keyList = new List<Key.KeyType>();

        openDoors = new SerializableDictionary<string, bool>();

        notesCollected = new SerializableDictionary<string, bool>();

        specialNotesData = new SerializableDictionary<string, MessageData>();

        hiddenPartsCollected = new SerializableDictionary<string, bool>();

        hiddenPartsData = new SerializableDictionary<string, PartData>();

         stageCompletionStatus = new List<bool> { false, false, false, false, false };
      // stageCompletionStatus = new List<bool> { true, true, true, true, true };

        currentScene = "SampleScene";

        messagesCollected = new SerializableDictionary<string, bool>();

        partsCollected = new SerializableDictionary<string, bool>();

        triggerCollected = new SerializableDictionary<string, bool>();

        triggerStageCollected = new SerializableDictionary<string, bool>();

        triggerCompleteCollected = new SerializableDictionary<string, bool>();

        cutscenePlayed = false;



        playerPosition = new Vector3(-12.0f, 1.0f, 0f);
    }

}

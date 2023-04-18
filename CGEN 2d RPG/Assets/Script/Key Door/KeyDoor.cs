using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string doorId;

    [ContextMenu("Generate guid for doorId")]
    private void GenerateGuid()
    {
        doorId = System.Guid.NewGuid().ToString();
    }

    private Animator animator;

    [SerializeField] private Key.KeyType keyType;

    private bool isOpen = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        isOpen = true;
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        isOpen = false;
        animator.SetBool("Open", false);
    }

    public void LoadData(GameData data, bool isRestarting)
    {
        data.openDoors.TryGetValue(doorId, out isOpen);
        if (isOpen)
        {
            if (isRestarting)
            {
                isOpen = false;
            }
            else
            {
                animator.SetBool("Open", true);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.openDoors.ContainsKey(doorId))
        {
            data.openDoors.Remove(doorId);
        }
        data.openDoors.Add(doorId, isOpen);
    }
}

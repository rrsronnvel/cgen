using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{

    private Animator animator;

    [SerializeField] private Key.KeyType keyType;

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
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}

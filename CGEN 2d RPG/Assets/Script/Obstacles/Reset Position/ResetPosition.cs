using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Animator animator;
    public GameObject[] objectsToReset; // The objects you want to reset
    public Vector3[] resetPositions; // The positions you want to reset to

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("NotPress");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.attachedRigidbody != null)
        {
            Debug.Log("Pressed");
            animator.Play("Pressed");
            for (int i = 0; i < objectsToReset.Length; i++) // Reset the position of each object
            {
                objectsToReset[i].transform.position = resetPositions[i];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.attachedRigidbody != null)
        {
            Debug.Log("NotPress");
            animator.Play("NotPress");
        }
    }
}

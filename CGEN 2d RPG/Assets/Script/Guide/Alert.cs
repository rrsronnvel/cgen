using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public GameObject panel;  // Assign this in Unity inspector
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            panel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

  
}

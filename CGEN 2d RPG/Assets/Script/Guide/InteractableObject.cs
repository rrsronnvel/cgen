using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable
{
    public GameObject panel;  // Assign this in Unity inspector
    [SerializeField] private GameObject controlButtons;

    // This function will be called when the player interacts with this object
    public void Interact()
    {
        panel.SetActive(true);
        controlButtons.SetActive(false); // Disable all control buttons
    }

    // This function can be linked to the close button to close the panel
    public void ClosePanel()
    {
        panel.SetActive(false);
        controlButtons.SetActive(true); // Enable all control buttons
    }
}

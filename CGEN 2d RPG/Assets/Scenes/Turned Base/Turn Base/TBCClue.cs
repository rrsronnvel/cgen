using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBCClue : MonoBehaviour
{
    // Reference to the UI Panel
    public GameObject uiPanel;
    [SerializeField] private GameObject controlButtons;
    // Start is called before the first frame update
    void Start()
    {
        

        // Initially, we want the UI Panel to be hidden
        uiPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This function is called when the player collides with the object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.tag == "Player")
        {
            // Show the UI Panel
            uiPanel.SetActive(true);
            controlButtons.SetActive(false);

            // Make the object disappear
            gameObject.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        uiPanel.SetActive(false);
        controlButtons.SetActive(true); // Enable all control buttons
    }
}

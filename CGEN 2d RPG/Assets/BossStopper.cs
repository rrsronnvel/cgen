using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStopper : MonoBehaviour
{
    public Animator animator;
    public static int stoppersPressed = 0; // Static variable to keep track of how many stoppers are pressed

    private bool isPressed = false; // Add this line

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("NotPress");

        // Reset the count of pressed stoppers
        stoppersPressed = 0; // Add this line
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.attachedRigidbody != null) && !isPressed) // Modify this line
        {
            animator.Play("Pressed");
            stoppersPressed++; // Increase the count of pressed stoppers
            isPressed = true; // Add this line
            Debug.Log("Buttons pressed: " + stoppersPressed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Player") || collision.attachedRigidbody != null) && isPressed) // Modify this line
        {
            animator.Play("NotPress");
            stoppersPressed--; // Decrease the count of pressed stoppers
            isPressed = false; // Add this line
            Debug.Log("Buttons pressed: " + stoppersPressed);
        }
    }
}

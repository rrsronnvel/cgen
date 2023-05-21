using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBackUp : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    public Animator animator; // Add a reference to the NPC's Animator
    private enum Direction { Left, Right, Down, Top }; // Add a Direction enum
    [SerializeField] private Direction defaultDirection; // Change the defaultDirection variable to use the Direction enum

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetDefaultDirection(); // Set the default direction at the start
    }

    public void Interact()
    {
        FacePlayer(); // Call the FacePlayer method when the player interacts with the NPC
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, null));
    }

    // Create a new method to make the NPC face a specific direction
    public void FaceDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            animator.Play("IdleLeft");
        }
        else if (direction.x < 0)
        {
            animator.Play("IdleRight");
        }
        else if (direction.y > 0)
        {
            animator.Play("IdleDown");
        }
        else if (direction.y < 0)
        {
            animator.Play("IdleTop");
        }
    }

    // Create a new method to set the default direction based on the defaultDirection enum value
    private void SetDefaultDirection()
    {
        switch (defaultDirection)
        {
            case Direction.Left:
                FaceDirection(Vector2.right);
                break;
            case Direction.Right:
                FaceDirection(Vector2.left);
                break;
            case Direction.Down:
                FaceDirection(Vector2.up);
                break;
            case Direction.Top:
                FaceDirection(Vector2.down);
                break;
        }
    }

    // Create a new method to make the NPC face the player
    public void FacePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Find the player object (make sure the player has the "Player" tag)
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            Vector2 playerDirection = new Vector2(playerController.animator.GetFloat("moveX"), playerController.animator.GetFloat("moveY"));

            // Play the required animation based on the player's direction

            if (playerDirection.y > 0) // Player is facing up
            {
                animator.Play("IdleDown");
            }
            else if (playerDirection.x > 0) // Player is facing right
            {
                animator.Play("IdleLeft");
            }
            else if (playerDirection.x < 0) // Player is facing left
            {
                animator.Play("IdleRight");
            }

            else if (playerDirection.y < 0) // Player is facing down
            {
                animator.Play("IdleTop");
            }
        }
    }

}
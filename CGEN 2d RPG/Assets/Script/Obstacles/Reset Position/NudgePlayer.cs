using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgePlayer : MonoBehaviour, Interactable
{
    public PlayerController player; // reference to the PlayerController

    public void Interact()
    {
        player.Nudge(new Vector3(0.01f, 0.01f, 0)); // move player slightly
    }
}

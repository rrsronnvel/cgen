using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    FreeRoam, Dialog, Battle
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.FreeRoam;
            }
            
        };
    }

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();

            // Check for pushable objects
            if (Input.GetButtonDown("InteractButton"))
            {
                var facingDir = new Vector3(playerController.animator.GetFloat("moveX"), playerController.animator.GetFloat("moveY"));
                var interactPos = playerController.transform.position + facingDir;
                var collider = Physics2D.OverlapCircle(interactPos, 0.2f, playerController.interactableLayer);

                if (collider != null)
                {
                    Interactable interactable = collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {

        }
    }
}

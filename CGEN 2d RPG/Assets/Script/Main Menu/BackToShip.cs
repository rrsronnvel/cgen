using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToShip : MonoBehaviour, Interactable
{
    [SerializeField] public Animator popupUIAnimator; // Reference to the animator of the Pop UI
    public string triggerName = "return"; // The name of the trigger to show the Pop UI



    public void Interact()
    {
        Debug.Log("Interacting");
        ShowPopUI();
    }

    private void ShowPopUI()
    {
        if (popupUIAnimator == null)
        {
            // Assuming the pop-up UI object has a unique name, replace "PopupUIObjectName" with the actual name
            GameObject popupUIObject = GameObject.Find("BackToShip");
            if (popupUIObject != null)
            {
                popupUIAnimator = popupUIObject.GetComponent<Animator>();
            }
        }

        if (popupUIAnimator != null)
        {
            popupUIAnimator.SetTrigger(triggerName);
        }
    }

}

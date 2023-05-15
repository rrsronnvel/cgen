using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignGuide : MonoBehaviour, Interactable
{
    [SerializeField] public Animator signGuidePopAnimator; // Reference to the animator of the Sign Guide Pop UI
    public string triggerName = "return"; // The name of the trigger to show the Sign Guide Pop UI

    public void Interact()
    {
        Debug.Log("Interacting");
        ShowSignGuidePop();
    }

    private void ShowSignGuidePop()
    {
        if (signGuidePopAnimator == null)
        {
            // Assuming the pop-up UI object has a unique name, replace "SignGuidePop" with the actual name
            GameObject signGuidePopObject = GameObject.Find("SignGuidePop");
            if (signGuidePopObject != null)
            {
                signGuidePopAnimator = signGuidePopObject.GetComponent<Animator>();
            }
        }

        if (signGuidePopAnimator != null)
        {
            signGuidePopAnimator.SetTrigger(triggerName);
        }
    }

    public void CloseSignGuidePop()
    {
        if (signGuidePopAnimator != null)
        {
            signGuidePopAnimator.SetTrigger("close");
        }
    }
}
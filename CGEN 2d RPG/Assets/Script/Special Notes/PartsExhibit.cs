using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartsExhibit : MonoBehaviour, Interactable
{
    public enum ExhibitPart
    {
        Part1,
        Part2,
        Part3,
        Part4,
        Part5
    }

    [SerializeField] public Animator partsExhibitPopAnimator; // Reference to the animator of the Parts Exhibit Pop UI
    public string triggerName = "return"; // The name of the trigger to show the Parts Exhibit Pop UI
    public ExhibitPart exhibitPart;

    private void Start()
    {
        switch (exhibitPart)
        {
            case ExhibitPart.Part1:
                if (StageManager.instance.stageCompletionStatus[0])
                {
                    transform.position = new Vector3(-6.92f, -10.96f, 0);
                }
                break;
            case ExhibitPart.Part2:
                if (StageManager.instance.stageCompletionStatus[1])
                {
                    transform.position = new Vector3(-4.7f, -10.96f, 0);
                }
                break;
                // Add similar cases for other parts here...
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        ShowPartsExhibitPop();
    }

    private void ShowPartsExhibitPop()
    {
        if (partsExhibitPopAnimator == null)
        {
            // Assuming the pop-up UI object has a unique name, replace "PartsExhibitPop" with the actual name
            GameObject partsExhibitPopObject = GameObject.Find("PartsExhibitPop");
            if (partsExhibitPopObject != null)
            {
                partsExhibitPopAnimator = partsExhibitPopObject.GetComponent<Animator>();
            }
        }

        if (partsExhibitPopAnimator != null)
        {
            partsExhibitPopAnimator.SetTrigger(triggerName);
        }
    }

    public void ClosePartsExhibitPop()
    {
        if (partsExhibitPopAnimator != null)
        {
            partsExhibitPopAnimator.SetTrigger("close");
        }
    }
}
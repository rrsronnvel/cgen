using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignGuide : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject Panel;

    public void Interact()
    {
        TogglePanel(true);
    }

    public void ClosePanel()
    {
        Debug.Log("ClosePanel called");
        TogglePanel(false);
    }

    private void TogglePanel(bool show)
    {
        Panel.SetActive(show);

        if (show)
        {
            PopupCloseOnClick popupCloseOnClick = Panel.GetComponent<PopupCloseOnClick>();
            if (popupCloseOnClick != null)
            {
                popupCloseOnClick.Initialize(ClosePanel);
            }
            else
            {
                Debug.LogError("PopupCloseOnClick component not found on the panel.");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TalkToSelf : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, null)); // Add 'this' reference to ShowDialog method
    }
   
}

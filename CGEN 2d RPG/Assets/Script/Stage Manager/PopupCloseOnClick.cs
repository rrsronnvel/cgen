using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PopupCloseOnClick : MonoBehaviour, IPointerClickHandler
{
    private Action closeAction;

    public void Initialize(Action closeAction)
    {
        this.closeAction = closeAction;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        closeAction?.Invoke();
    }
}

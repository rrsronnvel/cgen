using UnityEngine;
using UnityEngine.UI;

public class MovePanelToCenter : MonoBehaviour
{
    public Button movePanelButton;
    public Button noButton;
    private RectTransform panelRectTransform;

    void Start()
    {
        // Cache the panel's RectTransform
        panelRectTransform = GetComponent<RectTransform>();

        // Add a listener to the buttons
        movePanelButton.onClick.AddListener(MovePanelToScreenCenter);
        noButton.onClick.AddListener(MovePanelToRight);
    }

    void MovePanelToScreenCenter()
    {
        // Set the anchor to the middle
        panelRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        panelRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        // Set the pivot to the middle
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Set the anchored position to zero
        panelRectTransform.anchoredPosition = Vector2.zero;
    }

    void MovePanelToRight()
    {
        // Set the anchor to the middle-right
        panelRectTransform.anchorMin = new Vector2(1f, 0.5f);
        panelRectTransform.anchorMax = new Vector2(1f, 0.5f);

        // Set the pivot to the middle-left
        panelRectTransform.pivot = new Vector2(0f, 0.5f);

        // Set the anchored position to zero
        panelRectTransform.anchoredPosition = Vector2.zero;
    }
}

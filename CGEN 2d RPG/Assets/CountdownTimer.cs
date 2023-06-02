using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 60;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject winPanel; // Reference to your "Win" panel

    private void Start()
    {
        countdownText.text = countdownTime.ToString();
        winPanel.SetActive(false); // Ensure the panel is initially hidden
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
    }

    private void UpdateTimer()
    {
        countdownTime--;

        if (countdownTime <= 0)
        {
            countdownTime = 0;
            CancelInvoke("UpdateTimer"); // Stop the countdown
            winPanel.SetActive(true); // Show the panel when the countdown ends
        }

        countdownText.text = Mathf.Round(countdownTime).ToString();
    }
}

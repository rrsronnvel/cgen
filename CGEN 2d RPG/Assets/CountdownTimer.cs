using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float countdownTime = 60;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject winPanel; // Reference to your "Win" panel

    [SerializeField] private Boss1 boss; // Reference to your Boss1 script

    private void Start()
    {
        countdownText.text = countdownTime.ToString();
        winPanel.SetActive(false); // Ensure the panel is initially hidden
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);

        boss = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss1>();
    }

    private void UpdateTimer()
    {
        countdownTime--;

        if (countdownTime <= 0)
        {
            countdownTime = 0;
            CancelInvoke("UpdateTimer"); // Stop the countdown
            //winPanel.SetActive(true); // Show the panel when the countdown ends

            boss.Die(); // Trigger the boss's death animation
            StartCoroutine(ShowWinPanelAfterDelay(3)); // Show the win panel after a delay
        }

        countdownText.text = Mathf.Round(countdownTime).ToString();
    }

    private IEnumerator ShowWinPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winPanel.SetActive(true);
    }


}

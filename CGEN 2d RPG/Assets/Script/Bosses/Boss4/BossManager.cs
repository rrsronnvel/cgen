using UnityEngine;
using TMPro;

public class BossManager : MonoBehaviour
{
    public TMP_Text countdownText;
    public float countdownStart = 30f; // Change this to the length of your countdown
    private float countdownTime;

    private Boss4Chase boss4Chase;
    private Boss4Range boss4Range;

  

    void Start()
    {
        countdownTime = countdownStart;
        boss4Chase = FindObjectOfType<Boss4Chase>();
        boss4Range = FindObjectOfType<Boss4Range>();

        // Reset isAlive states for both bosses
        Boss4Chase.isAlive = true;
        Boss4Range.isAlive = true;

    }

    void Update()
    {
        if (countdownTime >0)
        {
            countdownTime -= Time.deltaTime;
        }
        else
        {
            countdownTime = 0; // Ensure countdownTime doesn't go below 0
            if (Boss4Chase.isAlive || Boss4Range.isAlive)
            {
                Boss4Chase.isAlive = false; // Use class name, not instance
                Boss4Range.isAlive = false; // Use class name, not instance
                boss4Chase.Die();
                boss4Range.KillBoss();
            }
        }
        countdownText.text = Mathf.CeilToInt(countdownTime).ToString("0"); // Round up and convert to string for displaying
    }

}

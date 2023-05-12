using System.Collections.Generic;
using UnityEngine;

public class FiretrapStopper : MonoBehaviour
{
    [SerializeField] private List<FiretrapDelay> firetraps;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.attachedRigidbody != null)
        {
            Debug.Log("Pause");
            PauseAllFiretraps();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.attachedRigidbody != null)
        {
            Debug.Log("Resume");
            ResumeAllFiretraps();
        }
    }

    private void PauseAllFiretraps()
    {
        foreach (var firetrap in firetraps)
        {
            firetrap.PauseTrap();
        }
    }

    private void ResumeAllFiretraps()
    {
        foreach (var firetrap in firetraps)
        {
            firetrap.ResumeTrap();
        }
    }
}

using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    void Start()
    {
        // Ensure that a PlayableDirector component is attached to this GameObject
        if (playableDirector == null)
        {
            Debug.LogError("No PlayableDirector component found on this GameObject.");
            return;
        }

        // Add a listener to the stopped event
        playableDirector.stopped += OnCutsceneStopped;
    }

    void OnDestroy()
    {
        // Remove the listener when the object is destroyed
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnCutsceneStopped;
        }
    }

    void OnCutsceneStopped(PlayableDirector director)
    {
        // Call RestartGame method from TestingRestart class
        TestingRestart.instance.RestartGame();
    }
}

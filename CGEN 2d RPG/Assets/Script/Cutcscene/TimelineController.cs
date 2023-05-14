using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string targetSceneName;

    private void Start()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }
}

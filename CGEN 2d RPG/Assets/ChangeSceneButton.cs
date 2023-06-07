using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        TestingRestart.instance.RestartGame();
        SceneManager.LoadScene(sceneName);
    }
}

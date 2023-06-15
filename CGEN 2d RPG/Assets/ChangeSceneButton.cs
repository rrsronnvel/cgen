using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public GameObject panel; // Add this line

    public void ChangeScene(string sceneName)
    {
        TestingRestart.instance.RestartGame();
        SceneManager.LoadScene(sceneName);
    }

    public void ClosePanel() // Add this method
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}

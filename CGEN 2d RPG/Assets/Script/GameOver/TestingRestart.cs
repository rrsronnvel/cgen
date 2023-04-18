using UnityEngine;
using UnityEngine.UI;

public class TestingRestart : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        DataPersistenceManager.instance.RestartGame();
    }
}

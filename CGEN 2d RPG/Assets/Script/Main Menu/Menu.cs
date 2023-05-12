using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    public static Menu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToMainMenu()
    {
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }

    public void BackShip()
    {

        // Call RestartGame method from TestingRestart class
        TestingRestart.instance.RestartGame();

        SceneManager.LoadScene("SampleScene1");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }
}

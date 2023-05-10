using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRestart : MonoBehaviour
{
    [Header("Pop up UI")]
    [SerializeField] private GameObject popUpUI;

    public TestingRestart testingRestart;
    private void ShowPopUpUI()
    {
        popUpUI.SetActive(true);
        TestingRestart.instance.SetupRestartButtons();

       
    }
}

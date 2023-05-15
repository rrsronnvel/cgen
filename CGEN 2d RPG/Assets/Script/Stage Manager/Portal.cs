using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Portal : MonoBehaviour, Interactable
{
    public int stageNumber; // Unique identifier for each stage
    public GameObject stageUI; // Reference to the Stage UI game object (Stage UI Panel in your hierarchy)

    private TextMeshProUGUI stageLevel;
    private TextMeshProUGUI stageDetail;
    private TextMeshProUGUI stageStatus;
    private TextMeshProUGUI stageDetail2;
    private Button enterButton;
    private Button closeButton;


    public string stageLevelText; // Text for the stage level (e.g., "Stage 1")
    public string stageDetailText; // Text for the stage detail (e.g., "Complete the puzzle to advance")
    public string stageDetail2Text;



    private void Initialize()
    {
        stageLevel = stageUI.transform.Find("Stage level").GetComponent<TextMeshProUGUI>();
        stageDetail = stageUI.transform.Find("Stage detail").GetComponent<TextMeshProUGUI>();
        stageDetail2 = stageUI.transform.Find("Stage detail2").GetComponent<TextMeshProUGUI>();
        stageStatus = stageUI.transform.Find("Stage status").GetComponent<TextMeshProUGUI>();
        enterButton = stageUI.transform.Find("Enter Button").GetComponent<Button>();
        closeButton = stageUI.transform.Find("Close Button").GetComponent<Button>();


        enterButton.onClick.AddListener(EnterStage);
        closeButton.onClick.AddListener(CloseStageUI);

    }

    private void Awake()
    {
        Initialize();
    }


    public void Interact()
    {
        stageLevel.text = stageLevelText;
        stageDetail.text = stageDetailText;
        stageDetail2.text = stageDetail2Text;


        if (StageManager.instance.IsStageUnlocked(stageNumber))
        {
            if (StageManager.instance.GetStageCompletionStatus(stageNumber))
            {
                stageStatus.text = "Completed";
            }
            else
            {
                stageStatus.text = "Unlocked";
            }
            enterButton.interactable = true;
        }
        else
        {
            stageStatus.text = "Locked";
            enterButton.interactable = false;
        }

        enterButton.onClick.RemoveAllListeners();
        closeButton.onClick.RemoveAllListeners();

        enterButton.onClick.AddListener(() => EnterStage());
        closeButton.onClick.AddListener(CloseStageUI);

        stageUI.SetActive(true);
    }



    private void EnterStage()
    {
        // Get the player's Health component
        Health playerHealth = FindObjectOfType<Health>();

        // Reset the player's health to full
        if (playerHealth != null)
        {
            playerHealth.ResetHealthToFull();
        }
        else
        {
            Debug.LogError("Player Health component not found.");
        }

        // Call RestartGame method from TestingRestart class
        TestingRestart.instance.RestartGame();

        string sceneName = "Stage" + stageNumber;
        Debug.Log("Trying to load scene: " + sceneName);
        SceneManager.LoadScene(sceneName);


    }




    private void CloseStageUI()
    {
        stageUI.SetActive(false);
    }

}
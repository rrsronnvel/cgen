using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionPanel : MonoBehaviour
{
    public Button okButton;
    [SerializeField] private TMP_Text instructionText;
    public string instructionString; // Add this line

    private void Start()
    {
        // Set the instruction text
        instructionText.text = instructionString; // Add this line

        // Pause the game
        Time.timeScale = 0;

        // Add a listener to the button to unpause the game and hide the panel when clicked
        okButton.onClick.AddListener(() =>
        {
            // Unpause the game
            Time.timeScale = 1;

            // Hide the panel
            gameObject.SetActive(false);
        });
    }
}

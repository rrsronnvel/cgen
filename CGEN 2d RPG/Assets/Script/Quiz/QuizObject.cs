using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswer;
    public Texture2D questionImage; // optional
}

public class QuizObject : MonoBehaviour, Interactable
{
    public GameObject panelQuiz;  // Assign this in Unity inspector
    [SerializeField] private GameObject controlButtons;

    public QuizQuestion[] questions;  // Assign your questions in Unity inspector
    private QuizQuestion currentQuestion;

    [SerializeField]  private QuizDoor door;  // Reference to QuizDoor script. Assign this in Unity inspector.

    // Reference to UI text elements
    public TMP_Text questionText;  // Change this
    public TMP_Text[] answerTexts;  // And this

    // Reference to UI Image for question image
    public Image questionImage; // optional

    [SerializeField] protected float damage;

    public Health playerHealth; // Reference to Health script. Assign this in Unity inspector.

    // This function will be called when the player interacts with this object
    public void Interact()
    {
        panelQuiz.SetActive(true);
        controlButtons.SetActive(false); // Disable all control buttons

        // Randomly select a question and display it
        currentQuestion = questions[Random.Range(0, questions.Length)];
        DisplayQuestion();
    }

    // This function can be linked to the close button to close the panel
    public void ClosePanel()
    {
        panelQuiz.SetActive(false);
        controlButtons.SetActive(true); // Enable all control buttons
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.question;

        // Display the answers
        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            answerTexts[i].text = currentQuestion.answers[i];
        }

        // Display the question image
        if (currentQuestion.questionImage != null)
        {
            questionImage.sprite = Sprite.Create(currentQuestion.questionImage, new Rect(0.0f, 0.0f, currentQuestion.questionImage.width, currentQuestion.questionImage.height), new Vector2(0.5f, 0.5f), 100.0f);
            questionImage.gameObject.SetActive(true);  // Make sure the image is visible
        }
        else
        {
            questionImage.gameObject.SetActive(false);  // Hide the image
        }
    }


    // This function will be called when an answer is selected
    public void AnswerSelected(int answerIndex)
    {
        if (answerIndex == currentQuestion.correctAnswer)
        {
            // The answer is correct, handle accordingly
            Debug.Log("Correct answer!");
            door.OpenDoor();
            // Here you could add the logic to unlock a door, give a reward, etc.
            // After that, close the panel
            ClosePanel();
        }
        else
        {
            // The answer is incorrect, handle accordingly
            Debug.Log("Incorrect answer!");
            playerHealth.TakeDamage(damage);
            // You could give feedback to the player, subtract points, etc.
            StartCoroutine(ReshowPanelAfterIncorrectAnswer());
        }
    }

    IEnumerator ReshowPanelAfterIncorrectAnswer()
    {
        ClosePanel();
        yield return new WaitForSeconds(0.5f); // Wait for 1 second
        Interact(); // Reopen the panel
    }

}

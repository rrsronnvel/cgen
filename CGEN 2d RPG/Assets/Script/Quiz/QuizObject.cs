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
    [Header("Object Reference")]
    public GameObject panelQuiz;  // Assign this in Unity inspector
    [SerializeField] private GameObject controlButtons;

    [SerializeField]  private QuizDoor door;  // Reference to QuizDoor script. Assign this in Unity inspector.

    // Reference to UI text elements
    public TMP_Text questionTextOnly; // Assign in Unity inspector
    public TMP_Text questionTextWithImage; // Assign in Unity inspector

    public TMP_Text[] answerTexts;  // And this
    // Reference to UI Image for question image
    public Image questionImage; // optional

    public Health playerHealth; // Reference to Health script. Assign this in Unity inspector.
    [SerializeField] protected float damage;

    [Header("Question and Choices")]
    public QuizQuestion[] questions;  // Assign your questions in Unity inspector
    private QuizQuestion currentQuestion;

    private bool isFirstTime = true;

    public GameObject questionOnlyPanel; // Assign in Unity inspector
    public GameObject questionWithImagePanel; // Assign in Unity inspector


    private void Awake()
    {
        questionTextOnly.enableAutoSizing = false;
        questionTextOnly.enableWordWrapping = true;

        questionTextWithImage.enableAutoSizing = false;
        questionTextWithImage.enableWordWrapping = true;


    }

    // This function will be called when the player interacts with this object
    public void Interact()
    {
        panelQuiz.SetActive(true);
        controlButtons.SetActive(false); // Disable all control buttons

        // Randomly select a question and display it
        // Only select a new question the first time or after a wrong answer
        if (isFirstTime)
        {
            currentQuestion = questions[Random.Range(0, questions.Length)];
            isFirstTime = false;
        }

        DisplayQuestion();
        CheckAndResizeText();
    }

    // This function can be linked to the close button to close the panel
    public void ClosePanel()
    {
        panelQuiz.SetActive(false);
        controlButtons.SetActive(true); // Enable all control buttons
    }

    private void DisplayQuestion()
    {
        questionTextOnly.text = currentQuestion.question.Replace("<br>", "\n");
        questionTextOnly.text = currentQuestion.question;

        questionTextWithImage.text = currentQuestion.question.Replace("<br>", "\n");
        questionTextWithImage.text = currentQuestion.question;

        // Display the answers
        for (int i = 0; i < currentQuestion.answers.Length; i++)
        {
            answerTexts[i].text = currentQuestion.answers[i];
        }

        // Display the question image
        if (currentQuestion.questionImage != null)
        {
            questionWithImagePanel.SetActive(true);
            questionOnlyPanel.SetActive(false);

            questionTextWithImage.text = currentQuestion.question.Replace("<br>", "\n");
            questionTextWithImage.text = currentQuestion.question;

            questionImage.sprite = Sprite.Create(currentQuestion.questionImage, new Rect(0.0f, 0.0f, currentQuestion.questionImage.width, currentQuestion.questionImage.height), new Vector2(0.5f, 0.5f), 100.0f);
            questionImage.gameObject.SetActive(true);  // Make sure the image is visible
        }
        else
        {
            questionWithImagePanel.SetActive(false);
            questionOnlyPanel.SetActive(true);

            questionTextOnly.text = currentQuestion.question.Replace("<br>", "\n");
            questionTextOnly.text = currentQuestion.question;

            questionImage.gameObject.SetActive(false);  // Hide the image
        }

        Invoke(nameof(CheckAndResizeText), 0.1f);
    }



    private void CheckAndResizeText()
    {
        if (questionTextOnly.preferredHeight > questionTextOnly.rectTransform.rect.height)
        {
            questionTextOnly.enableAutoSizing = true;
        }

        if (questionTextWithImage.preferredHeight > questionTextWithImage.rectTransform.rect.height)
        {
            questionTextWithImage.enableAutoSizing = true;
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
           // playerHealth.TakeDamage(damage);
            // You could give feedback to the player, subtract points, etc.
            StartCoroutine(ReshowPanelAfterIncorrectAnswer());
        }
    }

    IEnumerator ReshowPanelAfterIncorrectAnswer()
    {
        ClosePanel();
        yield return new WaitForSeconds(0.5f);
        isFirstTime = true; // Allow new question selection after a wrong answer
        Interact();
    }

}

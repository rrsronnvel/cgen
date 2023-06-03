using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComputerHistoryBattle : MonoBehaviour
{
    public Text questionText; // Change this to TMP_Text if you're using TextMeshPro
    public Button[] answerButtons; // An array of Buttons for the answer choices
    public ComputerHistoryQuestionBank questionBank; // The question bank

    private List<ComputerHistoryQuestion> questions = new List<ComputerHistoryQuestion>();
    private ComputerHistoryQuestion currentQuestion;

    public BattleSystem battleSystem;


    void Start()
    {
        // Display the first question
        DisplayQuestion(GetRandomQuestion());
    }

    public ComputerHistoryQuestion GetRandomQuestion()
    {
        int randomIndex = UnityEngine.Random.Range(0, questionBank.questions.Count);
        return questionBank.questions[randomIndex];
    }

    public void DisplayQuestion(ComputerHistoryQuestion question)
    {
        // Set the current question
        currentQuestion = question;

        // Display the question text
        questionText.text = question.question;

        // Display the answer choices
        for (int i = 0; i < question.answers.Length; i++)
        {
            // Make sure the button is enabled
            answerButtons[i].gameObject.SetActive(true);

            // Get the TMP_Text component of the button
            TMP_Text buttonText = answerButtons[i].GetComponentInChildren<TMP_Text>();

            // Set the button text to the answer choice
            buttonText.text = question.answers[i];

            // Create a new variable to capture the current value of i
            int answerIndex = i;

            // Add a click listener to the button
            answerButtons[i].onClick.AddListener(() => OnAnswerButtonClicked(answerIndex));
        }

    }

    public void OnAnswerButtonClicked(int buttonIndex)
    {
        // Check if the player's answer is correct
        if (buttonIndex == currentQuestion.correctAnswerIndex)
        {
            // The player answered correctly
            Debug.Log("Correct answer!");

            // Set the dialogue text
            battleSystem.dialogueText.text = "Your answer is correct. Now you could attack the enemy.";
        }
        else
        {
            // The player answered incorrectly
            Debug.Log("Incorrect answer.");

            // Set the dialogue text
            battleSystem.dialogueText.text = "You got the wrong answer. You couldn't attack this round.";

            // Disable the attack button
            battleSystem.attackButton.interactable = false;

            // It's the enemy's turn
            battleSystem.StartCoroutine(battleSystem.EnemyTurn());
        }

        // Remove the click listeners from the buttons
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        // Deactivate the Quiz Panel
        gameObject.SetActive(false);
    }




    public void StartQuiz()
    {
        // Activate the Quiz Panel
        gameObject.SetActive(true);

        // Display the first question
        DisplayQuestion(GetRandomQuestion());
    }

}

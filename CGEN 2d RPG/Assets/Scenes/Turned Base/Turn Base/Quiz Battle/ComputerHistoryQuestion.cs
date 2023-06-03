using UnityEngine;
using UnityEngine.UI; // If you're using Unity's legacy UI system
// using TMPro; // Uncomment this line if you're using TextMeshPro

public class ComputerHistoryQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;

    public ComputerHistoryQuestion(string question, string[] answers, int correctAnswerIndex)
    {
        this.question = question;
        this.answers = answers;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}
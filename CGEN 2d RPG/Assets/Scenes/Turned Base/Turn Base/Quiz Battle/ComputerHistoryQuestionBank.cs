using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionBank", menuName = "Quiz/Question Bank", order = 1)]
public class ComputerHistoryQuestionBank : ScriptableObject
{
    public List<ComputerHistoryQuestion> questions = new List<ComputerHistoryQuestion>();

    void OnEnable()
    {
        // Add questions to the list
        questions.Add(new ComputerHistoryQuestion(
            "What was the first generation of computers characterized by?",
            new string[] { "Vacuum tubes and magnetic drums", "Transistors", "Integrated circuits", "Microprocessors" },
            0));

        questions.Add(new ComputerHistoryQuestion(
            "Which generation of computers is characterized by microprocessors?",
            new string[] { "First generation", "Second generation", "Third generation", "Fourth generation" },
            3));

        questions.Add(new ComputerHistoryQuestion(
            "What was the first commercially successful computer?",
            new string[] { "ENIAC", "UNIVAC", "IBM 701", "PDP-1" },
            1));

        questions.Add(new ComputerHistoryQuestion(
            "What was the first personal computer?",
            new string[] { "IBM PC", "Apple I", "Commodore PET", "Altair 8800" },
            3));

        questions.Add(new ComputerHistoryQuestion(
            "Who is considered the father of the computer?",
            new string[] { "Bill Gates", "Steve Jobs", "Charles Babbage", "Alan Turing" },
            2));
    }
}

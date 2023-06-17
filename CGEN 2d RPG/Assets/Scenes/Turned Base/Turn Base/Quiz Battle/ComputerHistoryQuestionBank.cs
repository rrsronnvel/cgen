using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionBank", menuName = "Quiz/Question Bank", order = 1)]
public class ComputerHistoryQuestionBank : ScriptableObject
{
    public List<ComputerHistoryQuestion> questions = new List<ComputerHistoryQuestion>();
}

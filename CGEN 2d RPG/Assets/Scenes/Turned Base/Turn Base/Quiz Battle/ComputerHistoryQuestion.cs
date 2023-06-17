[System.Serializable]
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

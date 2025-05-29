namespace QuizzApp.Models.DTOs;

public class SubmitAnswerDto
{
    public int QuizResultId { get; set; }
    public int QuestionId { get; set; }
    public int AnswerSelectedId { get; set; }
}

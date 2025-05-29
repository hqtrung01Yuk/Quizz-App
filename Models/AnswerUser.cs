namespace QuizzApp.Models
{
    public class AnswerUser
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int AnswerSelectedId { get; set; }
        public Answer AnswerSelected { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public int QuizResultId { get; set; }
        public QuizResult QuizResult { get; set; } = null!;
    }

}
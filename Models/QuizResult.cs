namespace QuizzApp.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        public ICollection<AnswerUser> AnswerUsers { get; set; } = new List<AnswerUser>();
    }
}
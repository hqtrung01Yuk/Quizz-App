namespace QuizzApp.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public double PassingRate { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;

        // 👇 Navigation tới các câu trả lời
        public ICollection<Answer> Answers { get; set; }

        // 👇 Nếu có liên kết với AnswerUser
        public ICollection<AnswerUser> AnswerUsers { get; set; }
    }

}
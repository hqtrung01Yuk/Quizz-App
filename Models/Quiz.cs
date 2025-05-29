namespace QuizzApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double PassingRate { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }

}
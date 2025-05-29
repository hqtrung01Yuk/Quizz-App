using QuizzApp.Models;

namespace QuizzApp.Repository;

public interface IQuizRepository
{
    Task<List<Question>> GetQuestionsAsync(int quizId);
    Task<Answer?> GetAnswerAsync(int questionId, int answerId);
    Task<Quiz?> GetQuizAsync(int quizId);
    Task<QuizResult?> GetQuizResultAsync(int quizResultId);
    Task<QuizResult> CreateQuizResultAsync(int quizId);
    Task SaveAnswerUserAsync(AnswerUser answerUser);
    Task EndQuizAsync(int quizResultId);
}

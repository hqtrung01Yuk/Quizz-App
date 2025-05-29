using QuizzApp.Models.DTOs;

namespace QuizzApp.Service;

public interface IQuizService
{
    Task<object> GetQuestionsAsync(int quizId);
    Task<object> SubmitAnswerAsync(SubmitAnswerDto dto);
    Task<object> GetQuizResultAsync(int quizResultId);
    Task<int> StartQuizAsync(int quizId);
    Task EndQuizAsync(int quizResultId);
}

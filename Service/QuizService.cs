using QuizzApp.Models;
using QuizzApp.Models.DTOs;
using QuizzApp.Repository;
using Microsoft.Extensions.Logging;

namespace QuizzApp.Service;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _repo;
    private readonly ILogger<QuizService> _logger;

    public QuizService(IQuizRepository repo, ILogger<QuizService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<object> GetQuestionsAsync(int quizId)
    {
        var questions = await _repo.GetQuestionsAsync(quizId);
        return questions.Select(q => new
        {
            q.Id,
            q.Text,
            Answers = q.Answers.Select(a => new { a.Id, a.Text })
        });
    }

    public async Task<object> SubmitAnswerAsync(SubmitAnswerDto dto)
    {
        _logger.LogInformation("Submitting answer for QuestionId: {QuestionId}, AnswerSelectedId: {AnswerSelectedId}, QuizResultId: {QuizResultId}",
            dto.QuestionId, dto.AnswerSelectedId, dto.QuizResultId);

        var answer = await _repo.GetAnswerAsync(dto.QuestionId, dto.AnswerSelectedId);
        if (answer == null)
        {
            _logger.LogWarning("Invalid answer submitted for QuestionId: {QuestionId}, AnswerSelectedId: {AnswerSelectedId}",
                dto.QuestionId, dto.AnswerSelectedId);
            return new { Error = "Invalid answer." };
        }

        var answerUser = new AnswerUser
        {
            QuestionId = dto.QuestionId,
            AnswerSelectedId = dto.AnswerSelectedId,
            IsCorrect = answer.IsCorrect,
            QuizResultId = dto.QuizResultId
        };

        await _repo.SaveAnswerUserAsync(answerUser);

        _logger.LogInformation("Answer saved for QuestionId: {QuestionId} with correctness: {IsCorrect}", dto.QuestionId, answer.IsCorrect);

        return new { IsCorrect = answer.IsCorrect };
    }

    public async Task<object> GetQuizResultAsync(int quizResultId)
    {
        _logger.LogInformation("Getting quiz result for quizResultId: {QuizResultId}", quizResultId);

        var result = await _repo.GetQuizResultAsync(quizResultId);
        if (result == null)
        {
            _logger.LogWarning("Quiz result not found for quizResultId: {QuizResultId}", quizResultId);
            return new { Error = "Result not found." };
        }

        int correct = result.AnswerUsers.Count(a => a.IsCorrect);
        int total = result.Quiz.Questions.Count;
        TimeSpan duration = result.EndTime - result.StartTime;

        result.Score = correct;
        result.Passed = ((double)correct / total) >= result.Quiz.PassingRate;

        _logger.LogInformation("Quiz result calculated for quizResultId: {QuizResultId}. Passed: {Passed}, Score: {Score}",
            quizResultId, result.Passed, result.Score);

        return new
        {
            result.Passed,
            Duration = duration.ToString(@"mm\:ss"),
            CorrectAnswers = correct,
            IncorrectAnswers = total - correct,
            Answers = result.AnswerUsers.Select(au => new
            {
                Question = au.Question.Text,
                SelectedAnswer = au.AnswerSelected.Text,
                IsCorrect = au.IsCorrect
            })
        };
    }

    public async Task<int> StartQuizAsync(int quizId)
    {
        _logger.LogInformation("Starting quiz for quizId: {QuizId}", quizId);

        var quiz = await _repo.GetQuizAsync(quizId);
        if (quiz == null)
        {
            _logger.LogWarning("Quiz with id {QuizId} not found", quizId);
            throw new ArgumentException($"Quiz with id {quizId} does not exist.");
        }

        var result = await _repo.CreateQuizResultAsync(quizId);

        _logger.LogInformation("Quiz started with quizResultId: {QuizResultId}", result.Id);

        return result.Id;
    }

    public async Task EndQuizAsync(int quizResultId)
    {
        _logger.LogInformation("Ending quiz for quizResultId: {QuizResultId}", quizResultId);

        await _repo.EndQuizAsync(quizResultId);

        _logger.LogInformation("Quiz ended for quizResultId: {QuizResultId}", quizResultId);
    }
}

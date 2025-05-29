using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizzApp.Data;
using QuizzApp.Models;

namespace QuizzApp.Repository;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<QuizRepository> _logger;

    public QuizRepository(AppDbContext context, ILogger<QuizRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Question>> GetQuestionsAsync(int quizId)
    {
        return await _context.Questions
            .Where(q => q.QuizId == quizId)
            .Include(q => q.Answers)
            .ToListAsync();
    }

    public async Task<Answer?> GetAnswerAsync(int questionId, int answerId)
    {
        _logger.LogInformation("Fetching answer with answerId: {AnswerId} for questionId: {QuestionId}", answerId, questionId);
        var answer = await _context.Answers
            .FirstOrDefaultAsync(a => a.Id == answerId && a.QuestionId == questionId);
        if (answer == null)
        {
            _logger.LogWarning("Answer not found for answerId: {AnswerId} and questionId: {QuestionId}", answerId, questionId);
        }
        return answer;
    }

    public async Task<Quiz?> GetQuizAsync(int quizId)
    {
        _logger.LogInformation("Fetching quiz with quizId: {QuizId}", quizId);
        var quiz = await _context.Quizzes.FindAsync(quizId);
        if (quiz == null)
        {
            _logger.LogWarning("Quiz not found with quizId: {QuizId}", quizId);
        }
        return quiz;
    }

    public async Task<QuizResult?> GetQuizResultAsync(int quizResultId)
    {
        _logger.LogInformation("Fetching quiz result for quizResultId: {QuizResultId}", quizResultId);
        var result = await _context.QuizResults
            .Include(q => q.AnswerUsers)
                .ThenInclude(au => au.Question)
            .Include(q => q.Quiz)
                .ThenInclude(q => q.Questions)
            .Include(q => q.AnswerUsers)
                .ThenInclude(au => au.AnswerSelected)
            .FirstOrDefaultAsync(q => q.Id == quizResultId);

        if (result == null)
        {
            _logger.LogWarning("Quiz result not found for quizResultId: {QuizResultId}", quizResultId);
        }
        return result;
    }

    public async Task<QuizResult> CreateQuizResultAsync(int quizId)
    {
        _logger.LogInformation("Creating new quiz result for quizId: {QuizId}", quizId);
        var quizResult = new QuizResult
        {
            QuizId = quizId,
            StartTime = DateTime.UtcNow
        };
        _context.QuizResults.Add(quizResult);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created quiz result with id: {QuizResultId}", quizResult.Id);
        return quizResult;
    }

    public async Task SaveAnswerUserAsync(AnswerUser answerUser)
    {
        _logger.LogInformation("Saving answer user data for questionId: {QuestionId}, answerSelectedId: {AnswerSelectedId}, quizResultId: {QuizResultId}",
            answerUser.QuestionId, answerUser.AnswerSelectedId, answerUser.QuizResultId);
        _context.AnswerUsers.Add(answerUser);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Answer user saved successfully for quizResultId: {QuizResultId}", answerUser.QuizResultId);
    }

    public async Task EndQuizAsync(int quizResultId)
    {
        _logger.LogInformation("Ending quiz for quizResultId: {QuizResultId}", quizResultId);
        var result = await _context.QuizResults.FindAsync(quizResultId);
        if (result != null)
        {
            result.EndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Quiz ended for quizResultId: {QuizResultId}", quizResultId);
        }
        else
        {
            _logger.LogWarning("QuizResult not found for quizResultId: {QuizResultId}", quizResultId);
        }
    }
}

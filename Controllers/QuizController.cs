using Microsoft.AspNetCore.Mvc;
using QuizzApp.Models.DTOs;
using QuizzApp.Service;

namespace QuizzApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IQuizService _service;

    public QuizController(IQuizService service)
    {
        _service = service;
    }

    [HttpGet("{quizId}/questions")]
    public async Task<IActionResult> GetQuestions(int quizId)
        => Ok(await _service.GetQuestionsAsync(quizId));

    [HttpPost("submit-answer")]
    public async Task<IActionResult> SubmitAnswer([FromBody] SubmitAnswerDto dto)
        => Ok(await _service.SubmitAnswerAsync(dto));

    [HttpGet("result/{quizResultId}")]
    public async Task<IActionResult> GetResult(int quizResultId)
        => Ok(await _service.GetQuizResultAsync(quizResultId));

    [HttpPost("{quizId}/start")]
    [Produces("application/json")]
    public async Task<IActionResult> StartQuiz(int quizId)
        => Ok(new { QuizResultId = await _service.StartQuizAsync(quizId) });

    [HttpPost("result/{quizResultId}/end")]
    public async Task<IActionResult> EndQuiz(int quizResultId)
    {
        await _service.EndQuizAsync(quizResultId);
        return Ok();
    }
}

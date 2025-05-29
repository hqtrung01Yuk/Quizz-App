using Microsoft.EntityFrameworkCore;
using QuizzApp.Data;
using QuizzApp.Repository;
using QuizzApp.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Thêm Swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Thêm SQLite service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// đăng kí Service
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();

var app = builder.Build();

// Lấy logger trực tiếp từ DI container
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Ứng dụng đã bắt đầu chạy.");

// Dùng Swagger và Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var quiz = context.Quizzes.FirstOrDefault() ?? new QuizzApp.Models.Quiz { Title = "HTML/CSS/JS/C# Quiz", PassingRate = 0.6 };
    if (quiz.Id == 0)
    {
        context.Quizzes.Add(quiz);
        await context.SaveChangesAsync();
    }
    await QuestionSeed.SeedQuestions(context, quiz.Id);
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

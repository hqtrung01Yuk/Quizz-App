using QuizzApp.Models;

namespace QuizzApp.Data;

public static class QuestionSeed
{
    public static async Task SeedQuestions(AppDbContext context, int quizId)
    {
        if (context.Questions.Any()) return;

        var questions = new List<Question>
        {
            new Question
            {
                Text = "What does HTML stand for?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "Hyper Text Markup Language", IsCorrect = true },
                    new() { Text = "Home Tool Markup Language", IsCorrect = false },
                    new() { Text = "Hyperlinks and Text Markup Language", IsCorrect = false },
                    new() { Text = "Hyper Tool Multi Language", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which HTML element is used to define internal CSS?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "<style>", IsCorrect = true },
                    new() { Text = "<css>", IsCorrect = false },
                    new() { Text = "<script>", IsCorrect = false },
                    new() { Text = "<link>", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which CSS property controls the text size?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "font-size", IsCorrect = true },
                    new() { Text = "text-size", IsCorrect = false },
                    new() { Text = "font-style", IsCorrect = false },
                    new() { Text = "text-style", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "How do you select an element with id 'demo' in CSS?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "#demo", IsCorrect = true },
                    new() { Text = ".demo", IsCorrect = false },
                    new() { Text = "demo", IsCorrect = false },
                    new() { Text = "*demo", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which event occurs when the user clicks on an HTML element in JS?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "onclick", IsCorrect = true },
                    new() { Text = "onchange", IsCorrect = false },
                    new() { Text = "onmouseclick", IsCorrect = false },
                    new() { Text = "onmouseover", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which keyword is used to declare a variable in JavaScript?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "let", IsCorrect = true },
                    new() { Text = "int", IsCorrect = false },
                    new() { Text = "var", IsCorrect = false },
                    new() { Text = "String", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which symbol is used for comments in C#?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "//", IsCorrect = true },
                    new() { Text = "<!-- -->", IsCorrect = false },
                    new() { Text = "#", IsCorrect = false },
                    new() { Text = "/* */", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "What does 'async' mean in C#?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "Allows a method to be awaited", IsCorrect = true },
                    new() { Text = "Defines a static method", IsCorrect = false },
                    new() { Text = "Makes a method synchronous", IsCorrect = false },
                    new() { Text = "Marks method for encryption", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "Which method is used to query a table in EF Core?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "DbSet.ToList()", IsCorrect = true },
                    new() { Text = "DbSet.Read()", IsCorrect = false },
                    new() { Text = "DbSet.FindAll()", IsCorrect = false },
                    new() { Text = "DbSet.SelectAll()", IsCorrect = false }
                }
            },
            new Question
            {
                Text = "What is the default HTTP method for form submission in HTML?",
                PassingRate = 1.0,
                QuizId = quizId,
                Answers = new List<Answer>
                {
                    new() { Text = "GET", IsCorrect = true },
                    new() { Text = "POST", IsCorrect = false },
                    new() { Text = "PUT", IsCorrect = false },
                    new() { Text = "DELETE", IsCorrect = false }
                }
            }
        };

        await context.Questions.AddRangeAsync(questions);
        await context.SaveChangesAsync();
    }
}

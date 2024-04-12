namespace EducationalQuizApp.Utilities
{
    using EducationalQuizApp.Model;
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;

    public class QuizEvaluator
    {
        public bool IsAnswerCorrect(Question question, int selectedAnswerIndex)
        {
            return question.Answers[selectedAnswerIndex].IsCorrect;
        }

        // Add more evaluation functions as needed (e.g., calculating scores)
    }

}


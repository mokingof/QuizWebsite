using EducationalQuizApp.Model;
using EducationalQuizApp.Utilities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace EducationalQuizApp.Services
{
    public class QuizStateManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly QuizService _quizService;

        public QuizStateManager(IHttpContextAccessor httpContextAccessor, QuizService quizService)
        {
            _httpContextAccessor = httpContextAccessor;
            _quizService = quizService;
        }
        public void SaveCurrentQuiz(Quiz quiz)
        {
            _httpContextAccessor.HttpContext.Session.Set("CurrentQuiz", quiz);
        }
        public Quiz GetCurrentQuiz()
        {
            return _httpContextAccessor.HttpContext.Session.Get<Quiz>("CurrentQuiz");
        }
        public void SetQuizCategory(string category)
        {

            _httpContextAccessor.HttpContext.Session.SetString("QuizCategory", category);
            SaveCurrentQuiz(_quizService.GetQuizByCategory(category));
        }
        public bool IsQuizComplete()
        {
            var currentQuiz = GetCurrentQuiz();
            if (currentQuiz == null)
                return false;

            int currentQuestionIndex = GetCurrentQuestionIndex();
            return currentQuestionIndex >= currentQuiz.Questions.Count;
        }

        public string GetQuizCategory()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("QuizCategory");
        }

        public void ClearQuizData()
        {
            _httpContextAccessor.HttpContext.Session.Remove("CurrentQuiz");
            _httpContextAccessor.HttpContext.Session.Remove("QuizCategory");
        }

        public bool IsQuizDataAvailable()
        {
            return GetCurrentQuiz() != null;
        }

        public int GetCurrentQuestionIndex()
        {
            // Retrieves the current question index from the session.
            return _httpContextAccessor.HttpContext.Session.GetInt32("CurrentQuestionIndex") ?? 0;
        }

        public void SetCurrentQuestionIndex(int index)
        {
            // Stores the current question index in the session.
            _httpContextAccessor.HttpContext.Session.SetInt32("CurrentQuestionIndex", index);
        }

        public void AdvanceToNextQuestion()
        {
            var currentQuestionIndex = GetCurrentQuestionIndex();
            SetCurrentQuestionIndex(currentQuestionIndex + 1);
        }

        // Resets the quiz state, such as setting the current question index back to 0
        public void ResetQuiz()
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("CurrentQuestionIndex", 0);
            _httpContextAccessor.HttpContext.Session.Remove("CurrentScore");
        }

        public int GetUserScore()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("CurrentScore") ?? 0;
        }

        public void updateUserScore(int scoreToAdd)
        {
            var currentScore = GetUserScore();
            _httpContextAccessor.HttpContext.Session.SetInt32("CurrentScore", currentScore + scoreToAdd);
        }

    }

}

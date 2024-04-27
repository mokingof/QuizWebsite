﻿using EducationalQuizApp.Model;
using EducationalQuizApp.Utilities;
using System.Text.Json;
using Newtonsoft.Json;
namespace EducationalQuizApp.Services
{
    public class QuizStateManager
    {
        private const string UserAnswerKey = "UserAnswers";     
        private const string QuizCategoryKey = "QuizCategory";
        private const string CurrentQuizKey = "CurrentQuiz";
        private const string CurrentQuestionIndexKey = "CurrentQuestionIndex";
        private const string CurrentScoreKey = "CurrentScore";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly QuizService _quizService;

        public QuizStateManager(IHttpContextAccessor httpContextAccessor, QuizService quizService)
        {
            _httpContextAccessor = httpContextAccessor;
            _quizService = quizService;
        }

        // Dictionary to store answers with the question ID as the key
        public void SaveAnswer(int questionId, int answerId)
        {
            Dictionary<int, int> answers = GetUserAnswers() ?? new Dictionary<int, int>();
            answers[questionId] = answerId;
            _httpContextAccessor.HttpContext.Session.SetString("UserAnswers", JsonConvert.SerializeObject(answers));
        }

        public Dictionary<int, int> GetUserAnswers()
        {
            string data = _httpContextAccessor.HttpContext.Session.GetString("UserAnswers");
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<Dictionary<int,int>>(data);
            }
            return new Dictionary<int, int>();
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
       
        public string GetQuizCategory()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("QuizCategory");
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
            _httpContextAccessor.HttpContext.Session.Remove("UserAnswers");
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

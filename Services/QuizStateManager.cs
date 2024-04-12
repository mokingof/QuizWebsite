namespace EducationalQuizApp.Services
{
    public class QuizStateManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuizStateManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public int GetCurrentScore()
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32("CurrentScore") ?? 0;
        }

        public void UpdateScore(int scoreToAdd)
        {
            var currentScore = GetCurrentScore();
            _httpContextAccessor.HttpContext.Session.SetInt32("CurrentScore", currentScore + scoreToAdd);
        }

    }


}

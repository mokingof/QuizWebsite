using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using System.Collections.Generic;

public class QuizManager
{
    private readonly QuizService _quizService;
    private readonly QuizStateManager _quizStateManager;

    public QuizManager(QuizService quizService, QuizStateManager quizStateManager)
    {
        _quizService = quizService;
        _quizStateManager = quizStateManager;
    }

    // Fetch a quiz for a given category and store it in the session
    public Quiz GetQuizForCategory(string category)
    {
        if (category != _quizStateManager.GetQuizCategory())
        {
            _quizStateManager.SetQuizCategory(category);
            var quiz = _quizService.GetQuizByCategory(category);
            _quizStateManager.SaveCurrentQuiz(quiz);
            _quizStateManager.ResetQuiz(); // Resets state relevant to the quiz, such as current question index and score
            return quiz;
        }
        return _quizStateManager.GetCurrentQuiz();
    }

    // Evaluate an answer and return true if correct, false otherwise
    public bool SubmitAnswer(Quiz quiz, int answerIndex)
    {
        if (quiz == null) return false;

        var currentQuestion = quiz.Questions.ElementAtOrDefault(_quizStateManager.GetCurrentQuestionIndex());
        if (currentQuestion != null && answerIndex >= 0 && answerIndex < currentQuestion.Answers.Count)
        {
            var isCorrect = currentQuestion.Answers[answerIndex].IsCorrect;
            if (isCorrect)
            {
                _quizStateManager.UpdateScore(1);  // Increase score by 1
            }
            _quizStateManager.AdvanceToNextQuestion();  // Move to next question regardless of correctness
            return isCorrect;
        }
        return false;
    }

    // Check if the quiz is complete
    public bool IsQuizComplete(Quiz quiz)
    {
        return _quizStateManager.GetCurrentQuestionIndex() >= quiz.Questions.Count;
    }

    // Reset current quiz data
    public void ResetCurrentQuiz()
    {
        _quizStateManager.ClearQuizData();
    }

    // Optionally, if you need to list categories
    public List<string> GetCategories()
    {
        return _quizService.GetQuizCategories().ToList();  // Assuming QuizService can list all categories
    }
}

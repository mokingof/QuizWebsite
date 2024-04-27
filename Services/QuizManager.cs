using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var quiz = _quizService.GetQuizByCategory(category);      
            return quiz;
        }
        return _quizStateManager.GetCurrentQuiz();
    }

    // Evaluate an answer and return true if correct, false otherwise
    public bool SubmitAnswer(Quiz quiz, int answerId)
    {
        if (quiz == null) throw new InvalidOperationException("Quiz is not initialized in QuizManager.");

        var currentQuestion = quiz.Questions.ElementAtOrDefault(_quizStateManager.GetCurrentQuestionIndex());
        if (currentQuestion != null)
        {
            var selectedAnswer = currentQuestion.Answer.FirstOrDefault(a => a.Id == answerId);
            if (selectedAnswer != null)
            {
                if (selectedAnswer.IsCorrect)
                {       
                    return true;
                }
            }
        }
        return false;
    }

    public void AdvanceToNextQuestion()
    {
        _quizStateManager.AdvanceToNextQuestion();
      
    }

    public void GiveUserPoint()
    {
        _quizStateManager.updateUserScore(1);

    }
   
    public bool IsQuizComplete()
    {
        var currentQuiz =_quizStateManager.GetCurrentQuiz();
        if (currentQuiz == null) return false;

        int currentQuestionIndex = _quizStateManager.GetCurrentQuestionIndex();
        return currentQuestionIndex >= currentQuiz.Questions.Count;
    }
    //list categories
    public List<string> GetCategories()
    {
        if (_quizService == null) throw new InvalidOperationException("QuizService is not initialized in QuizManager.");
        return _quizService.GetQuizCategories().ToList();
    }
}

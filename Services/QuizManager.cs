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
    public bool SubmitAnswer(Quiz quiz, int answerIndex)
    {
        if (quiz == null) throw new InvalidOperationException("Quiz is not initialized in QuizManager.");

        var currentQuestion = quiz.Questions.ElementAtOrDefault(_quizStateManager.GetCurrentQuestionIndex());
        if (currentQuestion != null && answerIndex >= 0 && answerIndex < currentQuestion.Answers.Count)
        {
            var isCorrect = currentQuestion.Answers[answerIndex].IsCorrect;
            if (isCorrect)
            { 
                return isCorrect;
            } 
            else
            {
                _quizStateManager.AdvanceToNextQuestion();
                return false;
            }      
        }
        return false;
    }
    
    public void AdvanceToNextQuestion()
    {
        _quizStateManager.AdvanceToNextQuestion();
        _quizStateManager.updateUserScore(1);

    }


    //list categories
    public List<string> GetCategories()
    {
        if (_quizService == null) throw new InvalidOperationException("QuizService is not initialized in QuizManager.");
        return _quizService.GetQuizCategories().ToList();
    }
}

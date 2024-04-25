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
           // _quizStateManager.SetQuizCategory(category);
            var quiz = _quizService.GetQuizByCategory(category);
           // _quizStateManager.SaveCurrentQuiz(quiz);
           // _quizStateManager.ResetQuiz(); // Resets state relevant to the quiz, such as current question index and score
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
               
                return isCorrect;
            }

        
        }
        return false;
    }

    // return current Question
    //public Question CurrentQuestion(Quiz quiz)
    //{      
    //    return quiz.Questions.ElementAtOrDefault(_quizStateManager.GetCurrentQuestionIndex()) ?? 
    //        throw new ArgumentNullException(nameof(quiz), "Data cannot be null.");
    //}

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

    public Quiz GetcurrentQuiz()
    {
        return _quizStateManager.GetCurrentQuiz();
    }

    public void AdvanceToNextQuestion()
    {
        _quizStateManager.AdvanceToNextQuestion();
    }

    public int GetCurrentQuestionIndex()
    {
        return _quizStateManager.GetCurrentQuestionIndex();
    }

    public void UpdateScore(int score)
    {
        _quizStateManager.UpdateScore(score);
    }

    public string GetQuizCategory()
    {
       return _quizStateManager.GetQuizCategory();
    }

    public int GetScore()
    {
        return _quizStateManager.GetScore();
    }


    //list categories
    public List<string> GetCategories()
    {
        if (_quizService == null)
        {
            throw new InvalidOperationException("QuizService is not initialized.");
        }
        return _quizService.GetQuizCategories().ToList();
    }
}

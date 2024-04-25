using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using EducationalQuizApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Sockets;
// Ensure you have the appropriate using directives for your services and models

public class QuizModel : PageModel
{
    private readonly QuizManager _quizManager;
    private readonly QuizStateManager _quizStateManager;

    [BindProperty]
    public int SelectedAnswer { get; set; }

    public Quiz Quiz { get; set; }
    public Question CurrentQuestion => Quiz.Questions.ElementAtOrDefault(_quizManager.GetCurrentQuestionIndex());
    public string FeedbackMessage { get; set; }
    public bool? IsCorrectAnswer { get; set; } = null;
    public bool IsQuizComplete => _quizStateManager.GetCurrentQuestionIndex() >= Quiz.Questions.Count;


    public QuizModel(QuizManager quizManager, QuizStateManager quizStateManager)
    {
        _quizManager = quizManager;
        _quizStateManager = quizStateManager;
    }

    public void OnGet(string category)
    {
        if (string.IsNullOrEmpty(category))
        {
            category = _quizStateManager.GetQuizCategory();
        }
        else
        {
            _quizStateManager.SetQuizCategory(category);
        }
        Quiz = _quizManager.GetQuizForCategory(category);
        _quizStateManager.SaveCurrentQuiz(Quiz);
    }

    public IActionResult OnPost()
    {

        Quiz = _quizStateManager.GetCurrentQuiz();
        IsCorrectAnswer = _quizManager.SubmitAnswer(Quiz, SelectedAnswer);

        if (IsCorrectAnswer == true)
        {
            _quizStateManager.UpdateScore(1);
            _quizStateManager.AdvanceToNextQuestion();

            if (IsQuizComplete)
            {
                FeedbackMessage = $"Quiz Finished, you got {_quizStateManager.GetScore()} out of {Quiz.Questions.Count}!";
                return Page();
            }
            
            FeedbackMessage = "Correct!";
        }
        else
        {
            FeedbackMessage = $"Incorrect. The correct answer was \"{CurrentQuestion.Answers.FirstOrDefault(a => a.IsCorrect)?.AnswerText}\".";

        }
        return Page();

    }

    // Resets Quiz
    public IActionResult OnPostReset()
    {
        _quizStateManager.ResetQuiz();
        return RedirectToPage("/QuizCategory");

    }

    public IActionResult OnPostNextQuestion()
    {
       



        return RedirectToPage();              
    }

}

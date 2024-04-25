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

    [BindProperty]
    public int SelectedAnswer { get; set; }

    [BindProperty]
    public string Category { get; set; }

    public Quiz Quiz { get; set; }
    public Question CurrentQuestion => Quiz.Questions.ElementAtOrDefault(_quizManager.GetCurrentQuestionIndex());
    public string FeedbackMessage { get; set; }
    public bool? IsCorrectAnswer { get; set; } = null;
    public bool IsQuizComplete => _quizManager.GetCurrentQuestionIndex() >= Quiz.Questions.Count;
   

    public QuizModel(QuizManager quizManager)
    {
        _quizManager = quizManager;
    }

    public void OnGet(string category)
    {
        Quiz = _quizManager.GetQuizForCategory(category);
     
    }

    public IActionResult OnPost()
    {
        Quiz = _quizManager.GetQuizForCategory(Category);
      //  Quiz = _quizManager.GetcurrentQuiz();
        IsCorrectAnswer = _quizManager.SubmitAnswer(Quiz, SelectedAnswer);

        if (IsCorrectAnswer == true)
        {
         
            if (_quizManager.IsQuizComplete(Quiz))
            {
                FeedbackMessage = $"Quiz Finished, you got {_quizManager.GetScore()} out of {Quiz.Questions.Count}!";
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
        _quizManager.ResetCurrentQuiz();
        return RedirectToPage("/QuizCategory");

    }
  
    public IActionResult OnPostNextQuestion()
    {
        _quizManager.AdvanceToNextQuestion();  // Move to the next question    
        return RedirectToPage();                // Redirect to refresh the page state
    }

}

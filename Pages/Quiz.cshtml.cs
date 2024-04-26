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
    public Question CurrentQuestion => Quiz.Questions.ElementAtOrDefault(_quizStateManager.GetCurrentQuestionIndex());
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
        if (Quiz == null) throw new InvalidOperationException("Quiz is not initialized in QuizModel.");
        
        IsCorrectAnswer = _quizManager.SubmitAnswer(Quiz, SelectedAnswer);
        _quizStateManager.SaveAnswer(_quizStateManager.GetCurrentQuestionIndex(), SelectedAnswer);

        if (IsCorrectAnswer == true)
        {
            FeedbackMessage = $"Correct!";
            _quizManager.GiveUserPoint();

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
        
        return RedirectToPage("/QuizCategory");

    }
public IActionResult OnPostNextQuestion()
{
    _quizManager.AdvanceToNextQuestion();
    if (_quizManager.IsQuizComplete())
    {
           // _quizStateManager.ResetQuiz();
            return RedirectToPage("/QuizResults");
    }
    return RedirectToPage();
}


}

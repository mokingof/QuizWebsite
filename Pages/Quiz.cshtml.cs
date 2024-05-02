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
    public int? SelectedAnswer { get; set; }
    [BindProperty]
    public int? QuestionId { get; set; }

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

        if (!SelectedAnswer.HasValue)
        {
            FeedbackMessage = "please select an answer before submitting.";
            return Page();
        } else
        {

        }

        IsCorrectAnswer = _quizManager.SubmitAnswer(Quiz, SelectedAnswer.Value);
        _quizStateManager.SaveAnswer(QuestionId.Value, SelectedAnswer.Value);
      
        if (IsCorrectAnswer == true)
        {
            FeedbackMessage = "Correct!";
        }
        else
        {
            // Find the correct answer's text to display in feedback
            var correctAnswerText = CurrentQuestion.Answer.FirstOrDefault(a => a.IsCorrect)?.AnswerText;
            FeedbackMessage = $"Incorrect. The correct answer was \"{correctAnswerText}\".";
        }
          ViewData["OnSubmitAttribute"] = FeedbackMessage != null ? "onsubmit=\"disableRadioButtons()\"" : "";
        return Page();
    }
    public IActionResult OnPostNextQuestion()
    {
        _quizManager.AdvanceToNextQuestion();
        if (_quizManager.IsQuizComplete())
        {

            return RedirectToPage("/QuizResults");
        }
        return RedirectToPage();
    }



}
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
    private readonly QuizService _quizService;
    private readonly QuizStateManager _stateManager;
    private readonly QuizEvaluator _evaluator;
    private readonly ILogger<QuizModel> _logger;

    [BindProperty]
    public int SelectedAnswer { get; set; }

    public Quiz Quiz
    {
        get => _stateManager.GetCurrentQuiz();
        set => _stateManager.SaveCurrentQuiz(value);
    }

    public Question CurrentQuestion => Quiz?.Questions.ElementAtOrDefault(_stateManager.GetCurrentQuestionIndex());
    public bool? IsCorrectAnswer { get; set; } = null;
    public string FeedbackMessage { get; set; }
    public int Score { get; set; }

    public string QuizCategory
    {
        get => _stateManager.GetQuizCategory();
        set
        {
            if (value != _stateManager.GetQuizCategory())
            {
                _stateManager.SetQuizCategory(value);
                Quiz = _quizService.GetQuizByCategory(value);  // Fetch new quiz based on changed category
            }
        }
    }
    public bool IsQuizComplete => _stateManager.GetCurrentQuestionIndex() >= Quiz?.Questions.Count;

    public QuizModel(QuizService quizService, QuizStateManager stateManager, QuizEvaluator evaluator, ILogger<QuizModel> logger)
    {
        _quizService = quizService;
        _stateManager = stateManager;
        _evaluator = evaluator;
        _logger = logger;
    }

    public void OnGet(string category)
    {
        if (!string.IsNullOrEmpty(category) && category != QuizCategory)
        {
            QuizCategory = category; // This sets and fetches the quiz based on the new category
        }
    }

    public IActionResult OnPost()
    {
        if (Quiz == null)
        {
            _logger.LogError("Quiz data is missing on POST request.");
            return RedirectToPage("/Error");
        }

        var currentQuestionIndex = _stateManager.GetCurrentQuestionIndex();
        var currentQuestion = Quiz.Questions[currentQuestionIndex];

        //Evaluate the answer
        IsCorrectAnswer = _evaluator.IsAnswerCorrect(currentQuestion, SelectedAnswer);

        if (IsCorrectAnswer == true)
        {
            _stateManager.AdvanceToNextQuestion();
            _stateManager.UpdateScore(1);
            if (_stateManager.GetCurrentQuestionIndex() >= Quiz.Questions.Count)
            {
                Score = _stateManager.GetCurrentScore();
                FeedbackMessage = $"Quiz finished, you got {Score} out of {Quiz.Questions.Count} questions right.";
                return Page();
            }
        }
        else
        {   
            FeedbackMessage = $"Incorrect. The correct answer was \"{currentQuestion.Answers.FirstOrDefault(a => a.IsCorrect)?.AnswerText}\".";
        }

        return Page();
    }

    // Resets Quiz
    public IActionResult OnPostReset()
    {
        _stateManager.ClearQuizData();
        return RedirectToPage("/QuizCategory");
       
    }

    public IActionResult OnPostNextQuestion()
    {
        _logger.LogInformation("Executing OnPostNextQuestion");
        SelectedAnswer = -1;
        _stateManager.AdvanceToNextQuestion();  // Move to the next question
        return RedirectToPage();                // Redirect to refresh the page state
    }

}

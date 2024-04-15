using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using EducationalQuizApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// Ensure you have the appropriate using directives for your services and models

public class QuizModel : PageModel
{
    private readonly QuizService _quizService;
    private readonly QuizStateManager _stateManager;
    private readonly QuizEvaluator _evaluator;

    // BindProperty attribute is used for properties that should receive their value from the request
    [BindProperty]
    public int SelectedAnswer { get; set; }

    // Properties to hold quiz data and state
    public Quiz Quiz { get; set; }
    public Question CurrentQuestion => Quiz.Questions.ElementAtOrDefault(_stateManager.GetCurrentQuestionIndex());
    public bool? IsCorrectAnswer { get; set; } = null;
    public string FeedbackMessage { get; set; }
    public bool IsQuizComplete => _stateManager.GetCurrentQuestionIndex() >= Quiz.Questions.Count;

    // Constructor with dependency injection
    public QuizModel(QuizService quizService, QuizStateManager stateManager, QuizEvaluator evaluator)
    {
        _quizService = quizService;
        _stateManager = stateManager;
        _evaluator = evaluator;
    }

    public void OnGet()
    {
        // Initialize or load the Quiz object
        Quiz = _quizService.GetSampleQuiz();

        // Check if we need to reset the quiz (e.g., starting over or first access)
        if (IsQuizComplete || Quiz == null)
        {
            _stateManager.ResetQuiz();
        }
    }

    public IActionResult OnPost()
    {
        // Reload the quiz - consider optimizing this in a real application
        Quiz = _quizService.GetSampleQuiz();

        var currentQuestionIndex = _stateManager.GetCurrentQuestionIndex();
        var currentQuestion = Quiz.Questions[currentQuestionIndex];

        // Evaluate the answer
        IsCorrectAnswer = _evaluator.IsAnswerCorrect(currentQuestion, SelectedAnswer);

        if (IsCorrectAnswer == true)
        {    
            
            _stateManager.AdvanceToNextQuestion();
            // Prepare for the next question or end of quiz
            if (_stateManager.GetCurrentQuestionIndex() < Quiz.Questions.Count)
            {
                // Redirect to refresh the page state for the next question
                return RedirectToPage();
            }
            else
            {
                // Quiz completion logic
                //_stateManager.ResetQuiz(); // Optional: Reset for a new round
                FeedbackMessage = "Congratulations, you've completed the quiz!";
                Console.Write("FeedbackMessage"); 
                return Page();
            }
        }
        else
        {
            // Incorrect answer, generate feedback
            FeedbackMessage = $"Incorrect. The correct answer was \"{currentQuestion.Answers.FirstOrDefault(a => a.IsCorrect)?.AnswerText}\".";
            return Page(); // Stay on the current question
        }
    }

    public IActionResult OnPostReset()
    {
        _stateManager.ResetQuiz();
        return RedirectToPage();
    }
}

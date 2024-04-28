using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EducationalQuizApp.Pages
{
    public class QuizReviewModel : PageModel
    {
        private readonly QuizStateManager _quizStateManager;

        public Quiz Quiz {  get; private set; }
        public string QuizName { get; private set; }
        public Dictionary<int,int> UserAnswers { get; private set; }
     
        public QuizReviewModel(QuizStateManager quizStateManager)
        {
            _quizStateManager = quizStateManager;
        }
        public void OnGet()
        {
            QuizName = _quizStateManager.GetQuizCategory();
            Quiz = _quizStateManager.GetCurrentQuiz();
            UserAnswers = _quizStateManager.GetUserAnswers();

        }

        public IActionResult OnPostReset()
        {
            _quizStateManager.ResetQuiz();
            return RedirectToPage("/QuizCategory");

        }
    }
}

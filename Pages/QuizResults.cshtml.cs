using EducationalQuizApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EducationalQuizApp.Pages
{
    public class QuizResultsModel : PageModel
    {
        private readonly QuizStateManager _quizStateManager;


        public int FinalScore { get; private set; }
        public string QuizName { get; private set; }

        public QuizResultsModel(QuizStateManager quizStateManager)
        {
            _quizStateManager = quizStateManager;
        }


        public void OnGet()
        {
            FinalScore = _quizStateManager.GetUserScore();
            QuizName = _quizStateManager.GetQuizCategory();
            _quizStateManager.ResetQuiz();

        }

        public IActionResult OnPostReset()
        {

            return RedirectToPage("/QuizCategory");

        }
    }
}

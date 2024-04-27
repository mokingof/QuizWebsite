using EducationalQuizApp.Model;
using EducationalQuizApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EducationalQuizApp.Pages
{
    // The purpose of this class is to display the users results for a specific quiz.
    public class QuizResultsModel : PageModel
    {
        private readonly QuizStateManager _quizStateManager;


        public int FinalScore { get; private set; }
        public string QuizName { get; private set; }
        public Quiz QuizSize { get; private set; }



        public QuizResultsModel(QuizStateManager quizStateManager)
        {
            _quizStateManager = quizStateManager;
        }

        
        public void OnGet()
        {
            //Get user final score
            FinalScore = _quizStateManager.GetUserScore();
            //Get quiz size in order to see how many question they got right
            QuizSize = _quizStateManager.GetCurrentQuiz();
            //To display quiz name
            QuizName = _quizStateManager.GetQuizCategory();
                     
        }

        public IActionResult OnPostShowQuiz()
        {
            return RedirectToPage("/QuizReview");
        }


        public IActionResult OnPostReset()
        {
            _quizStateManager.ResetQuiz();
            return RedirectToPage("/QuizCategory");

        }
    }
}

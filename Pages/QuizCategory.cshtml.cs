using EducationalQuizApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EducationalQuizApp.Pages
{
    public class QuizCategoryModel : PageModel
    {
        private readonly QuizService _quizService;
        private readonly QuizManager _quizManager;
   

        public QuizCategoryModel(QuizService quizService, QuizManager _quizManager)
        {
            _quizService = quizService;
            _quizManager = _quizManager;
        }

        public List<string> QuizCategories { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; } // to store the selected category

        public void OnGet()
        {
            QuizCategories = _quizManager.GetCategories();
        }

        public IActionResult OnPost()
        {
            // Redirect to the Quiz page with the selected category
            return RedirectToPage("/Quiz", new { category = SelectedCategory });

        }
    }
}

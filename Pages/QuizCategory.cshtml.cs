using EducationalQuizApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EducationalQuizApp.Pages
{
    public class QuizCategoryModel : PageModel
    {
        private readonly QuizService _quizService;
        private readonly ILogger<QuizModel> _logger;

        public QuizCategoryModel(QuizService quizService)
        {
            _quizService = quizService;
          
        }

        public List<string> Categories { get; set; }

        [BindProperty]
        public string SelectedCategory { get; set; } // to store the selected category

        public void OnGet()
        {
           // _logger.LogInformation($"The OnGet method, Retrieving Selected category:{SelectedCategory}");
            Categories = _quizService.GetQuizCategories().ToList();
        }

        public IActionResult OnPost()
        {
            // Check if the selected category is valid
            if (!string.IsNullOrEmpty(SelectedCategory) && _quizService.GetQuizByCategory(SelectedCategory) != null)
            {
                // Redirect to the Quiz page with the selected category
                return RedirectToPage("/Quiz", new { category = SelectedCategory });
            }
            // Handle the case where no valid category was selected
            ModelState.AddModelError("", "Please select a valid category.");
            Categories = _quizService.GetQuizCategories().ToList(); // Reload categories for the page
            return Page();
        }
    }
}

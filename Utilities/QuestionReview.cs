using EducationalQuizApp.Model;

namespace EducationalQuizApp.Utilities
{
    public class QuestionReview
    {
        public Question Question { get; set; }
        public int? SelectedAnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }
}

namespace EducationalQuizApp.Model
{
    public class Question
    {
        public string QuestionText { get; set; }
        public List<Answers> Answer { get; set; }

        public int Id { get; set; }

    }
}

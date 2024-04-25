using EducationalQuizApp.Model;

namespace EducationalQuizApp.Services
{
    public class QuizService
    {
        private Dictionary<string, Quiz> _quizzes = new Dictionary<string, Quiz>();

        private readonly ILogger<QuizService> _logger;
        public QuizService(ILogger<QuizService> logger)
        {

            InitialiseQuizzes();
            _logger = logger;
        }

        public Quiz GetQuizByCategory(string category)
        {
         

            return _quizzes.GetValueOrDefault(category);
          
        }


        public IEnumerable<string> GetQuizCategories()
        {
            return _quizzes.Keys;
        }

        private void InitialiseQuizzes()
        {

            _quizzes = new Dictionary<string, Quiz>
            {
                ["Maths"] = new Quiz
                {
                    Title = "Basic Maths Quiz",
                    Category = "Maths",
                    Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is 5 + 3?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "8", IsCorrect = true },
                            new Answers { AnswerText = "7", IsCorrect = false },
                            new Answers { AnswerText = "9", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is 10 - 6?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "5", IsCorrect = false },
                            new Answers { AnswerText = "4", IsCorrect = true },
                            new Answers { AnswerText = "3", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is 7 x 6?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "42", IsCorrect = true },
                            new Answers { AnswerText = "48", IsCorrect = false },
                            new Answers { AnswerText = "36", IsCorrect = false }
                        }
                    }
                }
                },
                ["Science"] = new Quiz
                {
                    Title = "Basic Science Quiz",
                    Category = "Science",
                    Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What planet is known as the Red Planet?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "Jupitor", IsCorrect = true },
                            new Answers { AnswerText = "Mars", IsCorrect = true },
                            new Answers { AnswerText = "Saturn", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What gas do plants absorb from the atmosphere?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "Carbon Dioxide", IsCorrect = true },
                            new Answers { AnswerText = "Oxygen", IsCorrect = false },
                            new Answers { AnswerText = "Nitrogen", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is the boiling point of water?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "100 degrees Celsius", IsCorrect = true },
                            new Answers { AnswerText = "90 degrees Celsius", IsCorrect = false },
                            new Answers { AnswerText = "110 degrees Celsius", IsCorrect = false }
                        }
                    }
                }
                },
                ["Health"] = new Quiz
                {
                    Title = "Basic Health Quiz",
                    Category = "Health",
                    Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "Which nutrient plays an essential role in muscle-building?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "Proteins", IsCorrect = true },
                            new Answers { AnswerText = "Carbohydrates", IsCorrect = false },
                            new Answers { AnswerText = "Fats", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is the normal human body temperature in degrees Celsius?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "40", IsCorrect = true },
                            new Answers { AnswerText = "35", IsCorrect = false },
                            new Answers { AnswerText = "37", IsCorrect = true }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which vitamin is most commonly associated with citrus fruits?",
                        Answers = new List<Answers>
                        {
                            new Answers { AnswerText = "Vitamin D", IsCorrect = false },
                            new Answers { AnswerText = "Vitamin C", IsCorrect = true },
                            new Answers { AnswerText = "Vitamin B12", IsCorrect = false }
                        }
                    }
                }
                }
            };

        }

    }
}

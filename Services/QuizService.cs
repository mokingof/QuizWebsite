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
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "8", IsCorrect = true, Id = 1},
                            new Answers { AnswerText = "7", IsCorrect = false, Id = 2},
                            new Answers { AnswerText = "9", IsCorrect = false, Id = 3 }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is 10 - 6?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "5", IsCorrect = false, Id = 4 },
                            new Answers { AnswerText = "4", IsCorrect = true, Id = 5 },
                            new Answers { AnswerText = "3", IsCorrect = false, Id = 6 }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is 7 x 6?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "42", IsCorrect = true, Id = 7 },
                            new Answers { AnswerText = "48", IsCorrect = false, Id = 8 },
                            new Answers { AnswerText = "36", IsCorrect = false , Id = 9}
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
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "Jupitor", IsCorrect = true, Id = 10 },
                            new Answers { AnswerText = "Mars", IsCorrect = true , Id = 11},
                            new Answers { AnswerText = "Saturn", IsCorrect = false , Id = 12}
                        }
                    },
                    new Question
                    {
                        QuestionText = "What gas do plants absorb from the atmosphere?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "Carbon Dioxide", IsCorrect = true, Id = 13 },
                            new Answers { AnswerText = "Oxygen", IsCorrect = false, Id = 14 },
                            new Answers { AnswerText = "Nitrogen", IsCorrect = false, Id = 15 }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is the boiling point of water?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "100 degrees Celsius", IsCorrect = true, Id = 16 },
                            new Answers { AnswerText = "90 degrees Celsius", IsCorrect = false, Id = 17 },
                            new Answers { AnswerText = "110 degrees Celsius", IsCorrect = false, Id = 18 }
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
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "Proteins", IsCorrect = true, Id = 19 },
                            new Answers { AnswerText = "Carbohydrates", IsCorrect = false, Id = 20 },
                            new Answers { AnswerText = "Fats", IsCorrect = false, Id = 21 }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is the normal human body temperature in degrees Celsius?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "40C", IsCorrect = true, Id = 22 },
                            new Answers { AnswerText = "35C", IsCorrect = false, Id = 23 },
                            new Answers { AnswerText = "37C", IsCorrect = true, Id = 24 }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which vitamin is most commonly associated with citrus fruits?",
                        Answer = new List<Answers>
                        {
                            new Answers { AnswerText = "Vitamin D", IsCorrect = false, Id = 25},
                            new Answers { AnswerText = "Vitamin C", IsCorrect = true, Id = 26 },
                            new Answers { AnswerText = "Vitamin B12", IsCorrect = false, Id = 27 }
                        }
                    }
                }
                }
            };

        }

    }
}

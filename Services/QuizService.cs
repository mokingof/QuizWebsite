using EducationalQuizApp.Model;

namespace EducationalQuizApp.Services
{
    public class QuizService
    {

        // A method to retrieve a quiz. It returns a pre-defined quiz object.
        public Quiz GetSampleQuiz()
        {
            // Create a new Quiz object and populate it with questions and answers
            return new Quiz
            {
                Title = "Question 1",
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is 2 + 2 ?",
                        Answers = new List<Answers>
                        {
                            new Answers {AnswerText = "3",IsCorrect = false},
                            new Answers {AnswerText = "4",IsCorrect = true},
                            new Answers {AnswerText= "5",IsCorrect = false},
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is 3 / 3 ?",
                        Answers = new List<Answers> 
                        {
                            new Answers {AnswerText = "0",IsCorrect = true},
                            new Answers {AnswerText = "1",IsCorrect = false},
                            new Answers {AnswerText= "0.5",IsCorrect = false},

                        }
                    },

                    new Question
                    {
                        QuestionText = "Who is the Best home D",
                        Answers = new List<Answers>
                        {
                            new Answers {AnswerText = "Implosions",IsCorrect = true},
                            new Answers {AnswerText = "Name",IsCorrect = false},
                            new Answers {AnswerText= "Ghiom",IsCorrect = false},
                        }
                    },
                    new Question
                    {
                        QuestionText = "Ok, now who actually plays the home D role",
                        Answers = new List<Answers>
                        {
                            new Answers {AnswerText = "Implosions",IsCorrect = false},
                            new Answers {AnswerText = "Name",IsCorrect = true},
                            new Answers {AnswerText= "Alamar",IsCorrect = false},
                        }
                    }
                }
            };

        }
    }
}

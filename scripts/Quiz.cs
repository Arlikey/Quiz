using Quiz.gameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.scripts
{
    internal class Quiz
    {
        public Quiz(Dictionary<string, List<Question>> quizCategories)
        {
            QuizCategories = quizCategories;
            players = new List<Player>();
        }
        private List<Player> players;
        public Dictionary<string, List<Question>> QuizCategories { get; set; }

        public void StartQuiz(string selectedQuizCategory)
        {
            Player player = GetOrCreatePlayer(UserAuthentication.CurrentUser);
            int correctAnswersCount = 0;

            List<Question> questions = QuizCategories[selectedQuizCategory];

            int questionIndex = 1;

            foreach (var question in questions)
            {
                Console.Clear();
                Console.WriteLine($"\tКатегория: {selectedQuizCategory}\n");
                Console.WriteLine($"Вопрос #{questionIndex++}: { question.QuestionText}\n");

                int answerIndex = 1;

                foreach (var answer in question.Answers)
                {
                    Console.WriteLine($"{answerIndex++}. {answer}");
                }

                if(question.CorrectAnswers.Count == 1)
                {
                    Console.Write("\nВведите номер правильного ответа: ");
                }
                else
                {
                    Console.Write("\nВведите номера правильных ответов через пробел: ");
                }
                string[] userAnswers = Console.ReadLine().Split(' ');

                bool allCorrect = true;
                foreach (var answer in question.CorrectAnswers)
                {
                    if (!userAnswers.Contains(answer))
                    {
                        allCorrect = false;
                        break;
                    }
                }

                if (allCorrect && userAnswers.Length == question.CorrectAnswers.Count)
                {
                    correctAnswersCount++;
                }
            }

            player.Score = correctAnswersCount;

            Console.Clear();
            Console.WriteLine($"Вы ответили правильно на {correctAnswersCount} вопросов из {questions.Count}");
            player.AddQuizResult(selectedQuizCategory, player.Score);
            SavePlayerData(player);
            Console.WriteLine("\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey();
        }

        public Player GetOrCreatePlayer(UserLoginData userData)
        {
            Player player = LoadPlayerData(UserAuthentication.CurrentUser!);
            if (player == null)
            {
                player = new Player(userData);
                players.Add(player);
            }
            return player;
        }

        public void SavePlayerData(Player player)
        {
            string json = JsonSerializer.Serialize(player);
            string fileName = $"{player.UserData.Login}.json";
            File.WriteAllText(fileName, json);
        }
        public Player LoadPlayerData(UserLoginData userData)
        {
            string fileName = $"{userData.Login}.json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<Player>(json);
            }
            return null;
        }
    }
}

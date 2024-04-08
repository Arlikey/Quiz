using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.gameObjects
{
    internal class Player
    {
        public UserLoginData UserData { get; set; }
        public int Score { get; set; }
        public List<QuizResult> PreviousQuizResults { get; set; }

        public Player(UserLoginData userData)
        {
            UserData = userData;
            Score = 0;
            PreviousQuizResults = new List<QuizResult>();
        }

        public void AddQuizResult(string category, int score)
        {
            PreviousQuizResults.Add(new QuizResult(category, score));
        }

        public void ShowPreviousQuizResults()
        {
            Console.Clear();
            Console.WriteLine($"Прошлые викторины для пользователя {UserData.Login}:\n");
            foreach (var result in PreviousQuizResults)
            {
                Console.WriteLine($"\tКатегория: {result.Category} | Счет: {result.Score}");
            }
            Console.WriteLine("\nДля продолжения нажмите на любую клавишу...");
            Console.ReadKey();
        }

    }
}

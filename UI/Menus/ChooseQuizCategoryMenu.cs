using Quiz.gameObjects;
using Quiz.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Quiz.UI.Menus
{
    internal class ChooseQuizCategoryMenu : Menu
    {
        protected Quiz.scripts.Quiz quiz;
        public ChooseQuizCategoryMenu(ConsoleManager consoleManager, UserAuthentication userAuthentication) : base(consoleManager, userAuthentication)
        {
            string jsonString = File.ReadAllText("Quiz.json");
            quiz = new Quiz.scripts.Quiz(JsonSerializer.Deserialize<Dictionary<string, List<Question>>>(jsonString));
        }

        private bool isBack = false;
        string menuText = $"""
                Выбор новой викторины:

            Выберите категорию:

            """;
        string[] menuItems;

        public void StartMenu()
        {
            menuItems = quiz.QuizCategories.Keys.ToArray();
            Array.Resize(ref menuItems, menuItems.Length+1);
            menuItems[menuItems.Length - 1] = "Назад";
            
            DisplayMenu(menuText, menuItems);

            while (!isBack)
            {
                DisplayMenu(menuText, menuItems);

                MenuManagement(menuItems);

            }
            isBack = false;
            selectedItemIndex = 0;
        }
        public override void HandleMenuItemSelection(int selectedItemIndex)
        {
            switch (selectedItemIndex)
            {
                case 0:
                    quiz.StartQuiz(quiz.QuizCategories.ElementAt(selectedItemIndex).Key);
                    break;
                case 1:
                    quiz.StartQuiz(quiz.QuizCategories.ElementAt(selectedItemIndex).Key);
                    break;
                case 2:
                    isBack = true;
                    break;
            }
        }
    }
}

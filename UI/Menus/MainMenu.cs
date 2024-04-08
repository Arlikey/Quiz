using Quiz.gameObjects;
using Quiz.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.UI.Menus
{
    internal class MainMenu : Menu
    {
        private Player currentPlayer;
        protected Quiz.scripts.Quiz quiz;
        string menuText;
        string[] menuItems = { "Начать новую викторину", "Посмотреть результаты прошлых викторин", "Посмотреть ТОП-20 по викторине", "Изменить настройки", "Выход" };

        public MainMenu(ConsoleManager consoleManager, UserAuthentication userAuthentication) : base(consoleManager, userAuthentication)
        {
            string jsonString = File.ReadAllText("Quiz.json");
            quiz = new Quiz.scripts.Quiz(JsonSerializer.Deserialize<Dictionary<string, List<Question>>>(jsonString));
        }
        public void StartMenu()
        {
            DisplayMenu(menuText, menuItems);

            while (true)
            {
                
                menuText = $"""
                    Главное меню:

                {UserAuthentication.CurrentUser}

                Выберите действие:

                """;
                DisplayMenu(menuText, menuItems);

                MenuManagement(menuItems);

            }
        }
        public override void HandleMenuItemSelection(int selectedItemIndex)
        {
            switch (selectedItemIndex)
            {
                case 0:
                    new ChooseQuizCategoryMenu(consoleManager, userAuthentication).StartMenu();
                    break;
                case 1:
                    currentPlayer = quiz.LoadPlayerData(UserAuthentication.CurrentUser!);
                    if(currentPlayer == null)
                    {
                        Console.Clear();
                        Console.WriteLine("Игрок ещё не проходил викторин!\nДля продолжения нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                    }
                    currentPlayer.ShowPreviousQuizResults();
                    break;
                case 2:
                    Console.WriteLine("К сожалению данная функция не была внедрена в продукт :(\nДля продолжения нажмите на любую клавишу...");
                    Console.ReadKey();
                    break;
                case 3:
                    new Settings(consoleManager, userAuthentication).StartMenu();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

using Quiz.gameObjects;
using Quiz.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.UI.Menus
{
    internal class MainMenu : Menu
    {
        string menuText;
        string[] menuItems = { "Начать новую викторину", "Посмотреть результаты прошлых викторин", "Посмотреть ТОП-20 по викторине", "Изменить настройки", "Выход" };
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
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    new Settings().StartMenu();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

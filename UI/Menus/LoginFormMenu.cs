using Quiz.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.UI.Menus
{
    internal class LoginFormMenu : Menu
    {
        string menuText = """
                Добро пожаловать в "Викторину"

            Выберите действие:

            """;
        string[] menuItems = { "Войти", "Зарегистрироваться", "Выход" };
        public void StartMenu()
        {

            DisplayMenu(menuText, menuItems);

            while (true)
            {
                DisplayMenu(menuText, menuItems);

                MenuManagement(menuItems);

            }
        }
        public override void HandleMenuItemSelection(int selectedItemIndex)
        {
            switch (selectedItemIndex)
            {
                case 0:
                    if (UserAuthentication.Login())
                    {
                        new MainMenu().StartMenu();
                    }
                    break;
                case 1:
                    UserAuthentication.Register();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

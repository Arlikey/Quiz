using Quiz.gameObjects;
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

        public LoginFormMenu(ConsoleManager consoleManager, UserAuthentication userAuthentication) : base(consoleManager, userAuthentication)
        {
        }

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
                    if (userAuthentication.Login(consoleManager.EnterLogin(), consoleManager.EnterPassword()))
                    {
                        new MainMenu(consoleManager, userAuthentication).StartMenu();
                    }
                    else
                    {
                        Console.WriteLine("Данного пользователя не существует. Повторите попытку!\nДля продолжения нажмите любую клавишу...");
                        Console.ReadKey();
                    }
                    break;
                case 1:
                    string login = consoleManager.EnterLogin();
                    string password = consoleManager.EnterPassword();
                    DateOnly birthdate = consoleManager.EnterBirthdate();
                    UserLoginData userLoginData = new UserLoginData() { Login = login, Password = password, Birthdate = birthdate };
                    if (userAuthentication.Register(userLoginData))
                    {
                        Console.WriteLine("Вы успешно зарегистрировались!\nДля продолжения нажмите любую клавишу...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Пользователь с таким логином уже существует. Повторите попытку!\nДля продолжения нажмите любую клавишу...");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}

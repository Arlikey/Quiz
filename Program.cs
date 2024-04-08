using Quiz.gameObjects;
using Quiz.scripts;
using Quiz.UI.Menus;
using System.IO;
using System.Text.Json;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginFormMenu loginFormMenu = new LoginFormMenu(new ConsoleManager(), new UserAuthentication());
            loginFormMenu.StartMenu();
        }
    }
}

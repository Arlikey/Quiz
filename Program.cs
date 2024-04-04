using Quiz.gameObjects;
using Quiz.scripts;
using Quiz.UI.Menus;

namespace Quiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LoginFormMenu loginFormMenu = new LoginFormMenu();
            loginFormMenu.StartMenu();
        }
    }
}

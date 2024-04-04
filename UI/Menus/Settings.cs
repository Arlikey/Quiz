using Quiz.scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.UI.Menus
{
    internal class Settings : Menu
    {
        private bool isBack = false;
        string menuText = $"""
                Настройки:

            Выберите действие:

            """;
        string[] menuItems = { "Изменить пароль", "Изменить дату рождения", "Назад"};
        public void StartMenu()
        {

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
                    UserAuthentication.ChangePassword();
                    break;
                case 1:
                    UserAuthentication.ChangeBirthdate();
                    break;
                case 2:
                    isBack = true;
                    break;
            }
        }
    }
}

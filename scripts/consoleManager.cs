using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.scripts
{
    internal class ConsoleManager
    {
        private string emptyStringMessage = "Вы ввели пустую строку.";

        public string EnterText(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input))
            {
                Console.Write($"{emptyStringMessage} {prompt}");
                input = Console.ReadLine();
            }
            return input;
        }
        public string EnterPassword()
        {
            Console.Clear();
            return EnterText("Введите пароль: ");
        }

        public string EnterLogin()
        {
            Console.Clear();
            return EnterText("Введите логин: ");
        }

        public DateOnly EnterBirthdate()
        {
            Console.Clear();
            DateOnly birthDateDateOnly;
            string prompt = "Введите дату рождения (dd/mm/yyyy): ";
            string birthdate = EnterText(prompt);

            while (!DateOnly.TryParse(birthdate, out birthDateDateOnly))
            {
                Console.Clear();
                Console.Write("Указан неправильный формат даты.\n");
                birthdate = EnterText(prompt);
            }

            return birthDateDateOnly;
        }
    }
}

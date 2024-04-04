using Quiz.gameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.scripts
{
    internal static class UserAuthentication
    {
        public static UserLoginData? CurrentUser { get; set; }

        private static string path = "UserLoginData.json";

        public static bool IsUserExists(string login)
        {
            List<UserLoginData> users = GetUsers();
            return users.Any(user => user.Login == login);
        }

        public static bool RegisterUser(string login, string password, DateOnly birthdate)
        {
            if (IsUserExists(login))
            {
                Console.WriteLine("\nПользователь с таким именем уже существует. Попробуйте ещё раз.");
                return false;
            }

            UserLoginData newUser = new UserLoginData { Login = login, Password = password, Birthdate = birthdate };
            List<UserLoginData> userList = GetUsers();
            userList.Add(newUser);

            string jsonString = JsonSerializer.Serialize(userList);
            File.WriteAllText(path, jsonString);

            Console.WriteLine("\nВы успешно зарегистрировались!");
            return true;
        }
        public static bool AuthenticateUser(string login, string password)
        {
            List<UserLoginData> users = GetUsers();
            UserLoginData user = users.FirstOrDefault(user => user.Login == login && user.Password == password)!;
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }
        private static List<UserLoginData> GetUsers()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<UserLoginData>>(jsonString)!;
            }
            return new List<UserLoginData>();
        }

        public static bool Login()
        {
            Console.Clear();
            Console.WriteLine("Введите логин и пароль пользователя:\n");
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            while (string.IsNullOrEmpty(login))
            {
                Console.Write("Введеная строка не может быть пустой.\nВведите логин: ");
                login = Console.ReadLine();
            }
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            while (string.IsNullOrEmpty(password))
            {
                Console.Write("Введеная строка не может быть пустой.\nВведите пароль: ");
                password = Console.ReadLine();
            }

            if (AuthenticateUser(login, password))
            {
                return true;
            }
            else
            {
                Console.WriteLine("\nНеверное имя пользователя или пароль. Попробуйте ещё раз.");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return false;
            }
        }

        public static void Register()
        {
            Console.Clear();
            Console.WriteLine("Введите данные для нового пользователя:\n");
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            while (string.IsNullOrEmpty(login))
            {
                Console.Write("Введеная строка не может быть пустой.\nВведите логин: ");
                login = Console.ReadLine();
            }
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            while (string.IsNullOrEmpty(password))
            {
                Console.Write("Введеная строка не может быть пустой.\nВведите пароль: ");
                password = Console.ReadLine();
            }
            Console.Write("Укажите вашу дату рождения (пример 30.04.2005): ");
            string birthdate = Console.ReadLine();
            DateOnly birthDateDateOnly;
            while (!DateOnly.TryParse(birthdate, out birthDateDateOnly))
            {
                Console.Write("Указан неправильный формат даты.\nУкажите дату рождения: ");
                birthdate = Console.ReadLine();
            }
            RegisterUser(login, password, birthDateDateOnly);

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        public static void ChangePassword()
        {
            Console.Clear();
            List<UserLoginData> users = GetUsers();
            UserLoginData user = users.FirstOrDefault(u => CurrentUser?.Login == u.Login)!;

            Console.Write("Введите новый пароль: ");
            string password = Console.ReadLine()!;
            while (string.IsNullOrEmpty(password))
            {
                Console.Write("Введеная строка не может быть пустой.\nВведите пароль: ");
                password = Console.ReadLine()!;
            }
            user.Password = password;

            CurrentUser = user;

            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(path, jsonString);

            Console.WriteLine("\nПароль успешно изменен!");
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        public static void ChangeBirthdate()
        {
            Console.Clear();
            List<UserLoginData> users = GetUsers();
            UserLoginData user = users.FirstOrDefault(u => CurrentUser?.Login == u.Login)!;

            Console.Write("Введите новую дату рождения (пример 30.04.2005): ");
            string birthdate = Console.ReadLine();
            DateOnly birthDateDateOnly;
            while (!DateOnly.TryParse(birthdate, out birthDateDateOnly))
            {
                Console.Write("Указан неправильный формат даты.\nУкажите дату рождения: ");
                birthdate = Console.ReadLine();
            }
            user.Birthdate = birthDateDateOnly;

            CurrentUser = user;

            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(path, jsonString);

            Console.WriteLine("\nДата рождения успешно изменена!");
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}

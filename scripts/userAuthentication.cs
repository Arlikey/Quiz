using Quiz.gameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.scripts
{
    internal class UserAuthentication
    {
        public static UserLoginData? CurrentUser { get; set; }

        private static string path = "UserLoginData.json";

        public bool IsUserExists(string login)
        {
            List<UserLoginData> users = GetUsers();
            return users.Any(user => user.Login == login);
        }

        public bool RegisterUser(UserLoginData userLoginData)
        {
            if (IsUserExists(userLoginData.Login))
            {
                return false;
            }

            UserLoginData newUser = new UserLoginData { Login = userLoginData.Login, Password = userLoginData.Password, Birthdate = userLoginData.Birthdate };
            List<UserLoginData> userList = GetUsers();
            userList.Add(newUser);

            string jsonString = JsonSerializer.Serialize(userList);
            File.WriteAllText(path, jsonString);

            return true;
        }
        public bool AuthenticateUser(string login, string password)
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
        private List<UserLoginData> GetUsers()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<UserLoginData>>(jsonString)!;
            }
            return new List<UserLoginData>();
        }

        public bool Login(string login, string password)
        {
            if (AuthenticateUser(login, password))
            {
                return true;
            }
            return false;
        }

        public bool Register(UserLoginData userLoginData)
        {
            if (RegisterUser(userLoginData)) return true;
            return false;
        }
        public void ChangePassword(string password)
        {
            List<UserLoginData> users = GetUsers();
            UserLoginData user = users.FirstOrDefault(u => CurrentUser?.Login == u.Login)!;

            user.Password = password;

            CurrentUser = user;

            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(path, jsonString);
        }
        public void ChangeBirthdate(DateOnly birthdate)
        {
            List<UserLoginData> users = GetUsers();
            UserLoginData user = users.FirstOrDefault(u => CurrentUser?.Login == u.Login)!;

            user.Birthdate = birthdate;

            CurrentUser = user;

            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText(path, jsonString);
        }
    }
}

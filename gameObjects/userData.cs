using Quiz.scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Quiz.gameObjects
{
    internal class UserLoginData
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public DateOnly Birthdate { get; set; }
        public override string ToString()
        {
            return $"Логин - {Login}\nДата рождения - {Birthdate}";
        }
    }
}

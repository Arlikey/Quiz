using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.gameObjects
{
    internal class QuizResult
    {
        public string Category { get; set; }
        public int Score { get; set; }

        public QuizResult(string category, int score)
        {
            Category = category;
            Score = score;
        }
    }
}

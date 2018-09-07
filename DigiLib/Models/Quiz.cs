using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Quiz
    {
        public int ID { get; set; }
        public string Quizs { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Subject
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Video> Videos { get; set; }
        public ICollection<Presentation> Presentations { get; set; }
        public ICollection<Url> Urls { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}

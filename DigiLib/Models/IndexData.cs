using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class IndexData
    {
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Video> Videos { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }
        public IEnumerable<Url> Urls { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Quiz> Quizzes { get; set; }
        public Subject Subject { get; set; }
    }
}

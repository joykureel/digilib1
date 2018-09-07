using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Books { get; set; }
        public string BookLink { get; set; }
        public string BookImage { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}

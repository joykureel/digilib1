using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Presentation
    {
        public int ID { get; set; }
        public string PresentationName { get; set; }

        public string Presentations { get; set; }
        public int SubjectID { get; set; }
        public  Subject Subject { get; set; }
    }
}

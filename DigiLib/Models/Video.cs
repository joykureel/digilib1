using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Video
    {
        public int ID { get; set; }
        public string VideoTitle { get; set; }
        public string VideoDescription { get; set; }
        public string Videos { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}

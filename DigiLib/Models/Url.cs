using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiLib.Models
{
    public class Url
    {
        public int ID { get; set; }
        public string UrlName { get; set; }
        public string UrlDescription { get; set; }
        public string Urls { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }
}

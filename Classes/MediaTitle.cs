using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeApp.Classes
{
    public class MediaTitle
    {
        public string English { get; set; }
        public string Romaji { get; set; }
        public string Native { get; set; }
        public List<string> Synonyms { get; set; }
    }
}

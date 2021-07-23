using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public class Entry
    {
        public EntryStatus Status { get; set; }
        public int Score { get; set; }
        public int Progress { get; set; }
        public int Repeat { get; set; }
        public bool IsPrivate { get; set; }
        public string Notes { get; set; }

        public DateTime Started { get; set; }
        public DateTime Completed { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Media Media;
    }
}

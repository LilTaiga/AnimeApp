using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public class UserList
    {
        public string Name { get; set; }
        public EntryStatus Status { get; set; }
        public bool IsCustomList { get; set; }

        public int Length { get; set; }
        public List<Entry> Entries { get; set; }
    }
}

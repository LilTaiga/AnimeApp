using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    public class UserList
    {
        public string Name { get; set; }
        public EntryStatus Status { get; set; }
        public bool IsCustomList { get; set; }

        [JsonProperty]
        public readonly List<Entry> entries = new List<Entry>();
        public IReadOnlyList<Entry> Entries
        {
            get { return entries.AsReadOnly(); }
        }


        public void AddOrUpdateEntry(Anilist.Result.Entry _entry)
        {
            Entry _oldEntry = entries.Find(n => n.Id == _entry.id);

            if (_oldEntry != null)
                _oldEntry = new Entry(_entry);
            else
                entries.Add(new Entry(_entry));
        }
    }
}

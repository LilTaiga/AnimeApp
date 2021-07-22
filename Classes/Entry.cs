using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AnimeApp.Enums;

namespace AnimeApp.Classes
{
    class Entry
    {
        EntryStatus status;
        int score;
        int progress;
        int repeat;
        bool isPrivate;
        string notes;

        DateTime started;
        DateTime completed;

        DateTime created;
        DateTime updated;

        Media media;
    }
}

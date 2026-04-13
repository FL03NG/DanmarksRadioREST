using System;
using System.Collections.Generic;
using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Repo
{
    public class MusicRepository : IMusicRepository
    {
        private readonly List<MusicRecord> _musicRecords;

        public MusicRepository()
        {
            _musicRecords = new List<MusicRecord>
            {
                new MusicRecord { Id = 1, Title = "Bohemian Rhapsody", Artist = "Queen", Duration = 354, Year = 1975 },
                new MusicRecord { Id = 2, Title = "Imagine", Artist = "John Lennon", Duration = 183, Year = 1971 },
                new MusicRecord { Id = 3, Title = "Stairway to Heaven", Artist = "Led Zeppelin", Duration = 482, Year = 1971 }
            };
        }
        public IEnumerable<MusicRecord> GetAll()
        {
            return _musicRecords;
        }

    }
}

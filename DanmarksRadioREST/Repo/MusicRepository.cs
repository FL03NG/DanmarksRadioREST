using System;
using System.Collections.Generic;
using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Repo
{
    public class MusicRepository : IMusicRepository
    {
        private readonly List<MusicRecord> _musicRecords;
        private int _nextId;

        public MusicRepository()
        {
            _musicRecords = new List<MusicRecord>
            {
                new MusicRecord { Id = 1, Title = "Bohemian Rhapsody", Artist = "Queen", Duration = 354, Year = 1975 },
                new MusicRecord { Id = 2, Title = "Imagine", Artist = "John Lennon", Duration = 183, Year = 1971 },
                new MusicRecord { Id = 3, Title = "Stairway to Heaven", Artist = "Led Zeppelin", Duration = 482, Year = 1971 }
            };
            _nextId = 4;
        }
        public IEnumerable<MusicRecord> GetAll()
        {
            return _musicRecords;
        }
        public MusicRecord? Add(MusicRecord musicRecord)
        {
            if (musicRecord == null)
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(musicRecord.Title))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(musicRecord.Artist))
            {
                return null;
            }
            if (musicRecord.Duration <= 0)
            {
                return null;
            }
            if (musicRecord.Year <= 0)
            {
                return null;
            }
            MusicRecord newRecord = new MusicRecord
            {
                Id = _nextId,
                Title = musicRecord.Title,
                Artist = musicRecord.Artist,
                Duration = musicRecord.Duration,
                Year = musicRecord.Year
            };
            _musicRecords.Add(newRecord);
            _nextId++;
            return newRecord;
        }
    }
}

using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Repo
{
    public class MusicRepository : IMusicRepository
    {
        private readonly List<MusicRecord> _music;

        public MusicRepository()
        {
            _music = new List<MusicRecord>
            {
                new MusicRecord { Id = 1, Title = "Bohemian Rhapsody", Artist = "Queen", Duration = 354, Year = 1975 },
                new MusicRecord { Id = 2, Title = "Imagine", Artist = "John Lennon", Duration = 183, Year = 1971 },
                new MusicRecord { Id = 3, Title = "Stairway to Heaven", Artist = "Led Zeppelin", Duration = 482, Year = 1971 }
            };
           
        }

        public List<MusicRecord> GetAll()
        {
            return _music;
        }

        public MusicRecord Add(MusicRecord musicRecord)
        {
            int newId = 1;

            if (_music.Count > 0)
            {
                newId = _music.Max(m => m.Id) + 1;
            }

            musicRecord.Id = newId;
            _music.Add(musicRecord);

            return musicRecord;
        }

        public MusicRecord? GetById(int id)
        {
            return _music.FirstOrDefault(m => m.Id == id);
        }

        public MusicRecord? Update(int id, MusicRecord updatedRecord)
        {
            MusicRecord? existingRecord = GetById(id);

            if (existingRecord == null)
            {
                return null;
            }

            existingRecord.Title = updatedRecord.Title;
            existingRecord.Artist = updatedRecord.Artist;
            existingRecord.Duration = updatedRecord.Duration;
            existingRecord.Year = updatedRecord.Year;

            return existingRecord;
        }

        public bool Delete(int id)
        {
            MusicRecord? musicRecord = GetById(id);

            if (musicRecord == null)
            {
                return false;
            }

            _music.Remove(musicRecord);
            return true;
        }
    }
}
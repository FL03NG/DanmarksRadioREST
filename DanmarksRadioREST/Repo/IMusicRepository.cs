using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Repo
{
    public interface IMusicRepository
    {
        List<MusicRecord> GetAll();
        MusicRecord Add(MusicRecord musicRecord);
        MusicRecord? GetById(int id);
        MusicRecord? Update(int id, MusicRecord updatedRecord);
        bool Delete(int id);
    }
}
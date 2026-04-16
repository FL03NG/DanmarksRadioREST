using DanmarksRadioREST.Models;

namespace DanmarksRadioREST.Repo
{
    public interface IMusicRepository
    {
        IEnumerable<MusicRecord> GetAll();
    }
}
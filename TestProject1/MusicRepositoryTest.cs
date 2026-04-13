using DanmarksRadioREST.Repo;
using DanmarksRadioREST.Models;
namespace TestProject1
{
    public class MusicRepositoryTest
    {
        [Fact]
        public void GetAll_ReturnsList()
        {
            var repository = new MusicRepository();
            var result = repository.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }
    }
}

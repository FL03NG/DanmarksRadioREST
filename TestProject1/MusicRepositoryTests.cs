using System.Linq;
using DanmarksRadioREST.Models;
using DanmarksRadioREST.Repo;
using Xunit;

namespace TestProject1
{
    public class MusicRepositoryTests
    {
        [Fact]
        public void GetAll_ReturnsList()
        {
            var repository = new MusicRepository();
            var result = repository.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void Add_AssignsNewIdAndIncreasesCount()
        {
            var repository = new MusicRepository();
            var initialCount = repository.GetAll().Count;

            var newRecord = new MusicRecord
            {
                Title = "New Song",
                Artist = "New Artist",
                Duration = 210,
                Year = 2024
            };

            var added = repository.Add(newRecord);

            var all = repository.GetAll();
            Assert.NotNull(added);
            Assert.Equal(initialCount + 1, all.Count);
            Assert.Equal(initialCount + 1, added.Id);
            Assert.Equal("New Song", added.Title);
            Assert.Equal("New Artist", added.Artist);
        }

        [Fact]
        public void GetById_ReturnsRecord_WhenExists()
        {
            var repository = new MusicRepository();
            var record = repository.GetById(1);
            Assert.NotNull(record);
            Assert.Equal(1, record.Id);
            Assert.Equal("Bohemian Rhapsody", record.Title);
        }

        [Fact]
        public void GetById_ReturnsNull_WhenNotExists()
        {
            var repository = new MusicRepository();
            var record = repository.GetById(999);
            Assert.Null(record);
        }

        [Fact]
        public void Update_UpdatesExistingRecord_ReturnsUpdatedRecord()
        {
            var repository = new MusicRepository();
            var updated = new MusicRecord
            {
                Id = 1, // caller-provided Id should not matter; repository preserves original Id
                Title = "Updated Title",
                Artist = "Updated Artist",
                Duration = 123,
                Year = 1999
            };

            var result = repository.Update(1, updated);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id); // id unchanged
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated Artist", result.Artist);

            var fetched = repository.GetById(1);
            Assert.NotNull(fetched);
            Assert.Equal("Updated Title", fetched.Title);
            Assert.Equal(123, fetched.Duration);
        }

        [Fact]
        public void Update_ReturnsNull_WhenNotExists()
        {
            var repository = new MusicRepository();
            var updated = new MusicRecord
            {
                Title = "X",
                Artist = "Y",
                Duration = 1,
                Year = 2000
            };

            var result = repository.Update(999, updated);
            Assert.Null(result);
        }

        [Fact]
        public void Delete_ReturnsTrue_WhenDeleted()
        {
            var repository = new MusicRepository();
            var initialCount = repository.GetAll().Count;

            var ok = repository.Delete(1);

            Assert.True(ok);
            Assert.Equal(initialCount - 1, repository.GetAll().Count);
            Assert.Null(repository.GetById(1));
        }

        [Fact]
        public void Delete_ReturnsFalse_WhenNotExists()
        {
            var repository = new MusicRepository();
            var ok = repository.Delete(999);
            Assert.False(ok);
        }
    }
}
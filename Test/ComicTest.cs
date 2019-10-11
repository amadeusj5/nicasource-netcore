using Xunit;
using System.Threading.Tasks;
using nicasource_netcore.Services;
using System;

namespace nicasource_netcore.Test
{
    public class ComicTest
    {
        private readonly ComicService _comicService;

        public ComicTest()
        {
            _comicService = new ComicService();
        }

        [Fact]
        public async Task testFirstComicHasNoPreviousUrl()
        {
            var comic = await _comicService.getAsync(1);

            Assert.Empty(comic.Previous);
        }

        [Fact]
        public async Task testLastComicHasNoNextUrl()
        {
            var comic = await _comicService.getAsync();

            Assert.Empty(comic.Next);
        }

        [Fact]
        public async Task testThrowsErrorWhenComicGreaterThanCurrent()
        {
            var current = await _comicService.getComicOfTheDay();
            var greater = current.Num + 1;

            await Assert.ThrowsAnyAsync<ArgumentNullException>(() => _comicService.getAsync(greater));
        }
    }
}

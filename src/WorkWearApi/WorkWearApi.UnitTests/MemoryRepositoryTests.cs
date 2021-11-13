using FluentAssertions;
using WorkWearApi.Services;
using Xunit;

namespace WorkWearApi.UnitTests
{
    public class MemoryRepositoryTests
    {
        [Fact]
        public void EnsureCanHandleNormalValues()
        {
            var repository = new MemoryRepository();

            repository.Add("foo", "bar");
            var value = repository.Get("foo");
            value.Should().Be("bar");
            repository.Update("foo", "baz");
            value = repository.Get("foo");
            value.Should().Be("baz");
        }
    }
}

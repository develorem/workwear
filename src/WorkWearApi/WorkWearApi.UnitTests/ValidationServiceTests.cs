using FluentAssertions;
using WorkWearApi.Services;
using Xunit;

namespace WorkWearApi.UnitTests
{
    public class ValidationServiceTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("A")]
        [InlineData("1")]
        [InlineData("~")]
        [InlineData("-")]
        [InlineData("_")]
        [InlineData("HJsd973_-~")]
        public void KeyShouldBeValid(string key)
        {
            var validationService = new ValidationService();
            var isValid = validationService.IsValidKey(key);
            isValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("&")]
        [InlineData("@")]
        [InlineData("#")]
        [InlineData("")]
        [InlineData("123456789012345678901234567890123")]
        [InlineData("aaa aaa")]
        [InlineData("hjsdkfhsdjk2037848923GY789ad ")]
        public void KeyShouldBeInvalid(string key)
        {
            var validationService = new ValidationService();
            var isValid = validationService.IsValidKey(key);
            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("&")]
        [InlineData("@")]
        [InlineData("#")]
        [InlineData("")]
        [InlineData("123456789012345678901234567890123")]
        [InlineData("aaa aaa")]
        [InlineData("hjsdkfhsdjk2037848923GY789ad ")]
        public void ValueShouldBeValid(string value)
        {
            var validationService = new ValidationService();
            var isValid = validationService.isValidValue(value);
            isValid.Should().BeTrue();
        }

        [Fact]
        public void ValueShouldBeInvalid()
        {
            var validationService = new ValidationService();

            var value = new string('x', 1025);
            var isValid = validationService.isValidValue(value);
            isValid.Should().BeFalse();
        }
    }
}

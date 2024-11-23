using GoldenRaspberryAwards.Core.Helpers;
using Xunit;

namespace GoldenRaspberryAwards.Domain.Tests.Helpers
{
    public class YesNoBooleanConverterTests
    {
        [Theory]
        [InlineData("yes", true)]
        [InlineData("no", false)]
        [InlineData("YES", true)]
        [InlineData("NO", false)]
        [InlineData("YeS", true)]
        [InlineData("nO", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void ConvertFromString_ShouldReturnCorrectBooleanValue(string text, bool expectedValue)
        {
            // Arrange
            var converter = new YesNoBooleanConverter();

            // Act
            var result = converter.ConvertFromString(text, null, null);

            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Theory]
        [InlineData(true, "yes")]
        [InlineData(false, "no")]
        public void ConvertToString_ShouldReturnCorrectStringValue(bool value, string expectedString)
        {
            // Arrange
            var converter = new YesNoBooleanConverter();

            // Act
            var result = converter.ConvertToString(value, null, null);

            // Assert
            Assert.Equal(expectedString, result);
        }
    }
}

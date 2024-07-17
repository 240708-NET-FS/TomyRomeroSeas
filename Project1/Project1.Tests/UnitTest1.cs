using Xunit;
using Project1;

namespace Project1.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Add_ShouldReturnSumOfTwoNumbers()
        {
            // Arrange
            int a = 3;
            int b = 4;

            // Act
            int result = Program.Add(a, b);

            // Assert
            Assert.Equal(7, result);
        }
    }
}

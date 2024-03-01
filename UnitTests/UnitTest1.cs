using UnitTests.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal("ONE HUNDRED DOLLARS", NumberHelper.ConvertNumberToWords(100));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal("ERROR OCCURED", NumberHelper.ConvertNumberToWords(100));
        }
    }
}
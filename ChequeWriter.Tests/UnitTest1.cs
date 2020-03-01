using System;
using Xunit;
using ChequeWriter;

namespace ChequeWriter.Tests
{
    public class UnitTest1
    {
        [Theory()]
        [InlineData("5.54", "five DOLLARS AND fifty four CENTS")]
        [InlineData("10.59", "ten DOLLARS AND fifty nine CENTS")]
        [InlineData("100.59", "one hundred DOLLARS AND fifty nine CENTS")]
        [InlineData("1000.54", "one thousand DOLLARS AND fifty four CENTS")]
        [InlineData("1000.00", "one thousand DOLLARS")]
        [InlineData("1000030.59", "one million, thirty DOLLARS AND fifty nine CENTS")]
        [InlineData("1999999999.99", "one billion, nine hundred and ninety nine million, nine hundred and ninety nine thousand, nine hundred and ninety nine DOLLARS AND ninety nine CENTS")]
        public void Test1(string number, string expectedResult)
        {
            var s = new Solution();
            var actualResult = s.ConvertToWords(number);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}

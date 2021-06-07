using System;
using TestCore.Services;
using Xunit;

namespace TestCore.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("EUW")]
        public void IRetrieveRegionService_HappyPath(string value)
        {
            var region = new IRetrieveRegionService();
            string expected = "euw1.api.riotgames.com";
            var actual = region.RetrieveRegion(value);

            Assert.Equal(expected, actual);
        }
    }
}

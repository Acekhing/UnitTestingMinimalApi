using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Services;
using Xunit;

namespace Testing.Models
{
    public class Player_IsPlayerShould
    {
        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        public void IsEliglePlayer_AgeEqualOrLessThan15_ReturnFalse(int value)
        {
            var mockPlayerService = new PlayerService();
            var actual = mockPlayerService.IsEliglePlayer(value);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(21)]
        public void IsEliglePlayer_AgeEqualOrGreaterThan20_ReturnFalse(int value)
        {
            var mockPlayerService = new PlayerService();
            var actual = mockPlayerService.IsEliglePlayer(value);
            Assert.False(actual);
        }
    }
}

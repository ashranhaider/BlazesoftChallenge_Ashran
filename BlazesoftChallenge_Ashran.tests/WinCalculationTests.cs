using BlazesoftChallenge_Ashran.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazesoftChallenge_Ashran.tests
{
    public class WinCalculationTests
    {
        private readonly WinCalculator _calculator;

        public WinCalculationTests()
        {
            _calculator = new WinCalculator();
        }
        #region Straight Win Tests
        [Fact]
        public void StraightWin_ShouldCalculateCorrectly_ForSingleWinningRow()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },   // win (3*3)
                new[] { 2, 3, 2, 3, 3 },   // no win
                new[] { 1, 2, 3, 3, 3 }    // no win (starts with 1 only once)
            };

            decimal bet = 2;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(3 * 3 * 2, result);
        }
        // Multiple Row Wins
        [Fact]
        public void StraightWin_ShouldSumMultipleWinningRows()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },  // 9
                new[] { 2, 2, 2, 3, 3 },  // 6
                new[] { 1, 2, 3, 3, 3 }   // no win
            };

            decimal bet = 1;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(9 + 6, result);
        }
        // No Wins
        [Fact]
        public void StraightWin_ShouldReturnZero_WhenNoValidStreak()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 4, 4, 5 },
                new[] { 2, 3, 2, 3, 3 },
                new[] { 1, 2, 3, 3, 3 }
            };

            decimal bet = 5;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(0, result);
        }
        // Long Streak

        [Fact]
        public void StraightWin_ShouldHandleFullRowMatch()
        {
            int[][] matrix =
            {
                new[] { 7, 7, 7, 7, 7 },
                new[] { 1, 2, 3, 4, 5 }
            };

            decimal bet = 1;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(7 * 5, result);
        }

        #endregion

        #region Zigzag Win Tests

        [Fact]
        public void Zigzag_ShouldCalculateCorrectWin_ForTopRow()
        {
            //Streak = 4
            // Win = 3 * 5 * bet
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },
                new[] { 2, 3, 2, 3, 3 },
                new[] { 1, 2, 3, 3, 3 }
            };

            decimal bet = 1;

            var result = _calculator.CalculateZigzagWins(matrix, bet);
            decimal expectedWin = (3+3+3+3) + (2+2+2) * bet; // first value (3) * count (4) * bet (1)   
            Assert.Equal(expectedWin, result);
        }
        [Fact]
        public void Zigzag_ShouldReturnZero_WhenNoStreak()
        {
            int[][] matrix =
            {
                new[] { 3, 4, 3, 4, 5 },
                new[] { 2, 1, 4, 3, 3 },
                new[] { 1, 2, 3, 3, 3 }
            };

            decimal bet = 2;

            var result = _calculator.CalculateZigzagWins(matrix, bet);

            Assert.Equal(0, result);
        }
        [Fact]
        public void Zigzag_ShouldReturnZero_WhenHeightIsOne()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 3, 3 }
            };

            decimal bet = 5;

            var result = _calculator.CalculateZigzagWins(matrix, bet);

            Assert.Equal(0, result);
        }

        #endregion

        [Fact]
        public void TotalWin_ShouldCombineStraightAndZigzag()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },
                new[] { 2, 2, 2, 3, 3 },
                new[] { 1, 2, 3, 3, 3 }
            };

            decimal bet = 1;

            decimal straightWin = (3 * 3 * bet) + (2 * 3 * bet); // Row 1 + Row 2
            decimal zigzagWin = (2 * 3 * bet); // Top row zigzag streak of 4

            var result = _calculator.CalculateTotalWin(matrix, bet);

            Assert.Equal(straightWin + zigzagWin, result);
        }

    }
}

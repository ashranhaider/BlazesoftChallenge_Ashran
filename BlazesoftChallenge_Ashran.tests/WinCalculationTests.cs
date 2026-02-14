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
        public void StraightWin_ShouldReturnZero_ForEmptyMatrix()
        {
            int[][] matrix = Array.Empty<int[]>();

            decimal bet = 2;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(0, result);
        }
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
        [Fact]
        public void StraightWin_ShouldHandleOnlyColumnValues()
        {
            int[][] matrix =
            {
                new[] { 7},
                new[] { 1},
                new[] { 2},
                new[] { 8}
            };

            decimal bet = 2;

            var result = _calculator.CalculateStraightWins(matrix, bet);

            Assert.Equal(0, result);
        }
        #endregion

        #region Zigzag Win Tests
        [Fact]
        public void Zigzag_ShouldReturnZero_ForEmptyMatrix()
        {
            int[][] matrix = Array.Empty<int[]>();

            decimal bet = 2;

            var result = _calculator.CalculateZigzagWins(matrix, bet);

            Assert.Equal(0, result);
        }
        [Fact]
        public void Zigzag_ShouldCalculateCorrectWin_ForTopRow()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },
                new[] { 2, 3, 2, 3, 3 },
                new[] { 1, 2, 3, 3, 3 }
            };

            decimal bet = 2;

            var result = _calculator.CalculateZigzagWins(matrix, bet);
            decimal expectedWin = (3 * 4 * bet) + (2 * 3 * bet);

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
        public void Zigzag_ShouldWork_ForHeightFour()
        {
            int[][] matrix =
            {
                new[] { 0, 0, 0, 0 },
                new[] { 7, 1, 0, 0 },
                new[] { 0, 7, 6, 7 },
                new[] { 0, 0, 7, 6 }
            };

            decimal bet = 2;

            var result = _calculator.CalculateZigzagWins(matrix, bet);
            decimal expectedWin = (7 * 4 * bet);

            Assert.Equal(expectedWin, result);
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

        #region Combined Win Tests

        [Fact]
        public void TotalWin_ShouldCombineStraightAndZigzag()
        {
            int[][] matrix =
            {
                new[] { 3, 3, 3, 4, 5 },
                new[] { 2, 2, 2, 3, 3 },
                new[] { 2, 2, 3, 3, 3 }
            };

            decimal bet = 1;

            decimal straightWin = (3 * 3 * bet) + (2 * 3 * bet); 
            decimal zigzagWin = (2 * 3 * bet); 

            var result = _calculator.CalculateTotalWin(matrix, bet);

            Assert.Equal(straightWin + zigzagWin, result);
        }
        [Fact]
        public void ShouldReturnZero_WhenWidthLessThanThree()
        {
            int[][] matrix =
            {
                new[] { 5, 5 },
                new[] { 5, 5 }
            };

            decimal bet = 10;

            var result = _calculator.CalculateTotalWin(matrix, bet);

            Assert.Equal(0, result);
        }
        [Fact]
        public void ShouldHandleAllZero_WhenAllValuesZero()
        {
            int[][] matrix =
            {
                new[] { 0, 0 ,0, 0},
                new[] { 0, 0 ,0, 0},
                new[] { 0, 0 ,0, 0},

            };

            decimal bet = 10;

            var result = _calculator.CalculateTotalWin(matrix, bet);

            Assert.Equal(0, result);
        }
        [Fact]
        public void TotalWin_ShouldHandleAllSameValuesMatrix()
        {
            int[][] matrix =
            {
                new[] { 2, 2, 2, 2 },
                new[] { 2, 2, 2, 2 },
                new[] { 2, 2, 2, 2 },
            };

            decimal bet = 2;

            var result = _calculator.CalculateTotalWin(matrix, bet);

            decimal straightWin = 3 * (2 * 4 * bet); // 3 rows
            decimal zigzagWin = 3 * (2 * 4 * bet);   // 3 zigzag lines

            decimal expected = straightWin + zigzagWin;

            Assert.Equal(expected, result);
        }
        [Fact]
        public void ShouldReturnZero_ForEmptyMatrix()
        {
            int[][] matrix = Array.Empty<int[]>();

            decimal bet = 1;

            var result = _calculator.CalculateTotalWin(matrix, bet);

            Assert.Equal(0, result);
        }
        #endregion
    }
}

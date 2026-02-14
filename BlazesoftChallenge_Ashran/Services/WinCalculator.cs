namespace BlazesoftChallenge_Ashran.Services
{
    public class WinCalculator : IWinCalculator
    {
        public decimal CalculateStraightWins(int[][] matrix, decimal bet)
        {
            decimal totalWin = 0;

            for (int row = 0; row < matrix.Length; row++)
            {
                int firstValue = matrix[row][0];
                int count = 1;

                for (int col = 1; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == firstValue)
                        count++;
                    else
                        break;
                }

                if (count > 2)
                    totalWin += firstValue * count * bet;
            }

            return totalWin;
        }


        public decimal CalculateZigzagWins(int[][] matrix, decimal bet)
        {
            return 0;
        }

        public decimal CalculateTotalWin(int[][] matrix, decimal bet)
        {
            return CalculateStraightWins(matrix, bet)
                 + CalculateZigzagWins(matrix, bet);
        }

    }
}

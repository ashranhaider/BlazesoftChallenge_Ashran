namespace BlazesoftChallenge_Ashran.Services
{
    public class WinCalculator : IWinCalculator
    {
        public decimal CalculateStraightWins(int[][] matrix, decimal bet)
        {
            decimal total = 0;

            for (int row = 0; row < matrix.Length; row++)
            {
                total += CalculateLineWin(matrix[row], bet);
            }

            return total;
        }

        public decimal CalculateZigzagWins(int[][] matrix, decimal bet)
        {
            int height = matrix.Length;

            if (height <= 1)
                return 0;
            
            decimal total = 0;

            for (int startRow = 0; startRow < height; startRow++)
            {
                int[] zigzagLine = BuildZigzagLine(matrix, startRow);
                total += CalculateLineWin(zigzagLine, bet);
            }

            return total;
        }

        public decimal CalculateTotalWin(int[][] matrix, decimal bet)
        {
            return CalculateStraightWins(matrix, bet)
                 + CalculateZigzagWins(matrix, bet);
        }

        private decimal CalculateLineWin(int[] line, decimal bet)
        {
            if (line.Length == 0)
                return 0;

            int firstValue = line[0];
            int count = 1;

            for (int i = 1; i < line.Length; i++)
            {
                if (line[i] == firstValue)
                    count++;
                else
                    break;
            }

            if (count > 2)
                return firstValue * count * bet;

            return 0;
        }
        private int[] BuildZigzagLine(int[][] matrix, int startRow) 
        {
            if(startRow < 0 || startRow >= matrix.Length)
                throw new ArgumentOutOfRangeException(nameof(startRow), "Start row must be within the bounds of the matrix.");
                        
            if (matrix.Length <= 1)
                return new int[0];

            int width = matrix[0].Length;
            int direction = 1; // 1 for down-right, -1 for up-right
            int row = startRow;
            int[] zigzagLine = new int[width];

            for (int col = 0; col < width; col++)
            {
                zigzagLine[col] = matrix[row][col];

                if (row == matrix.Length - 1) 
                {
                    direction = -1; // Change direction to up-right
                }
                else if(row == 0) 
                {
                    direction = 1; // Change direction to down-right
                }

                row += direction;
            }

            return zigzagLine;
        }
    }
}

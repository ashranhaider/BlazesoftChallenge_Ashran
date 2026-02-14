namespace BlazesoftChallenge_Ashran.Services
{
    public interface IWinCalculator
    {
        public decimal CalculateStraightWins(int[][] matrix, decimal bet);
        public decimal CalculateZigzagWins(int[][] matrix, decimal bet);
        public decimal CalculateTotalWin(int[][] matrix, decimal bet);
    }
}

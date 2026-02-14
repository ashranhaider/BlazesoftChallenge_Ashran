namespace BlazesoftChallenge_Ashran.Helpers
{
    public class Utilities
    {
        public static int[][] GenerateMatrix(int width, int height)
        {
            var matrix = new int[height][];

            for (int row = 0; row < height; row++)
            {
                matrix[row] = new int[width];

                for (int col = 0; col < width; col++)
                {
                    matrix[row][col] = Random.Shared.Next(0, 10);
                }
            }

            return matrix;
        }
    }
}

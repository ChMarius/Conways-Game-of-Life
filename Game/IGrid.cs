namespace GameOfLife
{
    public interface IGrid
    {
        public void SetGeneration(int generation);
        public void UpdateGrid(Cell[,] newGridState);
    }
}
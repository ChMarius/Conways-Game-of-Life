namespace GameOfLife
{
    public interface IGrid
    {
        public void SetGeneration(int generation);
        public void UpdateGrid(List<List<Cell>> newGridState);
    }
}
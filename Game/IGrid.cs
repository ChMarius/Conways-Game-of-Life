namespace GameOfLife
{
    public interface IGrid
    {
        public Cell[,] Cells { get; set; }
        public void Randomize();
        public void Update(Cell[,] newGrid);
        public int GetLiveCellNeighborCount(int x, int y);
    }
}
namespace GameOfLife
{
    public interface ICell
    {
        public bool GetCellState();
        public void SetCellState(bool cellState);
    }
}
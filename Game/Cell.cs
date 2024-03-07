namespace GameOfLife
{
    public class Cell : ICell
    {
        public bool CellState { get; private set; }
        public bool GetCellState()
        {
            return CellState; // alive if true, dead if false
        }
        public void SetCellState(bool cellState)
        {
            CellState = cellState;
        }
    }
}
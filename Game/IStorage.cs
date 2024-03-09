namespace GameOfLife
{
    public interface IStorage
    {
        public void StoreGrid(Grid grid);
        public Grid LoadGrid(string filePath);
    }
}
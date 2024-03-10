using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class GridTests
    {
        [Fact]
        public void Randomize_GridHasRandomStates()
        {
            // Arrange
            Grid grid = new Grid(5, 5);

            // Act
            grid.Randomize();

            // Assert
            bool allCellsAreNotSame = false;
            for (int i = 0; i < grid.Columns; i++)
            {
                for (int j = 0; j < grid.Rows; j++)
                {
                    if (grid.Cells[i, j].GetCellState() != grid.Cells[0, 0].GetCellState())
                    {
                        allCellsAreNotSame = true;
                        break;
                    }
                }
                if (allCellsAreNotSame)
                    break;
            }

            Assert.True(allCellsAreNotSame); // At least one cell should have a different state
        }

        [Fact]
        public void SetGeneration_GenerationIsUpdated()
        {
            // Arrange
            Grid grid = new Grid(5, 5);
            int generation = 10;

            // Act
            grid.SetGeneration(generation);

            // Assert
            Assert.Equal(generation, grid.Generation); // Generation should be updated
        }

        [Fact]
        public void Update_NewGridDimensionsMatchExistingGrid_UpdatedSuccessfully()
        {
            // Arrange
            Grid grid = new Grid(3, 3);
            Cell[,] newGrid = new Cell[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    newGrid[i, j] = new Cell();

            // Act
            grid.Update(newGrid);

            // Assert
            Assert.Equal(newGrid, grid.Cells); // Grid should be updated successfully
        }

        [Fact]
        public void GetLiveCellNeighborCount_CalculateCorrectly()
        {
            // Arrange
            Grid grid = new Grid(3, 3);
            grid.Cells[0, 0].SetCellState(true);
            grid.Cells[0, 1].SetCellState(true);
            grid.Cells[1, 0].SetCellState(true);

            // Act
            int liveNeighbors = grid.GetLiveCellNeighborCount(1, 1);

            // Assert
            Assert.Equal(3, liveNeighbors); // Should have 3 live neighbors
        }
    }
}
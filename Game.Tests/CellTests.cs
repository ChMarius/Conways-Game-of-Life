using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class CellTests
    {
        [Fact]
        public void SetCellState_ChangesCellState()
        {
            // Arrange
            Cell cell = new Cell();

            // Act
            cell.SetCellState(true);

            // Assert
            Assert.True(cell.GetCellState()); // Check if state is correctly set to false
        }

        [Fact]
        public void GetCellState_DefaultStateIsFalse()
        {
            // Arrange
            Cell cell = new Cell();

            // Assert
            Assert.False(cell.GetCellState()); // Check if the default state is false
        }
    }
}
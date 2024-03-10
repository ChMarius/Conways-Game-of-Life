using Xunit;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class AutomatonSimulatorTests
    {
        [Fact]
        public void ApplyRules_OneLiveCell_NoChange()
        {
            // Arrange
            Grid grid = new Grid(3, 3);
            grid.Cells[1, 1].SetCellState(true);

            // Act
            var result = AutomatonSimulator.ApplyRules(grid);

            // Assert
            Assert.False(result.Cells[1,1].GetCellState()); // The live cell should die due to underpopulation
        }

        [Fact]
        public void ApplyRules_DeadCellWithThreeNeighbors_CellBecomesAlive()
        {
            // Arrange
            Grid grid = new Grid(3, 3);
            grid.Cells[0, 0].SetCellState(true);
            grid.Cells[0, 1].SetCellState(true);
            grid.Cells[1, 0].SetCellState(true);

            // Act
            var result = AutomatonSimulator.ApplyRules(grid);

            // Assert
            Assert.True(result.Cells[1, 1].GetCellState()); // A new cell should be born due to reproduction
        }
    }
}
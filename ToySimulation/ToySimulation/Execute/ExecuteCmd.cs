namespace ToySimulation
{
    public class ExecuteCmd
    {
        private int currentRow;
        private int currentCell;
        private Direction currentDirection;
        private readonly Validator validator;
        public ExecuteCmd(Validator Validator)
        {
            currentRow = -1;
            currentCell = -1;
            currentDirection = Direction.NORTH;
            validator = Validator;
        }

        /// <summary>
        /// Place the robot in the table
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="newCell"></param>
        /// <param name="direction"></param>
        public void Place(int newRow, int newCell, Direction direction)
        {
            currentRow = newRow;
            currentCell = newCell;
            currentDirection = direction;
        }

        /// <summary>
        /// Move the roboat one step forward
        /// </summary>
        public void Move()
        {
            int newRow = currentRow;
            int newCell = currentCell;

            switch (currentDirection)
            {
                case Direction.NORTH:
                    newRow++;
                    break;
                case Direction.SOUTH:
                    newRow--;
                    break;
                case Direction.EAST:
                    newCell++;
                    break;
                case Direction.WEST:
                    newCell--;
                    break;
            }

            if (validator.IsValidPosition(newRow, newCell))
            {
                currentRow = newRow;
                currentCell = newCell;
            }
        }

        /// <summary>
        /// Rotate the robot and get the current direction
        /// </summary>
        /// <param name="degrees"></param>
        public void Rotate(int degrees)
        {
            int normalizedDegrees = (degrees + 360) % 360;

            currentDirection = (Direction)(((int)currentDirection + normalizedDegrees + 360) % 360 / 90);
        }

        /// <summary>
        /// Report the current position and direction of the robot
        /// </summary>
        public void Report()
        {
            Console.WriteLine($"Robot is at position ({currentRow}, {currentCell}) direction {currentDirection}.");
        }

        #region Helper
        public int GetCurrentRow()
        {
            return currentRow;
        }

        public int GetCurrentCell()
        {
            return currentCell;
        }
        #endregion Helper
    }
}

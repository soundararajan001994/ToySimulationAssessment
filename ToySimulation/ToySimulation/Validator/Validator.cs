namespace ToySimulation
{
    public class Validator
    {
        private readonly int tableSize;

        public Validator(int size)
        {
            tableSize = size;
        }

        //valiadte robot was placed insid ethe table or not
        public bool IsValidPosition(int row, int cell)
        {
            return row >= 0 && row < tableSize && cell >= 0 && cell < tableSize;
        }
    }
}

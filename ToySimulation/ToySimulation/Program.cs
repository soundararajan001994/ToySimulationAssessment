namespace ToySimulation
{
    public static class Program
    {
        /// <summary>
        /// The table size is set to 5 by default.
        /// </summary>
        private static Table table = new Table();
        private static ExecuteCmd executeCmd;
        private static Validator validator;


        static Program()
        {
            validator = new Validator(table.tableSize);
            executeCmd = new ExecuteCmd(validator);
        }

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter a command :");
                var cmd = Console.ReadLine() ?? string.Empty;
                string input = cmd.Trim().ToUpper();
                // Exit the program if the user enters "EXIT".
                if (input == "EXIT")
                {
                    break;
                }
              
                ProcessCommand(input);
            }

            Console.WriteLine("Program exited. Press any key to close the console window...");
            Console.ReadKey();
        }

        static void ProcessCommand(string input)
        {
            string[] parts = input.Split(' ');
            // Check if the command is valid. Get first word of the command and check is valid or not.
            if (Enum.TryParse(parts[0], out Command command))
            {
                switch (command)
                {
                    case Command.PLACE:
                        if (parts.Length == 2)
                        {
                            string[] position = parts[1].Split(',');
                            if (position.Length == 3 && int.TryParse(position[0], out int newRow) && int.TryParse(position[1], out int newCell) && Enum.TryParse(position[2], out Direction direction))
                            {
                                if (validator.IsValidPosition(newRow, newCell))
                                {
                                    executeCmd.Place(newRow, newCell, direction);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("The robot was placed in the table");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("The robot was placed out of the table");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid command. Please enter a valid command.");
                                Console.ResetColor();
                            }
                        }
                        break;
                    case Command.MOVE:
                        if (validator.IsValidPosition(executeCmd.GetCurrentRow(), executeCmd.GetCurrentCell()))
                        {
                            executeCmd.Move();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The robot was one step moved");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The robot can't move forward on that direction, it may fall off the table.");
                            Console.ResetColor();
                        }
                        break;
                    case Command.LEFT:
                        executeCmd.Rotate(-90);
                        break;
                    case Command.RIGHT:
                        executeCmd.Rotate(90);
                        break;
                    case Command.REPORT:
                        executeCmd.Report();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command. Please enter a valid command.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
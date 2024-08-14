namespace Matrix
{
    internal class Program
    {
        static Random random = new Random();
        static List<(int x, int y)> characterPositions = new List<(int x, int y)>();

        static void Main()
        {
            // Hide the cursor to improve aesthetics
            Console.CursorVisible = false;

            // Get the dimensions of the console window
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // Create an array of characters to be used randomly
            char[] characters = "0Ѭ1#$%&£ÇABΠCDアンケートΓEFG∆HIアルバイJKLMNΘカステOPQR你好STUV始めましてエヅアドともうどぞよろしくおねがいしますΛWXYΩZ01234Υ56Σ789".ToCharArray();

            // Generate random X and Y positions for the columns
            int numberOfColumns = 70;
            int[] columnPositionsX = new int[numberOfColumns];
            int[] columnHeights = new int[numberOfColumns];
            int[] columnPositionsY = new int[numberOfColumns]; // New list for Y positions

            for (int i = 0; i < numberOfColumns; i++)
            {
                columnPositionsX[i] = random.Next(width);
                columnPositionsY[i] = random.Next(height); // Assign random Y position
                columnHeights[i] = columnPositionsY[i]; // Initialize column heights with the Y position
            }

            while (true)
            {
                // Update random characters on the console
                char randomCharacter = characters[random.Next(characters.Length)];
                int randomX = random.Next(width);
                int randomY = random.Next(height);

                bool isNear = false;

                foreach (var pos in characterPositions)
                {
                    if (Math.Abs(pos.x - randomX) <= 1 && Math.Abs(pos.y - randomY) <= 1)
                    {
                        // Clear the previous character
                        Console.SetCursorPosition(pos.x, pos.y);
                        Console.Write(' ');

                        // Remove the previous position from the list
                        characterPositions.Remove(pos);
                        isNear = true;
                        break;
                    }
                }

                // If no conflict, add the new position
                if (!isNear)
                {
                    characterPositions.Add((randomX, randomY));
                }

                // Set the cursor position and write the random character
                Console.SetCursorPosition(randomX, randomY);
                Console.ForegroundColor = ConsoleColor.Green;
                //Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Write(randomCharacter);

                // Brief pause to observe the change
                Thread.Sleep(30);

                // Update the columns
                for (int i = 0; i < numberOfColumns; i++)
                {
                    int x = columnPositionsX[i];
                    int y = columnHeights[i];

                    // Paint the previous character in green (if not at the top)
                    if (y > 0 && y < height)
                    {
                        Console.SetCursorPosition(x, y - 1);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(characters[random.Next(characters.Length)]);
                    }

                    // Paint the current character in white
                    if (y < height)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(characters[random.Next(characters.Length)]);
                    }

                    columnHeights[i]++;

                    // If the column has reached the bottom, restart
                    if (columnHeights[i] >= height)
                    {
                        // Clear the column
                        for (int j = 0; j < height; j++)
                        {
                            Console.SetCursorPosition(x, j);
                            Console.Write(' ');
                        }
                        // Restart the column position
                        columnPositionsX[i] = random.Next(width);
                        columnHeights[i] = 0;
                        columnPositionsY[i] = random.Next(height); // Restart random Y position
                    }
                }

                // Brief pause for the falling effect
                Thread.Sleep(30);
            }
        }
    }
}

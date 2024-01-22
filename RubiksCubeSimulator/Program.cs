
namespace RubiksCubeSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool endApp = false;
                // Display title as the C# console calculator app.
                var cube = new RubiksCube();
                Console.WriteLine("Welcome to the rubiks cube simulator!");
                Console.WriteLine("This is the current state of the rubiks cube:");
                Console.WriteLine("\n");
                cube.PrintCube();
                Console.WriteLine("\n");
                Console.WriteLine("At any point you can type 'print' and I shall print the cube for you!");
                Console.WriteLine("Please enter your command using the words 'front', 'top', 'back', 'bottom', 'right', 'left' for the faces and either c (clockwise) or a (anticlockwise) for the rotation");

                while (!endApp)
                {
                    string result = Console.ReadLine();
                    if (result == "n")
                    {
                        endApp = true;
                        break;
                    }
                    else if (result == "print")
                    {
                        cube.PrintCube();
                    }
                    else if (result == "front c")
                    {
                        cube.RotateFace("Front", true);
                    }
                    else if (result == "front a")
                    {
                        cube.RotateFace("Front", false);
                    }
                    else if (result == "top c")
                    {
                        cube.RotateFace("Top", true);
                    }
                    else if (result == "top a")
                    {
                        cube.RotateFace("Top", false);
                    }
                    else if (result == "back c")
                    {
                        cube.RotateFace("Back", true);
                    }
                    else if (result == "back a")
                    {
                        cube.RotateFace("Back", false);
                    }
                    else if (result == "bottom c")
                    {
                        cube.RotateFace("Bottom", true);
                    }
                    else if (result == "bottom a")
                    {
                        cube.RotateFace("Bottom", false);
                    }
                    else if (result == "right c")
                    {
                        cube.RotateFace("Right", true);
                    }
                    else if (result == "right a")
                    {
                        cube.RotateFace("Right", false);
                    }
                    else if (result == "left c")
                    {
                        cube.RotateFace("Left", true);
                    }
                    else if (result == "left a")
                    {
                        cube.RotateFace("Left", false);
                    }
                    else
                    {
                        Console.WriteLine("INVALID INPUT, TRY AGAIN!");
                        continue;
                    }

                    Console.WriteLine("Done! Please enter your next command :)");
                }
                return;
            }
            catch (Exception ex)
            {
                //Logger.log(ex.Message, ex.StackTrace);
            }
}
    }
}
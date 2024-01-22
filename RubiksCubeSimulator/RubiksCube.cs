using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCubeSimulator
{
    internal class RubiksCube
    {
        static string Front = "Front";
        static string Back = "Back";
        static string Right = "Right";
        static string Left = "Left";
        static string Bottom = "Bottom";
        static string Top = "Top";

        enum ShiftDirection
        {
            Left,
            Right,
            Up,
            Down
        }

        enum ShiftType
        {
            Row,
            Column
        }

        private Dictionary<string, string[,]> Faces = new Dictionary<string, string[,]>();

        public RubiksCube()
        {
            Faces[Front] = new string[,]{
            {"W", "W", "W"},
            {"W", "W", "W"},
            {"W", "W", "W"}
            };

            Faces[Top] = new string[,]{
            {"R", "R", "R"},
            {"R", "R", "R"},
            {"R", "R", "R"}
            };

            Faces[Left] = new string[,]{
            {"B", "B", "B"},
            {"B", "B", "B"},
            {"B", "B", "B"}
            };

            Faces[Right] = new string[,]{
            {"G", "G", "G"},
            {"G", "G", "G"},
            {"G", "G", "G"}
            };

            Faces[Back] = new string[,]{
            {"Y", "Y", "Y"},
            {"Y", "Y", "Y"},
            {"Y", "Y", "Y"}
            };

            Faces[Bottom] = new string[,]{
            {"O", "O", "O"},
            {"O", "O", "O"},
            {"O", "O", "O"}
            };
        }


        /// <summary>
        /// Was used during testing, no longer needed
        /// </summary>
        /// <param name="matrix"></param>
        static void PrintMatrix(string[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Rotates a 2D Matrix of any size clockwise
        /// </summary>
        /// <param name="matrix"></param>
        static string[,] RotateMatrixClockwise(string[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string[,] rotatedMatrix = new string[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedMatrix[j, rows - 1 - i] = matrix[i, j];
                }
            }

            return rotatedMatrix;
        }

        /// <summary>
        /// Rotates a 2D Matrix of any size anticlockwise
        /// </summary>
        /// <param name="matrix"></param>
        static string[,] RotateMatrixAnticlockwise(string[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string[,] rotatedMatrix = new string[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotatedMatrix[cols - 1 - j, i] = matrix[i, j];
                }
            }

            return rotatedMatrix;
        }

        /// <summary>
        /// Combines four 3 by 3 matrices into one 3 by 12 matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrix3"></param>
        /// <param name="matrix4"></param>
        /// <returns></returns>
        static string[,] CombineMatricesHorizontally(string[,] matrix1, string[,] matrix2, string[,] matrix3, string[,] matrix4)
        {
            int rows = 3;
            int cols = 12;

            string[,] combinedMatrix = new string[rows, cols];

            // Copy each matrix into the combined matrix
            CopyMatrix(matrix1, combinedMatrix, 0);
            CopyMatrix(matrix2, combinedMatrix, 3);
            CopyMatrix(matrix3, combinedMatrix, 6);
            CopyMatrix(matrix4, combinedMatrix, 9);   

            return combinedMatrix;
        }

        /// <summary>
        /// Combines four 3 by 3 matrices into one 3 by 12 matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrix3"></param>
        /// <param name="matrix4"></param>
        /// <returns></returns>
        static string[,] CombineMatricesVertically(string[,] matrix1, string[,] matrix2, string[,] matrix3, string[,] matrix4)
        {
            var result = CombineMatricesHorizontally(RotateMatrixAnticlockwise(matrix1), RotateMatrixAnticlockwise(matrix2), RotateMatrixAnticlockwise(matrix3), RotateMatrixAnticlockwise(matrix4));
            
            return result;
        }

        /// <summary>
        /// Splits a 3 by 12 matrix into four 3 by 3 matrices
        /// </summary>
        /// <param name="combinedMatrix"></param>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="matrix3"></param>
        /// <param name="matrix4"></param>
        static void SplitMatrix(string[,] combinedMatrix, out string[,] matrix1, out string[,] matrix2, out string[,] matrix3, out string[,] matrix4)
        {
            int rows = combinedMatrix.GetLength(0);
            int cols = combinedMatrix.GetLength(1);

            matrix1 = new string[3, 3];
            matrix2 = new string[3, 3];
            matrix3 = new string[3, 3];
            matrix4 = new string[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix1[i, j] = combinedMatrix[i, j];
                    matrix2[i, j] = combinedMatrix[i, j + 3];
                    matrix3[i, j] = combinedMatrix[i, j + 6];
                    matrix4[i, j] = combinedMatrix[i, j + 9];
                }
            }
        }

        /// <summary>
        /// Copies source matrix to a destination matrix, used to create the 3 by 12 matrix
        /// </summary>
        /// <param name="sourceMatrix"></param>
        /// <param name="destinationMatrix"></param>
        /// <param name="startCol"></param>
        static void CopyMatrix(string[,] sourceMatrix, string[,] destinationMatrix, int startCol)
        {
            int size = 3;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    destinationMatrix[i, startCol + j] = sourceMatrix[i, j];
                }
            }
        }
        
        /// <summary>
        /// Shifts a row of a matrix 3 spaces to the left or right and can also shift columns up or down 3 spaces (not used anymore)
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="index"></param>
        /// <param name="direction"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        static string[,] ShiftMatrixRowOrColumnBy3(string[,] matrix, int index, ShiftDirection direction, ShiftType type)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            string[,] shiftedMatrix = matrix.Clone() as string[,];

            switch (direction)
            {
                case ShiftDirection.Left:
                case ShiftDirection.Right:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            int newIndex = (direction == ShiftDirection.Left) ? (j + cols - 3) % cols : (j + 3) % cols;
                            shiftedMatrix[i, newIndex] = (type == ShiftType.Row && i == index) ? matrix[i, j] : shiftedMatrix[i, newIndex];
                        }
                    }
                    break;
                case ShiftDirection.Up:
                case ShiftDirection.Down:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            int newIndex = (direction == ShiftDirection.Up) ? (i + rows - 3) % rows : (i + 3) % rows;
                            shiftedMatrix[newIndex, j] = (type == ShiftType.Column && j == index) ? matrix[i, j] : shiftedMatrix[newIndex, j];
                        }
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid shift direction");
            }

            return shiftedMatrix;
        }

        /// <summary>
        /// Prints cube in 2D representation
        /// </summary>
        public void PrintCube()
        {
            try
            {
                Console.WriteLine($"|||{Faces[Top][0, 0]}{Faces[Top][0, 1]}{Faces[Top][0, 2]}");
                Console.WriteLine($"|||{Faces[Top][1, 0]}{Faces[Top][1, 1]}{Faces[Top][1, 2]}");
                Console.WriteLine($"|||{Faces[Top][2, 0]}{Faces[Top][2, 1]}{Faces[Top][2, 2]}");
                Console.WriteLine($"{Faces[Left][0, 0]}{Faces[Left][0, 1]}{Faces[Left][0, 2]}{Faces[Front][0, 0]}{Faces[Front][0, 1]}{Faces[Front][0, 2]}{Faces[Right][0, 0]}{Faces[Right][0, 1]}{Faces[Right][0, 2]}{Faces[Back][0, 0]}{Faces[Back][0, 1]}{Faces[Back][0, 2]}");
                Console.WriteLine($"{Faces[Left][1, 0]}{Faces[Left][1, 1]}{Faces[Left][1, 2]}{Faces[Front][1, 0]}{Faces[Front][1, 1]}{Faces[Front][1, 2]}{Faces[Right][1, 0]}{Faces[Right][1, 1]}{Faces[Right][1, 2]}{Faces[Back][1, 0]}{Faces[Back][1, 1]}{Faces[Back][1, 2]}");
                Console.WriteLine($"{Faces[Left][2, 0]}{Faces[Left][2, 1]}{Faces[Left][2, 2]}{Faces[Front][2, 0]}{Faces[Front][2, 1]}{Faces[Front][2, 2]}{Faces[Right][2, 0]}{Faces[Right][2, 1]}{Faces[Right][2, 2]}{Faces[Back][2, 0]}{Faces[Back][2, 1]}{Faces[Back][2, 2]}");
                Console.WriteLine($"|||{Faces[Bottom][0, 0]}{Faces[Bottom][0, 1]}{Faces[Bottom][0, 2]}");
                Console.WriteLine($"|||{Faces[Bottom][1, 0]}{Faces[Bottom][1, 1]}{Faces[Bottom][1, 2]}");
                Console.WriteLine($"|||{Faces[Bottom][2, 0]}{Faces[Bottom][2, 1]}{Faces[Bottom][2, 2]}");
            }
            catch (Exception ex)
            {
                //Logger.log("PrintCube", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Rotates specified space clockwise or anticlockwise
        /// </summary>
        /// <param name="face"></param>
        /// <param name="clockwise"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RotateFace(string face, bool clockwise)
        {
            switch (face)
            {
                case "Front":
                    if (clockwise)
                    {
                        Faces[Front] = RotateMatrixClockwise(Faces[Front]);

                        Faces[Left] = RotateMatrixClockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixAnticlockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Top], Faces[Right], Faces[Bottom]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Top] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Bottom] = matrix4;

                        Faces[Left] = RotateMatrixAnticlockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixClockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                    }
                    else
                    {
                        Faces[Front] = RotateMatrixAnticlockwise(Faces[Front]);
                        
                        Faces[Left] = RotateMatrixClockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixAnticlockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Top], Faces[Right], Faces[Bottom]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Top] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Bottom] = matrix4;

                        Faces[Left] = RotateMatrixAnticlockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixClockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                    }
                    break;
                case "Top":
                    if (clockwise)
                    {
                        Faces[Top] = RotateMatrixClockwise(Faces[Top]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Front], Faces[Right], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Front] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Back] = matrix4;
                    }
                    else
                    {
                        Faces[Top] = RotateMatrixAnticlockwise(Faces[Top]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Front], Faces[Right], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Front] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Back] = matrix4;
                    }
                    break;
                case "Left":
                    if (clockwise)
                    {
                        Faces[Left] = RotateMatrixClockwise(Faces[Left]);

                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);

                        var result = CombineMatricesVertically(Faces[Top], Faces[Front], Faces[Bottom], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Top] = RotateMatrixClockwise(matrix1);
                        Faces[Front] = RotateMatrixClockwise(matrix2);
                        Faces[Bottom] = RotateMatrixClockwise(matrix3);
                        Faces[Back] = RotateMatrixClockwise(matrix4);

                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                    }
                    else
                    {
                        Faces[Left] = RotateMatrixAnticlockwise(Faces[Left]);

                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);

                        var result = CombineMatricesVertically(Faces[Top], Faces[Front], Faces[Bottom], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Top] = RotateMatrixClockwise(matrix1);
                        Faces[Front] = RotateMatrixClockwise(matrix2);
                        Faces[Bottom] = RotateMatrixClockwise(matrix3);
                        Faces[Back] = RotateMatrixClockwise(matrix4);

                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                    }
                    break;
                case "Right":
                    if (clockwise)
                    {
                        Faces[Right] = RotateMatrixClockwise(Faces[Right]);

                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);

                        var result = CombineMatricesVertically(Faces[Top], Faces[Front], Faces[Bottom], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Top] = RotateMatrixClockwise(matrix1);
                        Faces[Front] = RotateMatrixClockwise(matrix2);
                        Faces[Bottom] = RotateMatrixClockwise(matrix3);
                        Faces[Back] = RotateMatrixClockwise(matrix4);

                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                    }
                    else
                    {
                        Faces[Right] = RotateMatrixAnticlockwise(Faces[Right]);

                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);

                        var result = CombineMatricesVertically(Faces[Top], Faces[Front], Faces[Bottom], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Top] = RotateMatrixClockwise(matrix1);
                        Faces[Front] = RotateMatrixClockwise(matrix2);
                        Faces[Bottom] = RotateMatrixClockwise(matrix3);
                        Faces[Back] = RotateMatrixClockwise(matrix4);

                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);
                    }
                    break;
                case "Back":
                    if (clockwise)
                    {
                        Faces[Back] = RotateMatrixClockwise(Faces[Back]);

                        Faces[Left] = RotateMatrixClockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixAnticlockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Top], Faces[Right], Faces[Bottom]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Top] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Bottom] = matrix4;

                        Faces[Left] = RotateMatrixAnticlockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixClockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                    }
                    else
                    {
                        Faces[Back] = RotateMatrixAnticlockwise(Faces[Back]);

                        Faces[Left] = RotateMatrixClockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixAnticlockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Top], Faces[Right], Faces[Bottom]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 0, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Top] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Bottom] = matrix4;

                        Faces[Left] = RotateMatrixAnticlockwise(Faces[Left]);
                        Faces[Right] = RotateMatrixClockwise(Faces[Right]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);
                    }
                    break;
                case "Bottom":
                    if (clockwise)
                    {
                        Faces[Bottom] = RotateMatrixClockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Front], Faces[Right], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Right, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Front] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Back] = matrix4;
                    }
                    else
                    {
                        Faces[Bottom] = RotateMatrixAnticlockwise(Faces[Bottom]);

                        var result = CombineMatricesHorizontally(Faces[Left], Faces[Front], Faces[Right], Faces[Back]);
                        var result2 = ShiftMatrixRowOrColumnBy3(result, 2, ShiftDirection.Left, ShiftType.Row);

                        string[,] matrix1;
                        string[,] matrix2;
                        string[,] matrix3;
                        string[,] matrix4;

                        SplitMatrix(result2, out matrix1, out matrix2, out matrix3, out matrix4);

                        Faces[Left] = matrix1;
                        Faces[Front] = matrix2;
                        Faces[Right] = matrix3;
                        Faces[Back] = matrix4;
                    }
                    break;
                default: throw new ArgumentException();
            }
        }
    }
}

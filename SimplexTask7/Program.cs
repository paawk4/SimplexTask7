using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplexTask7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                start:
                Console.Clear();
                double[,] matrix = new double[3, 5];
                int[][] a = new int[3][];
                double[,] defaultMatrix = { { 2, 3, 1, 0, 4 }, { 1, 0, 0, 1, 3 }, { 2, 1, 0, 0, 0 } };
                Console.WriteLine("Введите матрицу 3х5\nВведите 1, если хотите ввести матрицу из примера\nВведите 2, если хотите ввести свою матрицу");
                string insert = Console.ReadLine();
                if (insert == "1")
                {
                    for (int i = 0; i < defaultMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < defaultMatrix.GetLength(1); j++)
                        {
                            matrix[i, j] = defaultMatrix[i, j];
                        }

                    }
                }
                else if (insert == "2")
                {
                    Console.WriteLine("Введите матрицу:");
                    Console.WriteLine("Xi\tX2\tX3\tX4\tb");
                    for (int i = 0; i < a.GetLength(0); i++)
                    {
                        a[i] = Console.ReadLine()
                              .Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)
                              .Select(int.Parse)
                              .ToArray();

                    }
                    for (int i = 0; i < defaultMatrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < defaultMatrix.GetLength(1); j++)
                        {
                            matrix[i, j] = a[i][j];
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Попробуйте еще раз\n");
                    goto start;
                }

                Console.WriteLine("\nXi\tX2\tX3\tX4\tb");
                for (int i = 0; i < matrix.GetLength(0) * matrix.GetLength(1); i++)
                {
                    int x = i / matrix.GetLength(1);
                    int y = i - matrix.GetLength(1) * x;
                    Console.Write((i % matrix.GetLength(1) == 0 && i > 0 ? "\n" : "") + matrix[x, y] + "\t");
                }
                Console.WriteLine("\n");
                double[] marks = new double[4];
                double[] freeEl = new double[2];
                int counter1 = 0;
                int counter2 = 0;
                double maxMark = -9999;
                int resolveColumn = 0;
                int resolveRow = 0;
                double minRatio = 9999;

                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (i == 2 && j < 4)
                        {
                            marks[counter1] = (double)matrix[i, j];
                            counter1++;
                        }
                        if ((i == 0 && j == 4) || (i == 1 && j == 4))
                        {
                            freeEl[counter2] = (double)matrix[i, j];
                            counter2++;
                        }
                    }

                for (int i = 0; i < marks.Length; i++)
                {
                    if (marks[i] > maxMark)
                    {
                        maxMark = marks[i];
                        resolveColumn = i + 1;
                    }
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (j == resolveColumn - 1 && i != 2)
                        {
                            if (matrix[i, j] == 0 || matrix[i, 4] < 0)
                                continue;
                            else if (matrix[i, 4] >= 0 && matrix[i, j] > 0)
                            {
                                minRatio = ((double)((double)matrix[i, 4] / matrix[i, j] < minRatio ? matrix[i, 4] / matrix[i, j] : minRatio));
                                resolveRow = i + 1;
                            }

                        }
                    }

                double resolveEl = (double)matrix[resolveRow - 1, resolveColumn - 1];
                Console.WriteLine($"Разрешающий столбец: {resolveColumn}");
                Console.WriteLine($"Разрешающая строка: {resolveRow}");
                Console.WriteLine($"Разрешающий элемент: {resolveEl}");
                int freeperem = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                        if (IsSingleColumn(matrix, i, j))
                        {
                            freeperem++;
                            Console.WriteLine($"x{j + 1} = {matrix[freeperem - 1, 4]}");
                        }
                        else if (j + 1 < 5)
                        {
                            Console.WriteLine($"x{j + 1} = 0");
                        }
                        else
                        {
                            break;
                        }
                    break;
                }

                Console.WriteLine($"Свободный элемент: {freeperem}");
                Console.WriteLine($"Кол-во базисных переменных: {matrix.GetLength(1) - freeperem - 1}");
                Console.ReadKey();
            }
        }

        public static bool IsSingleColumn(double[,] matrix, int i, int j)
        {
            try
            {
                if ((matrix[i, j] == 1 && matrix[i + 1, j] == 0 && matrix[i + 2, j] == 0) || (matrix[i, j] == 0 && matrix[i + 1, j] == 1 && matrix[i + 2, j] == 0) || (matrix[i, j] == 0 && matrix[i + 1, j] == 0 && matrix[i + 2, j] == 1))
                    return true;
                else
                    return false;
            }
            catch (IndexOutOfRangeException) { }
            return false;
        }

    }
}

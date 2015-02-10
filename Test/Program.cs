using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubiksCube;
using System.Threading;

namespace Test
{
    class Program
    {
        const int DIM = 3;
        const bool RESOLVE = true
            ;

        static void Main(string[] args)
        {
            Cube cube = new Cube();

            cube.Scramble(10);

            DrawFlatCube(cube);

            if (RESOLVE)
            {
                CubeSolver solver = new CubeSolver(cube);

                solver.Resolve();

                CubeSolution solution = solver.Solution;
                solution.Optimize();

                foreach (AlgorithmViewPair pair in solution)
                {
                    cube.SetView(pair.View);
                    DrawFlatCube(cube);
                    Wait();

                    foreach (Move move in pair.Algorithm)
                    {
                        cube.RotateFace(move);

                        Console.Clear();

                        DrawFlatCube(cube);

                        Wait();
                    }
                }
            }
            else
                while (true)
                {
                    Console.Write("Inserisci mossa: ");
                    Move m = (Move)Enum.Parse(typeof(Move), Console.ReadLine());
                    cube.RotateFace(m);

                    DrawFlatCube(cube);
                }

            Console.ReadLine();
        }

        private static void Wait()
        {
            Console.ReadLine();
        }

        private static ConsoleColor GetConsoleColor(RubiksColor? c)
        {
            switch (c)
            {
                case RubiksColor.Blue:
                    return ConsoleColor.Blue;
                case RubiksColor.Green:
                    return ConsoleColor.DarkGreen;
                case RubiksColor.Orange:
                    return ConsoleColor.Red;
                case RubiksColor.Red:
                    return ConsoleColor.DarkRed;
                case RubiksColor.White:
                    return ConsoleColor.White;
                case RubiksColor.Yellow:
                    return ConsoleColor.Yellow;
            }

            return ConsoleColor.Black;
        }

        private static void WriteColored(string m, ConsoleColor col)
        {
            Console.BackgroundColor = col;
            Console.Write(m);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void DrawFlatCube(Cube cube)
        {
            RubiksColor?[,] matrix = new RubiksColor?[DIM * 4, DIM * 3];

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                {
                    matrix[i + DIM, j + DIM] = cube.FindCubie(i, j, 0).FrontColor;
                    matrix[i + DIM * 3, j + DIM] = cube.FindCubie(DIM - i - 1, j, DIM - 1).BackColor;
                    matrix[i + DIM * 2, j + DIM] = cube.FindCubie(DIM - 1, j, i).RightColor;
                    matrix[i, j + DIM] = cube.FindCubie(0, j, DIM - i - 1).LeftColor;
                    matrix[i + DIM, j] = cube.FindCubie(i, 0, DIM - j - 1).UpColor;
                    matrix[i + DIM, j + DIM * 2] = cube.FindCubie(i, DIM - 1, j).DownColor;
                }

            Console.Clear();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    WriteColored("  ", GetConsoleColor(matrix[j, i]));
                    if (j % 3 == 2)
                        Console.Write("  ");
                    else
                        Console.Write(" ");
                }

                if (i % 3 == 2)
                    Console.WriteLine();

                Console.WriteLine("\n");
            }
        }

        private static void Test()
        {
            LastLayerConfiguration YellowL = new LastLayerConfiguration(new RubiksColor?[,]
            {
                { RubiksColor.Green,     RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
                { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
                { RubiksColor.Red,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.NonYellow, RubiksColor.Any    },
                { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
                { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
            });
            LastLayerConfiguration Yellow = new LastLayerConfiguration(new RubiksColor?[,]
            {
                { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
                { RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
                { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.NonYellow, RubiksColor.Any    },
                { RubiksColor.Red,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
                { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
            });

            Yellow.Matches(YellowL);
        }
    }
}

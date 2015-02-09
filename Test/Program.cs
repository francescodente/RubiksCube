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
        const bool RESOLVE = true;

        static void Main(string[] args)
        {
            Cube cube = new Cube();

            cube.Scramble(20);

            CubeSolver solver = new CubeSolver(cube);

            //cube.Scramble(20);
            //cube.SetView(RubiksColor.White, RubiksColor.Blue);

            DrawFlatCube(cube);

            solver.CrossPhase();
            solver.FirstLayer();


            CubeSolution solution = solver.Solution;

            if (RESOLVE)
                foreach (AlgorithmViewPair pair in solution)
                {
                    cube.SetView(pair.View);

                    foreach (Move move in pair.Algorithm)
                    {
                        cube.RotateFace(move);

                        Console.Clear();

                        DrawFlatCube(cube);

                        Console.ReadLine();
                        //Thread.Sleep(1000);
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

        private static ConsoleColor GetConsoleColor(RubiksColor? c)
        {
            switch (c)
            {
                case RubiksColor.Blue:
                    return ConsoleColor.Blue;
                case RubiksColor.Green:
                    return ConsoleColor.Green;
                case RubiksColor.Orange:
                    return ConsoleColor.DarkYellow;
                case RubiksColor.Red:
                    return ConsoleColor.Red;
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
                    matrix[i, j + DIM] = cube.FindCubie(i, j, 0).FrontColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i + DIM * 2, j + DIM] = cube.FindCubie(DIM - i - 1, j, DIM - 1).BackColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i, j + DIM] = cube.FindCubie(i, j, 0).FrontColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i + DIM, j + DIM] = cube.FindCubie(DIM - 1, j, i).RightColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i + DIM * 3, j + DIM] = cube.FindCubie(0, j, DIM - i - 1).LeftColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i, j] = cube.FindCubie(i, 0, DIM - j - 1).UpColor;

            for (int i = 0; i < DIM; i++)
                for (int j = 0; j < DIM; j++)
                    matrix[i, j + DIM * 2] = cube.FindCubie(i, DIM - 1, j).DownColor;

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

                if (i % 3
                    
                    == 2)
                    Console.WriteLine();

                Console.WriteLine("\n");
            }
        }
    }
}

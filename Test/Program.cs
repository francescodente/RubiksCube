using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubiksCube;

namespace Test
{
    class Program
    {
        const int DIM = 3;

        static void Main(string[] args)
        {
            Cube cube = new Cube();
            CubeSolver solver = new CubeSolver(cube);

            foreach (Move m in Algorithms.PLLCornerCounterClockwise)
                cube.RotateFace(m);

            //cube.RotateFace(Move.Front);
            while (true)
            {
                Console.Clear();

                for (int x = 0; x < DIM; x++)
                    for (int y = 0; y < DIM; y++)
                        for (int z = 0; z < DIM; z++)
                        {
                            Cubie c = cube.FindCubie(x, y, z);

                            Console.WriteLine("{0} {1} {2}", x, y, z);
                            Console.Write("Front ");
                            WriteColored(" ", GetConsoleColor(c.FrontColor));
                            Console.Write("Back  ");
                            WriteColored(" ", GetConsoleColor(c.BackColor));
                            Console.Write("Left  ");
                            WriteColored(" ", GetConsoleColor(c.LeftColor));
                            Console.Write("Right ");
                            WriteColored(" ", GetConsoleColor(c.RightColor));
                            Console.Write("Up    ");
                            WriteColored(" ", GetConsoleColor(c.UpColor));
                            Console.Write("Down  ");
                            WriteColored(" ", GetConsoleColor(c.DownColor));
                            Console.WriteLine(new String('-', 70));
                        }
                Console.ReadLine();
                //Console.Write("Inserisci mossa: ");
                //Move m = (Move)Enum.Parse(typeof(Move), Console.ReadLine());
                //cube.RotateFace(m);
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
            Console.WriteLine(m);
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}

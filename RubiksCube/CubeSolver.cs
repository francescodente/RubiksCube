using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class CubeSolver
    {
        const RubiksColor STARTING_COLOR = RubiksColor.White;
        const RubiksColor FINAL_COLOR = RubiksColor.Yellow;
        const int DIM = 3;

        public AlgorithmCollection Solution { get; private set; }
        
        private RubiksColor[] SIDE_COLORS = { RubiksColor.Blue, RubiksColor.Orange, RubiksColor.Green, RubiksColor.Red };

        private CubeView _currentView;
        private List<Move> _currentAlgorithm;
        private Cube _cube;

        public CubeSolver(Cube cube)
        {
            _cube = cube.Clone();
            _currentView = new CubeView(_cube.GetFaceColor(Face.Front), _cube.GetFaceColor(Face.Up));
            _currentAlgorithm = new List<Move>();
        }

        public AlgorithmCollection Resolve()
        {
            Solution = new AlgorithmCollection();

            CrossPhase();
            FirstLayer();
            SecondLayer();
            OLL();
            PLL();

            if (_currentAlgorithm.Count > 0)
                Solution.Add(new AlgorithmViewPair(_currentAlgorithm, _currentView));

            return Solution;
        }

        private void PLL()
        {
			// Corners.
            ChangeView(new CubeView(SIDE_COLORS[0], FINAL_COLOR));

            IEnumerable<Cubie> corners = _cube.GetFaceCubies(Face.Up).Corners();
			int correctCornersCount = corners.CorrectCubies(_cube).Count<Cubie>();

            for (int i = 0; i < 4 && correctCornersCount != 4 && correctCornersCount != 1; i++)
			{
			    AddMove(Move.Up);
                correctCornersCount = corners.CorrectCubies(_cube).Count<Cubie>();
			}

            if (correctCornersCount != 4)
            {
                if (correctCornersCount != 1)
                {
                    AddMoveList(Algorithms.PLLCornerDoubleExchange);

                    while (!_cube.IsCubiePlacedCorrectly(_cube.FindCubie(0, 0, 0)))
                        AddMove(Move.Up);
                }
                else
                {
                    for (int c = 0; !_cube.IsCubiePlacedCorrectly(_cube.FindCubie(0, 0, 0)); c++)
                        ChangeView(new CubeView(SIDE_COLORS[c], FINAL_COLOR));

                    if (_cube.FindCubie(2, 0, 0).FrontColor == _cube.FindCubie(2, 1, 1).RightColor)
                        AddMoveList(Algorithms.PLLCornerCounterClockwise);
                    else
                        AddMoveList(Algorithms.PLLCornerClockwise);
                }
            }
			
			// Edges.
            for (int c = 0; c < SIDE_COLORS.Length && !_cube.IsSolved(); c++)
            {
                ChangeView(new CubeView(SIDE_COLORS[c], FINAL_COLOR));

                Cubie frontEdge = _cube.FindCubie(1, 0, 0);
                Cubie backEdge = _cube.FindCubie(1, 0, 2);
                Cubie leftEdge = _cube.FindCubie(0, 0, 1);

                if (_cube.IsCubiePlacedCorrectly(frontEdge))
                {
                    if ((int)leftEdge.LeftColor == -(int)frontEdge.FrontColor)
                        AddMoveList(Algorithms.PLLEdgeClockwise);
                    else
                        AddMoveList(Algorithms.PLLEdgeCounterClockwise);
                }
                else if ((int)frontEdge.FrontColor == -(int)_cube.GetFaceColor(Face.Front) && (int)backEdge.BackColor == -(int)_cube.GetFaceColor(Face.Back))
                    AddMoveList(Algorithms.PLLEdgeCrossExchange);
                else if (leftEdge.LeftColor == _cube.GetFaceColor(Face.Front) && backEdge.BackColor == _cube.GetFaceColor(Face.Right))
                    AddMoveList(Algorithms.PLLEdgeBackslashExchange);
            }
        }

        private void OLL()
        {
            YellowCrossCase();

            LastLayerConfiguration conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

            for (int c = 0; !conf.Matches(OllCases.OLLComplete); c++)
            {
                ChangeView(new CubeView(SIDE_COLORS[c], FINAL_COLOR));

                conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

                if (conf.Matches(OllCases.OLLSituation1))
                    AddMoveList(Algorithms.OLLSituation1);
                else if (conf.Matches(OllCases.OLLSituation2))
                    AddMoveList(Algorithms.OLLSituation2);
                else if (conf.Matches(OllCases.OLLSituation3))
                    AddMoveList(Algorithms.OLLSituation3);
                else if (conf.Matches(OllCases.OLLSituation4))
                    AddMoveList(Algorithms.OLLSituation4);
                else if (conf.Matches(OllCases.OLLSituation5))
                    AddMoveList(Algorithms.OLLSituation5);
                else if (conf.Matches(OllCases.OLLSituation6))
                    AddMoveList(Algorithms.OLLSituation6);
                else if (conf.Matches(OllCases.OLLSituation7))
                    AddMoveList(Algorithms.OLLSituation7);

                conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));
            }
        }

        private void SecondLayer()
        {
            for (int c = 0; c < SIDE_COLORS.Length; c++)
            {
                // Gets the edge that needs to be moved.
                Cubie edge = _cube.FindEdge(SIDE_COLORS[c], SIDE_COLORS[(c + 1) % 4]);
                if (!_cube.IsCubiePlacedCorrectly(edge))
                {
                    ChangeView(new CubeView(SIDE_COLORS[c], FINAL_COLOR));

                    // If the edge is in the middle layer and it is located in the back face,
                    // the view will be set to the back face. Then the program will use the algorithm to switch
                    // the edges.
                    if (edge.Y == 1)
                    {
                        if (edge.Z == 2)
                            ChangeView(new CubeView(SIDE_COLORS[(c + 2) % 4], FINAL_COLOR));

                        if (edge.X == 0)
                            AddMoveList(Algorithms.SecondLayerLeft);
                        else
                            AddMoveList(Algorithms.SecondLayerRight);
                    }

                    // Sets the view.
                    if (edge.UpColor == SIDE_COLORS[c])
                        ChangeView(new CubeView(SIDE_COLORS[(c + 1) % 4], FINAL_COLOR));
                    else
                        ChangeView(new CubeView(SIDE_COLORS[c], FINAL_COLOR));

                    // Rotates the upper layer until the edge is in the front face.
                    while (edge.Z != 0)
                        AddMove(Move.Up);

                    // Use the algorithm to place the edge in the correct position.
                    if (_cube.FindCenter((RubiksColor)edge.UpColor).LeftColor == edge.UpColor)
                        AddMoveList(Algorithms.SecondLayerLeft);
                    else
                        AddMoveList(Algorithms.SecondLayerRight);
                }
            }
        }

        private void FirstLayer()
        {
            for (int c = 0; c < SIDE_COLORS.Length; c++)
            {
                // Sets the view with the current color in the front face and the white on the top.
                ChangeView(new CubeView(SIDE_COLORS[c], STARTING_COLOR));

                // Gets the corner that needs to be moved.
                Cubie corner = _cube.FindCorner(STARTING_COLOR, SIDE_COLORS[c], SIDE_COLORS[(c + 1) % 4]);

                if (!_cube.IsCubiePlacedCorrectly(corner))
                {
                    // If the corner is in the top layer, the program will rotate it until the corner is on
                    // the top-right of the front face, then use the separate algorithm and rotate the layer back.
                    if (corner.Y == 0)
                    {
                        int rotations;
                        for (rotations = 0; corner.Z != 0 || corner.X != 2; rotations++)
                            AddMove(Move.Up);

                        if (corner.FrontColor == STARTING_COLOR || corner.UpColor == STARTING_COLOR)
                            AddMoveList(Algorithms.SeparateCorner1);
                        else if (corner.RightColor == STARTING_COLOR)
                            AddMoveList(Algorithms.SeparateCorner2);

                        for (int i = 0; i < rotations; i++)
                            AddMove(Move.UpPrime);
                    }

                    // Rotates the lower layer until the corner is in the bottom-right of the front face.
                    while (corner.X != 2 || corner.Z != 0)
                        AddMove(Move.Down);

                    // Uses the correct algorithm to place the corner in its position.
                    if (corner.FrontColor == STARTING_COLOR)
                        AddMoveList(Algorithms.WhiteFront);
                    else if (corner.DownColor == STARTING_COLOR)
                        AddMoveList(Algorithms.WhiteDown);
                    else
                        AddMoveList(Algorithms.WhiteRight); 
                }
            }
        }

        private void CrossPhase()
        {
            foreach (RubiksColor col in SIDE_COLORS)
            {
                // Sets the view with the current color in the front face and the white on the top.
                ChangeView(new CubeView(col, STARTING_COLOR));

                // Gets the edge that needs to be moved.
                Cubie edge = _cube.FindEdge(col, STARTING_COLOR);

                if (!_cube.IsCubiePlacedCorrectly(edge))
                {
                    // If the edge is in the lower horizontal layer, the program will
                    // rotate the lower layer until it reaches the front face.
                    if (edge.Y == 2)
                        while (edge.Z != 0)
                            AddMove(Move.Down);

                    // If the edge is in the upper horizonal layer, the program will
                    // rotate the upper layer until it reaches the front face, protect the edge with a front move
                    // and in the end rotate the upper layer back to its original position.
                    else if (edge.Y == 0)
                    {
                        int rotations;
                        for (rotations = 0; edge.Z != 0; rotations++)
                            AddMove(Move.Up);

                        AddMove(Move.Front);

                        for (int i = 0; i < rotations; i++)
                            AddMove(Move.UpPrime);
                    }

                    // If the edge is in the middle horizontal layer, but not in the front face the program will
                    // rotate the left/right face until the edge reaches the front face,
                    // protect it with a front/front prime move and in the end move the left/right
                    // face back.
                    else if (edge.Z != 0)
                    {
                        if (edge.X == 0)
                            AddMoveList(new Move[] { Move.Left, Move.Left, Move.Front, Move.Left, Move.Left });
                        else
                            AddMoveList(new Move[] { Move.Right, Move.Right, Move.FrontPrime, Move.Right, Move.Right });
                    }

                    // Now we are sure that the edge is in the front face.
                    // Two different cases may occur:

                    // 1. the white face of the edge is on the front face of the cube.
                    //    the program will first rotate the front face to bring the edge on the right or
                    //    on the left (if needed), then use the algorithm to place the edge on the top.
                    if (edge.FrontColor == STARTING_COLOR)
                    {
                        if (edge.X == 1)
                            AddMove(Move.Front);

                        if (edge.X == 0)
                            AddMoveList(Algorithms.CrossEdgeLeftWhiteFront);
                        else
                            AddMoveList(Algorithms.CrossEdgeRightWhiteFront);
                    }
                    // 2. the white face of the edge is on one of the sides.
                    //    the program will rotate the front face until the edge is on the top.
                    else
                    {
                        while (edge.Y != 0)
                            AddMove(Move.Front);
                    } 
                }
            }
        }

        private void ChangeView(CubeView view)
        {
            if (_currentView.FrontColor != view.FrontColor || _currentView.UpColor != view.UpColor)
            {
                if (_currentAlgorithm.Count > 0)
                {
                    Solution.Add(new AlgorithmViewPair(_currentAlgorithm, _currentView));
                    _currentAlgorithm = new List<Move>();
                }
                _currentView = view;
                _cube.SetView(view);
            }
        }

        private void AddMove(Move m)
        {
            _currentAlgorithm.Add(m);
            _cube.RotateFace(m);
        }

        private void AddMoveList(IEnumerable<Move> moveList)
        {
            foreach (Move m in moveList)
                AddMove(m);
        }

        private void YellowCrossCase()
        {
            foreach (RubiksColor col in SIDE_COLORS)
            {
                ChangeView(new CubeView(col, FINAL_COLOR));

                LastLayerConfiguration config = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

                if (config.Matches(OllCases.YellowCenter))
                {
                    AddMoveList(Algorithms.OLLCross1);
                    YellowCrossCase();
                }
                else if (config.Matches(OllCases.YellowL))
                    AddMoveList(Algorithms.OLLCross2);
                else if (config.Matches(OllCases.YellowLine))
                    AddMoveList(Algorithms.OLLCross1);
            }
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

                if (i % 3

                    == 2)
                    Console.WriteLine();

                Console.WriteLine("\n");
            }
        }
    }
}

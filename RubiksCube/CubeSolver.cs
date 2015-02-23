using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents an object able to resolve a 3x3x3 rubik's cube.
    /// </summary>
    public class CubeSolver
    {
        // The colors from which the solution may start and finish.
        const RubiksColor STARTING_COLOR = RubiksColor.White;
        const RubiksColor FINAL_COLOR = (RubiksColor)(-(int)STARTING_COLOR);
        const int DIM = 3;

        /// <summary>
        /// Gets the solution of the cube.
        /// </summary>
        public AlgorithmCollection Solution { get; private set; }
        
        // The array that contains the side colors.
        private RubiksColor[] SideColors = { RubiksColor.Blue, RubiksColor.Orange, RubiksColor.Green, RubiksColor.Red };

        private CubeView _currentView;          // The view on which the program is working on at the moment.
        private List<Move> _currentAlgorithm;   // The list of moves on which the program is working on at the moment.
        private Cube _cube;                     // The cube the program is trying to resolve.

        /// <summary>
        /// Creates a new instance of the <see cref="FCubeSolver"/> class.
        /// </summary>
        /// <param name="cube">The cube.</param>
        public CubeSolver(Cube cube)
        {
            _cube = cube.Clone();
            _currentView = new CubeView(_cube.GetFaceColor(Face.Front), _cube.GetFaceColor(Face.Up));
            _currentAlgorithm = new List<Move>();
        }

        /// <summary>
        /// Returns the algorithms that solves the cube.
        /// </summary>
        /// <returns>The solution.</returns>
        public AlgorithmCollection Solve()
        {
            Solution = new AlgorithmCollection();

            /* The solution is composed by five steps. 
             * 
             * 
             * 
             * 1. Cross Phase:
             * Consists of building an oriented white (or any other color) cross on the top layer, which means
             * all of the white edges must be placed correctly.
             * 
             * 
             * 2. First Layer:
             * After the white cross, all the white corners must be put in their places, without scrambling
             * what has been done before. With this done, the first layer is completed.
             * 
             * 
             * 3. Second Layer:
             * All of the side edges must be put in the right place and this is what this step does.
             * 
             * 
             * 4. OLL (Orientation Last Layer):
             * Consists of orienting all of the cubies in the last layer with the final color in the upper face.
             * 
             * 
             * 5. PLL (Permutation Last Layer):
             * Consists of ordering all the cubies of the last layer to have each one of them placed correctly.
             * 
             */
            CrossPhase();
            FirstLayer();
            SecondLayer();
            OLL();
            PLL();

            if (_currentAlgorithm.Count > 0)
                Solution.Add(new AlgorithmViewPair(_currentAlgorithm, _currentView));

            return Solution;
        }

        /// <summary>
        /// Executes the PLL phase.
        /// </summary>
        private void PLL()
        {
            // ******************* CORNERS *******************

            // Sets the view.
            ChangeView(new CubeView(SideColors[0], FINAL_COLOR));

            // Gets the last layer corners and the number of how many of them is already correct.
            IEnumerable<Cubie> corners = _cube.GetFaceCubies(Face.Up).Corners();
			int correctCornersCount = corners.CorrectCubies(_cube).Count<Cubie>();

            // Rotates the last layer until only one or all of the corners are correct (max 4 times).
            for (int i = 0; i < 4 && correctCornersCount != 4 && correctCornersCount != 1; i++)
			{
			    AddMove(Move.Up);
                correctCornersCount = corners.CorrectCubies(_cube).Count<Cubie>();
			}

            // Continues with the algorithm only if any of the corners isn't already correct.
            if (correctCornersCount != 4)
            {
                // If more or less than one corner is correct, the program will use the algorithm to
                // exchange the corners and then rotate the upper layer until the corners are correct.
                if (correctCornersCount != 1)
                {
                    AddMoveList(Algorithms.PLLCornerDoubleExchange);

                    while (!_cube.IsCubiePlacedCorrectly(_cube.FindCubie(0, 0, 0)))
                        AddMove(Move.Up);
                }

                // If one corner is correct the cube will be rotated until it is on the left in the front face,
                // then use the correct algorithm.
                else
                {
                    for (int c = 0; !_cube.IsCubiePlacedCorrectly(_cube.FindCubie(0, 0, 0)); c++)
                        ChangeView(new CubeView(SideColors[c], FINAL_COLOR));

                    if (_cube.FindCubie(2, 0, 0).FrontColor == _cube.FindCubie(2, 1, 1).RightColor)
                        AddMoveList(Algorithms.PLLCornerCounterClockwise);
                    else
                        AddMoveList(Algorithms.PLLCornerClockwise);
                }
            }

            // ******************* EDGES *******************

            // Keeps on turning the cube until the cube is solved.
            for (int c = 0; c < SideColors.Length && !_cube.IsSolved(); c++)
            {
                ChangeView(new CubeView(SideColors[c], FINAL_COLOR));

                Cubie frontEdge = _cube.FindCubie(1, 0, 0);
                Cubie backEdge = _cube.FindCubie(1, 0, 2);
                Cubie leftEdge = _cube.FindCubie(0, 0, 1);

                // Depending on the situation, the program will use a different algorithm.
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

        /// <summary>
        /// Executes the OLL phase.
        /// </summary>
        private void OLL()
        {
            // The first thing to do is to build a yellow cross.
            BuildYellowCross();

            // Gets the configuration of the last layer.
            LastLayerConfiguration conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

            // Keeps on changing the view until OLL is complete.
            for (int c = 0; !conf.Matches(OllCases.OLLComplete); c++)
            {
                // Sets the view.
                ChangeView(new CubeView(SideColors[c], FINAL_COLOR));

                // Gets the configuration before the algorithms.
                conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

                // If the configuration matches any of the situations, the proper algorithm will be used.
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

                // Gets the configuration after the algorithms.
                conf = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));
            }
        }

        private void SecondLayer()
        {
            for (int c = 0; c < SideColors.Length; c++)
            {
                // Gets the edge that needs to be moved.
                Cubie edge = _cube.FindEdge(SideColors[c], SideColors[(c + 1) % 4]);
                if (!_cube.IsCubiePlacedCorrectly(edge))
                {
                    ChangeView(new CubeView(SideColors[c], FINAL_COLOR));

                    // If the edge is in the middle layer and it is located in the back face,
                    // the view will be set to the back face. Then the program will use the algorithm to switch
                    // the edges.
                    if (edge.Y == 1)
                    {
                        if (edge.Z == 2)
                            ChangeView(new CubeView(SideColors[(c + 2) % 4], FINAL_COLOR));

                        if (edge.X == 0)
                            AddMoveList(Algorithms.SecondLayerLeft);
                        else
                            AddMoveList(Algorithms.SecondLayerRight);
                    }

                    // Sets the view.
                    if (edge.UpColor == SideColors[c])
                        ChangeView(new CubeView(SideColors[(c + 1) % 4], FINAL_COLOR));
                    else
                        ChangeView(new CubeView(SideColors[c], FINAL_COLOR));

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
            for (int c = 0; c < SideColors.Length; c++)
            {
                // Sets the view with the current color in the front face and the white on the top.
                ChangeView(new CubeView(SideColors[c], STARTING_COLOR));

                // Gets the corner that needs to be moved.
                Cubie corner = _cube.FindCorner(STARTING_COLOR, SideColors[c], SideColors[(c + 1) % 4]);

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
            foreach (RubiksColor col in SideColors)
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

        /// <summary>
        /// Changes the current view and adds the current algorithm to the solution.
        /// </summary>
        /// <param name="view">New view</param>
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

        /// <summary>
        /// Adds a move to the current algorithm.
        /// </summary>
        /// <param name="move">The move.</param>
        private void AddMove(Move move)
        {
            _currentAlgorithm.Add(move);
            _cube.ExecuteMove(move);
        }

        /// <summary>
        /// Adds a list of moves to the current algorithm.
        /// </summary>
        /// <param name="moveList">The list</param>
        private void AddMoveList(IEnumerable<Move> moveList)
        {
            foreach (Move m in moveList)
                AddMove(m);
        }

        /// <summary>
        /// Builds the yellow cross for OLL.
        /// </summary>
        private void BuildYellowCross()
        {
            foreach (RubiksColor col in SideColors)
            {
                // Sets the view.
                ChangeView(new CubeView(col, FINAL_COLOR));

                // Gets the configuration.
                LastLayerConfiguration config = new LastLayerConfiguration(_cube.GetFaceCubies(Face.Up));

                // Uses the correct algorithm if any match is found.
                if (config.Matches(OllCases.YellowCenter))
                {
                    // If the configuration matches the yellow center case, another algorithm will be needed.
                    AddMoveList(Algorithms.OLLCross1);
                    BuildYellowCross();
                }
                else if (config.Matches(OllCases.YellowL))
                    AddMoveList(Algorithms.OLLCross2);
                else if (config.Matches(OllCases.YellowLine))
                    AddMoveList(Algorithms.OLLCross1);
            }
        }
    }
}

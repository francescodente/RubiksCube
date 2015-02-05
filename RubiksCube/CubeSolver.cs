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
        const int DIMENSION = 3;

        public CubeSolution Solution { get; private set; }
        
        private RubiksColor[] SIDE_COLORS = { RubiksColor.Blue, RubiksColor.Orange, RubiksColor.Green, RubiksColor.Red };

        private CubeView _currentView;
        private List<Move> _currentAlgorithm;
        private Cube _cube;

        public CubeSolver(Cube cube)
        {
            _cube = cube.Clone();
            Solution = new CubeSolution();
            _currentView = new CubeView(RubiksColor.Blue, RubiksColor.White);
            _currentAlgorithm = new List<Move>();
        }

        public void Resolve()
        {
            CrossPhase();
            FirstLayer();
            SecondLayer();
            OLL();
            PLL();
        }

        private void PLL()
        {
            throw new NotImplementedException();
        }

        private void OLL()
        {
            YellowCrossCase();
        }

        private void SecondLayer()
        {
            for (int c = 0; c < SIDE_COLORS.Length; c++)
            {
                // Gets the edge that needs to be moved.
                Cubie edge = _cube.FindEdge(SIDE_COLORS[c], SIDE_COLORS[c + 1]);

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
                    ChangeView(new CubeView(SIDE_COLORS[c + 1], FINAL_COLOR));
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

        private void FirstLayer()
        {
            for (int c = 0; c < SIDE_COLORS.Length; c++)
            {
                // Sets the view with the current color in the front face and the white on the top.
                ChangeView(new CubeView(SIDE_COLORS[c], STARTING_COLOR));

                // Gets the corner that needs to be moved.
                Cubie corner = _cube.FindCorner(STARTING_COLOR, SIDE_COLORS[c], SIDE_COLORS[c + 1]);

                // If the corner is in the top layer, the program will rotate it until the corner is on
                // the top-right of the front face, then use the separate algorithm and rotate the layer back.
                if (corner.Y == 0)
                {
                    int rotations;
                    for (rotations = 0; corner.Z != 0 && corner.X != 2; rotations++)
                        AddMove(Move.Up);

                    if (corner.FrontColor == STARTING_COLOR || corner.UpColor == STARTING_COLOR)
                        AddMoveList(Algorithms.SeparateCorner1);
                    else if (corner.RightColor == STARTING_COLOR)
                        AddMoveList(Algorithms.SeparateCorner2);

                    for (int i = 0; i < rotations; i++)
                        AddMove(Move.UpPrime);
                }

                // Rotates the lower layer until the corner is in the bottom-right of the front face.
                while (corner.X != 2 && corner.Z != 0)
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

        private void CrossPhase()
        {
            foreach (RubiksColor col in SIDE_COLORS)
            {
                // Sets the view with the current color in the front face and the white on the top.
                ChangeView(new CubeView(col, STARTING_COLOR));

                // Gets the edge that needs to be moved.
                Cubie edge = _cube.FindEdge(col, STARTING_COLOR);

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

                // If the edge is in the middle horizontal layer, the program will
                // rotate the left/right face until the edge reaches the front face,
                // protect it with a front/front prime move and in the end move the left/right
                // face back.
                else
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

        private void ChangeView(CubeView view)
        {
            if (_currentView.FrontColor != view.FrontColor && _currentView.UpColor != view.UpColor && _currentAlgorithm.Count > 0)
            {
                Solution.Add(new AlgorithmViewPair(_currentAlgorithm, _currentView));
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
            _currentAlgorithm.AddRange(moveList);
            Algorithms.ExecuteAlgorithm(_cube, moveList);
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
                else
                    AddMoveList(Algorithms.OLLCross1);
            }
        }
    }
}

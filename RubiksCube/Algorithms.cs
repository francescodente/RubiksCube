using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class Algorithms
    {
        public static Move[] CrossEdgeRightWhiteFront = 
        {
            Move.UpPrime,
            Move.Right,
            Move.Up
        };

        public static Move[] CrossEdgeLeftWhiteFront = 
        {
            Move.Up,
            Move.LeftPrime,
            Move.UpPrime
        };

        public static Move[] SeparateCorner1 =
        {
            Move.Front,
            Move.Down,
            Move.FrontPrime,
            Move.DownPrime
        };

        public static Move[] SeparateCorner2 =
        {
            Move.RightPrime,
            Move.DownPrime,
            Move.Right,
            Move.Down
        };

        public static Move[] WhiteFront =
        {
            Move.Front,
            Move.Down,
            Move.FrontPrime
        };

        public static Move[] WhiteRight =
        {
            Move.RightPrime,
            Move.DownPrime,
            Move.Right
        };

        public static Move[] WhiteDown =
        {
            Move.RightPrime,
            Move.Down,
            Move.Down,
            Move.Right,
            Move.Down,
            Move.RightPrime,
            Move.DownPrime,
            Move.Right
        };

        public static Move[] SecondLayerRight =
        {
            Move.Up,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.UpPrime,
            Move.FrontPrime,
            Move.Up,
            Move.Front
        };

        public static Move[] SecondLayerLeft =
        {
            Move.UpPrime,
            Move.LeftPrime,
            Move.Up,
            Move.Left,
            Move.Up,
            Move.Front,
            Move.UpPrime,
            Move.FrontPrime
        };

        public static Move[] OLLCross1 = 
        {
            Move.Front,
            Move.Right,
            Move.Up,
            Move.RightPrime,
            Move.UpPrime,
            Move.FrontPrime
        };

        public static Move[] OLLCross2 = 
        {
            Move.Front,
            Move.Up,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.FrontPrime
        };

        public static Move[] OLLSituation1 = 
        {
            Move.RightPrime,
            Move.UpPrime,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.Up,
            Move.Up,
            Move.Right
        };

        public static Move[] OLLSituation2 = 
        {
            Move.Right,
            Move.Up,
            Move.RightPrime,
            Move.Up,
            Move.Right,
            Move.Up,
            Move.Up,
            Move.RightPrime
        };

        public static Move[] OLLSituation3 = 
        {
            Move.Right,
            Move.Right,
            Move.DownPrime,
            Move.Right,
            Move.Up,
            Move.Up,
            Move.RightPrime,
            Move.Down,
            Move.Right,
            Move.Up,
            Move.Up,
            Move.Right
        };

        public static Move[] OLLSituation4 = 
        {
            Move.RightPrime,
            Move.FrontPrime,
            Move.Left,
            Move.Front,
            Move.Right,
            Move.FrontPrime,
            Move.LeftPrime,
            Move.Front
        };

        public static Move[] OLLSituation5 = 
        {
            Move.RightPrime,
            Move.FrontPrime,
            Move.LeftPrime,
            Move.Front,
            Move.Right,
            Move.FrontPrime,
            Move.Left,
            Move.Front
        };

        public static Move[] OLLSituation6 = 
        {
            Move.Right,
            Move.UpPrime,
            Move.UpPrime,
            Move.RightPrime,
            Move.RightPrime,
            Move.UpPrime,
            Move.Right,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.RightPrime,
            Move.Up,
            Move.Up,
            Move.Right
        };

        public static Move[] OLLSituation7 = 
        {
            Move.Right,
            Move.Up,
            Move.RightPrime,
            Move.Up,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.Up,
            Move.Right,
            Move.Up,
            Move.Up,
            Move.RightPrime
        };

        public static Move[] PLLCornerClockwise = 
        {
            Move.RightPrime,
            Move.Front,
            Move.RightPrime,
            Move.Back,
            Move.Back,
            Move.Right,
            Move.FrontPrime,
            Move.RightPrime,
            Move.Back,
            Move.Back,
            Move.Right,
            Move.Right
        };

        public static Move[] PLLCornerCounterClockwise = 
        {
            Move.Right,
            Move.Right,
            Move.Back,
            Move.Back,
            Move.Right,
            Move.Front,
            Move.RightPrime,
            Move.Back,
            Move.Back,
            Move.Right,
            Move.FrontPrime,
            Move.Right
        };

        public static Move[] PLLCornerDoubleExchange =
        {
            Move.Front,
            Move.RightPrime,
            Move.FrontPrime,
            Move.Left,
            Move.Front,
            Move.Right,
            Move.FrontPrime,
            Move.Left,
            Move.Left,
            Move.BackPrime,
            Move.Right,
            Move.Back,
            Move.Left,
            Move.BackPrime,
            Move.RightPrime,
            Move.Back
        };

        public static Move[] PLLEdgeClockwise = 
        {
            Move.LeftPrime,
            Move.LeftPrime,
            Move.Up,
            Move.Left,
            Move.Up,
            Move.LeftPrime,
            Move.UpPrime,
            Move.LeftPrime,
            Move.UpPrime,
            Move.LeftPrime,
            Move.Up,
            Move.LeftPrime
        };

        public static Move[] PLLEdgeCounterClockwise = 
        {
            Move.Right,
            Move.Right,
            Move.UpPrime,
            Move.RightPrime,
            Move.UpPrime,
            Move.Right,
            Move.Up,
            Move.Right,
            Move.Up,
            Move.Right,
            Move.UpPrime,
            Move.Right
        };

        public static Move[] PLLEdgeCrossExchange = 
        {
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
            Move.Down,
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
            Move.Up,
            Move.Up,
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
            Move.Down,
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
        };

        public static Move[] PLLEdgeBackslashExchange = 
        {
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
            Move.Down,
            Move.LeftPrime,
            Move.Right,
            Move.Back,
            Move.Back,
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
            Move.Front,
            Move.Front,
            Move.LeftPrime,
            Move.Right,
            Move.DownPrime,
            Move.Left,
            Move.Left,
            Move.Right,
            Move.Right,
        };

        public static void ExecuteAlgorithm(Cube cube, IEnumerable<Move> algo)
        {
            foreach (Move m in algo)
                cube.RotateFace(m);
        }
    }
}

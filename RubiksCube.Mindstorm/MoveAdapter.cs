using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RubiksCube;

namespace RubiksCube.Mindstorm
{
    public class MoveAdapter
    {
        private int _generalmove;

        public MoveAdapter(Move generalMove)
        {
            _generalmove = Math.Abs((int)generalMove);
        }

        public AlgorithmCollection AdaptSolution(AlgorithmCollection sol)
        {
            AlgorithmCollection newSolution = new AlgorithmCollection();

            foreach (AlgorithmViewPair pair in sol)
                foreach (Move move in pair.Algorithm)
                {
                    if (Math.Abs((int)move) == _generalmove)
                        newSolution.Add(new AlgorithmViewPair(new Move[] { move }, pair.View));
                }

            return null;
        }
    }
}

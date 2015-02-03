using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class AlgorithmViewPair
    {
        public List<Move> Algorithm { get; private set; }
        public CubeView View { get; private set; }

        public AlgorithmViewPair(Move[] algorythm, CubeView view) : this(new List<Move>(algorythm), view)
        {

        }

        public AlgorithmViewPair(List<Move> algorithm, CubeView view)
        {
            Algorithm = algorithm;
            View = view;
        }

        public void Execute(Cube cube)
        {
            cube.SetView(View);

            foreach (Move m in Algorithm)
            {
                cube.RotateFace(m);
            }
        }
    }
}

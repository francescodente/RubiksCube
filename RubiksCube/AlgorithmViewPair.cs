using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents an algorithm that must be executed with a particular view.
    /// </summary>
    public class AlgorithmViewPair
    {
        /// <summary>
        /// Gets the algorithm.
        /// </summary>
        public List<Move> Algorithm { get; private set; }

        /// <summary>
        /// Gets the view.
        /// </summary>
        public CubeView View { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlgorithmViewPair"/> class.
        /// </summary>
        /// <param name="algorithm">The algorithm.</param>
        /// <param name="view">The view.</param>
        public AlgorithmViewPair(IEnumerable<Move> algorithm, CubeView view)
        {
            Algorithm = new List<Move>(algorithm);
            View = view;
        }

        /// <summary>
        /// Executes the algorithm on the specified cube.
        /// </summary>
        /// <param name="cube"></param>
        public void Execute(Cube cube)
        {
            cube.SetView(View);

            foreach (Move m in Algorithm)
            {
                cube.ExecuteMove(m);
            }
        }
    }
}

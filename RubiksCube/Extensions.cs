using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class Extensions
    {
        /// <summary>
        /// Gets all of the corners contained in a list of cubies.
        /// </summary>
        /// <param name="cubies">The list.</param>
        /// <returns>The corners.</returns>
        public static IEnumerable<Cubie> Corners(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Corner)
                    yield return c;
        }

        /// <summary>
        /// Gets all of the edges contained in a list of cubies.
        /// </summary>
        /// <param name="cubies">The list.</param>
        /// <returns>The edges.</returns>
        public static IEnumerable<Cubie> Edges(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Edge)
                    yield return c;
        }

        /// <summary>
        /// Gets all of the centers contained in a list of cubies.
        /// </summary>
        /// <param name="cubies">The list.</param>
        /// <returns>The centers.</returns>
        public static IEnumerable<Cubie> Centers(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Center)
                    yield return c;
        }

        /// <summary>
        /// Gets all the correct cubies in a list of cubies.
        /// </summary>
        /// <param name="cubies">The list.</param>
        /// <param name="cube">The cube.</param>
        /// <returns>The correct cubies.</returns>
        public static IEnumerable<Cubie> CorrectCubies(this IEnumerable<Cubie> cubies, Cube cube)
        {
            foreach (Cubie c in cubies)
                if (cube.IsCubiePlacedCorrectly(c))
                    yield return c;
        }
    }
}

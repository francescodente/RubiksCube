using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class Extensions
    {
        public static IEnumerable<Cubie> Corners(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Corner)
                    yield return c;
        }

        public static IEnumerable<Cubie> Edges(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Edge)
                    yield return c;
        }

        public static IEnumerable<Cubie> Centers(this IEnumerable<Cubie> cubies)
        {
            foreach (Cubie c in cubies)
                if (c.CubieType == CubieType.Center)
                    yield return c;
        }

        public static IEnumerable<Cubie> CorrectCubies(this IEnumerable<Cubie> cubies, Cube cube)
        {
            foreach (Cubie c in cubies)
                if (cube.IsCubiePlacedCorrectly(c))
                    yield return c;
        }
    }
}

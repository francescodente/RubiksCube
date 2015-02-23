using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents a 3x3x3 rubik's cube.
    /// </summary>
    public class Cube : IEnumerable<Cubie>
    {
        private const int DIMENSION = 3;

        private List<Cubie> _cubies;

        /// <summary>
        /// A 5x5x5 matrix used to initialize the colors of the cubies.
        /// </summary>
        internal RubiksColor?[, ,] Solved { get; set; }

        /// <summary>
        /// Gets all the centers of the cube.
        /// </summary>
        public IEnumerable<Cubie> Centers
        {
            get
            {
                return _cubies.Centers();
            }
        }

        /// <summary>
        /// Gets all the edges of the cube.
        /// </summary>
        public IEnumerable<Cubie> Edges
        {
            get
            {
                return _cubies.Edges();
            }
        }

        /// <summary>
        /// Gets all the corners of the cube.
        /// </summary>
        public IEnumerable<Cubie> Corners
        {
            get
            {
                return _cubies.Corners();
            }
        }

        /// <summary>
        /// Initializes a 
        /// </summary>
        public Cube()
        {
            _cubies = new List<Cubie>();

            Solved = new RubiksColor?[DIMENSION + 2, DIMENSION + 2, DIMENSION + 2];

            /* The matrix is first set to have the colors represented in the picture.
             * 
             *           Green                     
             *          ________                     z
             *         /       /|                   /
             *        / White / |                  /_____
             *       /_______/  |                  |     x
             * Red > |       |  / < Orange         |
             *       | Blue  | /                   y
             *       |_______|/
             *           ^
             *         Yellow
             */

            for (int i = 1; i <= DIMENSION; i++)
                for (int j = 1; j <= DIMENSION; j++)
                {
                    Solved[i, 0, j] = RubiksColor.White;
                    Solved[i, j, 0] = RubiksColor.Blue;
                    Solved[0, i, j] = RubiksColor.Red;
                    Solved[i, DIMENSION + 1, j] = RubiksColor.Yellow;
                    Solved[i, j, DIMENSION + 1] = RubiksColor.Green;
                    Solved[DIMENSION + 1, i, j] = RubiksColor.Orange;
                }

            // Creates the cubies.
            for (int x = 0; x < DIMENSION; x++)
                for (int y = 0; y < DIMENSION; y++)
                    for (int z = 0; z < DIMENSION; z++)
                        _cubies.Add(new Cubie(x, y, z, this));
        }

        /// <summary>
        /// Rotates the specified cubies around an axis.
        /// </summary>
        /// <param name="cubies">The cubies.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="reverse">Specifies if the rotation must be reverted.</param>
        internal void RotateCubiesAroundAxis(IEnumerable<Cubie> cubies, Axis axis, bool reverse)
        {
            foreach (Cubie c in cubies)
            {
                int newX = c.X, newY = c.Y, newZ = c.Z;

                // Depending on the axis and on the type of rotation, the x-y-z coordinates will change.
                if (reverse)
                    switch (axis)
                    {
                        case Axis.Z:
                            newX = DIMENSION - 1 - c.Y;
                            newY = c.X;
                            break;
                        case Axis.X:
                            newZ = DIMENSION - 1 - c.Y;
                            newY = c.Z;
                            break;
                        case Axis.Y:
                            newX = DIMENSION - 1 - c.Z;
                            newZ = c.X;
                            break;
                    }
                else
                    switch (axis)
                    {
                        case Axis.Z:
                            newY = DIMENSION - 1 - c.X;
                            newX = c.Y;
                            break;
                        case Axis.X:
                            newY = DIMENSION - 1 - c.Z;
                            newZ = c.Y;
                            break;
                        case Axis.Y:
                            newZ = DIMENSION - 1 - c.X;
                            newX = c.Z;
                            break;
                    }

                c.X = newX;
                c.Y = newY;
                c.Z = newZ;

                // The cubie must be also rotated to have the colors in the right place.
                c.Rotate(axis, reverse);
            }
        }

        /// <summary>
        /// Rotates one face.
        /// </summary>
        /// <param name="face">The face.</param>
        /// <param name="reverse">Specifies if the rotation must be reverted.</param>
        internal void RotateFace(Face face, bool reverse)
        {
            if (face == Face.Front || face == Face.Back)
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.Z, reverse);
            else if (face == Face.Left || face == Face.Right)
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.X, reverse);
            else
                RotateCubiesAroundAxis(GetFaceCubies(face), Axis.Y, reverse);
        }

        /// <summary>
        /// Creates a copy of this instance.
        /// </summary>
        /// <returns>The copy.</returns>
        public Cube Clone()
        {
            Cube clone = new Cube();

            clone._cubies.Clear();

            foreach (Cubie c in _cubies)
                clone._cubies.Add(c.Clone());

            return clone;
        }

        /// <summary>
        /// Executes a specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void ExecuteMove(Move move)
        {
            Face f = (Face)Math.Abs((int)move);

            RotateFace(f, (int)move > 0);
        }

        /// <summary>
        /// Gets the list of the cubies contained in the specified face.
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public IEnumerable<Cubie> GetFaceCubies(Face face)
        {
            foreach (Cubie c in _cubies)
                if (FaceContains(face, c))
                    yield return c;
        }

        /// <summary>
        /// Gets the color of the center cubie of the specified face.
        /// </summary>
        /// <param name="face">The face.</param>
        /// <returns>The color.</returns>
        public RubiksColor GetFaceColor(Face face)
        {
            return (RubiksColor)GetFaceCubies(face).Centers().ElementAt<Cubie>(0).GetColor(face);
        }

        /// <summary>
        /// Indicates if a face contains a specified cubie.
        /// </summary>
        /// <param name="face">The face..</param>
        /// <param name="cubie">The cubie</param>
        /// <returns></returns>
        public bool FaceContains(Face face, Cubie cubie)
        {
            switch (face)
            {
                case Face.Front:
                    if (cubie.Z == 0)
                        return true;
                    return false;
                case Face.Back:
                    if (cubie.Z == DIMENSION - 1)
                        return true;
                    return false;
                case Face.Right:
                    if (cubie.X == DIMENSION - 1)
                        return true;
                    return false;
                case Face.Left:
                    if (cubie.X == 0)
                        return true;
                    return false;
                case Face.Up:
                    if (cubie.Y == 0)
                        return true;
                    return false;
                case Face.Down:
                    if (cubie.Y == DIMENSION - 1)
                        return true;
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the enumerator of the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<Cubie> GetEnumerator()
        {
            foreach (Cubie c in _cubies)
                yield return c;
        }

        /// <summary>
        /// Gets the enumerator of the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Returns the center with the specified color, if existing.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The center.</returns>
        public Cubie FindCenter(RubiksColor color)
        {
            foreach (Cubie c in Centers)
                if (c.Colors.Contains(color))
                    return c;

            return null;
        }

        /// <summary>
        /// Returns the edge with the specified colors, if existing.
        /// </summary>
        /// <param name="color1">The first color.</param>
        /// <param name="color2">The second color.</param>
        /// <returns>The edge.</returns>
        public Cubie FindEdge(RubiksColor color1, RubiksColor color2)
        {
            foreach (Cubie c in Edges)
                if (c.Colors.Contains(color1) && c.Colors.Contains(color2))
                    return c;

            return null;
        }

        /// <summary>
        /// Returns the corner with the specified colors, if existing.
        /// </summary>
        /// <param name="color1">The first color.</param>
        /// <param name="color2">The second color.</param>
        /// <param name="color3">The third color.</param>
        /// <returns>The corner.</returns>
        public Cubie FindCorner(RubiksColor color1, RubiksColor color2, RubiksColor color3)
        {
            foreach (Cubie c in Corners)
                if (c.Colors.Contains(color1) && c.Colors.Contains(color2) && c.Colors.Contains(color3))
                    return c;

            return null;
        }

        /// <summary>
        /// Indicates if the cubie is in the right place.
        /// </summary>
        /// <param name="cubie">The cubie.</param>
        /// <returns>True if the cubie is placed correctly.</returns>
        public bool IsCubiePlacedCorrectly(Cubie cubie)
        {
            foreach (Face f in Enum.GetValues(typeof(Face)))
                if (cubie.GetColor(f) != null && cubie.GetColor(f) != GetFaceColor(f))
                    return false;
            return true;
        }

        /// <summary>
        /// Sets the view of the cube.
        /// </summary>
        /// <param name="front">The color of the front face.</param>
        /// <param name="up">The color of the up face.</param>
        public void SetView(RubiksColor front, RubiksColor up)
        {
            Cubie centerWithFrontColor = FindCenter(front);

            // Rotates the cube until the center reaches the front face.
            if (centerWithFrontColor.Y == 1)
                while (centerWithFrontColor.Z != 0)
                    RotateCubiesAroundAxis(_cubies, Axis.Y, false);
            else
                while (centerWithFrontColor.Z != 0)
                    RotateCubiesAroundAxis(_cubies, Axis.X, false);

            // Rotates the cube until the center reaches the up face.
            while (FindCubie(1, 0, 1).UpColor != up)
                RotateCubiesAroundAxis(_cubies, Axis.Z, false);
        }

        /// <summary>
        /// Sets the view of the cube.
        /// </summary>
        /// <param name="view">The view</param>
        public void SetView(CubeView view)
        {
            SetView(view.FrontColor, view.UpColor);
        }

        /// <summary>
        /// Finds a cubie from its x-y-z coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <returns></returns>
        public Cubie FindCubie(int x, int y, int z)
		{
			foreach (Cubie c in _cubies)
				if (c.X == x && c.Y == y && c.Z == z)
					return c;
				
			return null;
		}

        /// <summary>
        /// Indicates if the cube is solved or not.
        /// </summary>
        /// <returns>True if the cube is solved.</returns>
        public bool IsSolved()
        {
            foreach (Cubie c in _cubies)
                if (!IsCubiePlacedCorrectly(c))
                    return false;

            return true;
        }

        /// <summary>
        /// Scrambles the cube with a specified number of random moves.
        /// </summary>
        /// <param name="n">Number of moves.</param>
        public void Scramble(int n)
        {
            Random rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                Face f = (Face)rnd.Next(1, 7);
                bool rev = rnd.Next(0, 2) == 1;

                RotateFace(f, rev);
            }
        }
    }
}

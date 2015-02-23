using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    /// <summary>
    /// Represents one of the 26 cubies of a rubik's cube.
    /// </summary>
    public class Cubie
    {
        /// <summary>
        /// Gets an array that contains the colors of the cubie in the following order:
        /// Front - Back - Right - Left - Up - Down
        /// </summary>
        public RubiksColor?[] Colors { get; private set; }

        /// <summary>
        /// Gets the cubie type (Center / Edge / Corner).
        /// </summary>
        public CubieType CubieType { get; private set;}

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        /// Gets the front color.
        /// </summary>
        public RubiksColor? FrontColor { get { return Colors[0]; } }

        /// <summary>
        /// Gets the back color.
        /// </summary>
        public RubiksColor? BackColor { get { return Colors[1]; } }

        /// <summary>
        /// Gets the right color.
        /// </summary>
        public RubiksColor? RightColor { get { return Colors[2]; } }

        /// <summary>
        /// Gets the left color.
        /// </summary>
        public RubiksColor? LeftColor { get { return Colors[3]; } }

        /// <summary>
        /// Gets the up color.
        /// </summary>
        public RubiksColor? UpColor { get { return Colors[4]; } }

        /// <summary>
        /// Gets the down color.
        /// </summary>
        public RubiksColor? DownColor { get { return Colors[5]; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cubie"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        /// <param name="cube">The cube.</param>
        public Cubie(int x, int y, int z, Cube cube) : this(x, y, z)
        {
            Colors = new RubiksColor?[6];

            // Gets the colors from the 5x5x5 matrix.
            Colors[0] = cube.Solved[X + 1, Y + 1, Z];
            Colors[1] = cube.Solved[X + 1, Y + 1, Z + 2];
            Colors[2] = cube.Solved[X + 2, Y + 1, Z + 1];
            Colors[3] = cube.Solved[X, Y + 1, Z + 1];
            Colors[4] = cube.Solved[X + 1, Y, Z + 1];
            Colors[5] = cube.Solved[X + 1, Y + 2, Z + 1];

            // Counts the number of the colored faces.
            int coloredFaces = 0;

            if (FrontColor != null)
                coloredFaces++;
            if (BackColor != null)
                coloredFaces++;
            if (RightColor != null)
                coloredFaces++;
            if (LeftColor != null)
                coloredFaces++;
            if (UpColor != null)
                coloredFaces++;
            if (DownColor != null)
                coloredFaces++;

            CubieType = (CubieType)coloredFaces;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cubie"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="z">The Z coordinate.</param>
        private Cubie(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Creates a copy of this cubie.
        /// </summary>
        /// <returns>The copy.</returns>
        public Cubie Clone()
        {
            Cubie clone = new Cubie(this.X, this.Y, this.Z);

            clone.Colors = new RubiksColor?[6];
            for (int i = 0; i < Colors.Length; i++)
                clone.Colors[i] = this.Colors[i];

            clone.CubieType = this.CubieType;

            return clone;
        }

        /// <summary>
        /// Rotates the colors of the cubie around an axis.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="reverse">Specifies if the rotation must be reverted.</param>
        public void Rotate(Axis axis, bool reverse)
        {
            RubiksColor?[] newColors = null;

            switch (axis)
            {
                case Axis.X:
                    if (!reverse)
                        newColors = new RubiksColor?[] { UpColor, DownColor, RightColor, LeftColor, BackColor, FrontColor };
                    else
                        newColors = new RubiksColor?[] { DownColor, UpColor, RightColor, LeftColor, FrontColor, BackColor };
                    break;
                case Axis.Y:
                    if (!reverse)
                        newColors = new RubiksColor?[] { RightColor, LeftColor, BackColor, FrontColor, UpColor, DownColor };
                    else
                        newColors = new RubiksColor?[] { LeftColor, RightColor, FrontColor, BackColor, UpColor, DownColor };
                    break;
                case Axis.Z:
                    if (!reverse)
                        newColors = new RubiksColor?[] { FrontColor, BackColor, DownColor, UpColor, RightColor, LeftColor };
                    else
                        newColors = new RubiksColor?[] { FrontColor, BackColor, UpColor, DownColor, LeftColor, RightColor };
                    break;
            }

            Colors = newColors;
        }

        /// <summary>
        /// Gets the color of the specified face of the cubie.
        /// </summary>
        /// <param name="f">The face.</param>
        /// <returns>The color.</returns>
        public RubiksColor? GetColor(Face f)
        {
            return Colors[(int)f - 1];
        }
    }
}

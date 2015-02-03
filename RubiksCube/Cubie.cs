using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    public class Cubie
    {
        public RubiksColor?[] Colors { get; private set; }
        public CubieType CubieType { get; private set;}

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public RubiksColor? FrontColor { get { return Colors[0]; } }
        public RubiksColor? BackColor { get { return Colors[1]; } }
        public RubiksColor? RightColor { get { return Colors[2]; } }
        public RubiksColor? LeftColor { get { return Colors[3]; } }
        public RubiksColor? UpColor { get { return Colors[4]; } }
        public RubiksColor? DownColor { get { return Colors[5]; } }

        internal Cubie(int x, int y, int z, Cube cube) : this(x, y, z)
        {
            Colors = new RubiksColor?[6];

            Colors[0] = cube.Solved[X + 1, Y + 1, Z];
            Colors[1] = cube.Solved[X + 1, Y + 1, Z + 2];
            Colors[2] = cube.Solved[X + 2, Y + 1, Z + 1];
            Colors[3] = cube.Solved[X, Y + 1, Z + 1];
            Colors[4] = cube.Solved[X + 1, Y, Z + 1];
            Colors[5] = cube.Solved[X + 1, Y + 2, Z + 1];

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

        private Cubie(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Cubie Clone()
        {
            Cubie clone = new Cubie(this.X, this.Y, this.Z);
            clone.Colors = this.Colors;
            clone.CubieType = this.CubieType;

            return clone;
        }
    }
}

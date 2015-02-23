using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents the flat configuration of the last layer.
    /// </summary>
    public class LastLayerConfiguration
    {
        const int DIM = 3;

        private RubiksColor?[,] _face; // Colors.

        /// <summary>
        /// Gets the color at position [x, y];
        /// </summary>
        /// <param name="x">The column.</param>
        /// <param name="y">The row.</param>
        /// <returns>The color</returns>
        public RubiksColor? this[int x, int y] { get { return _face[x, y]; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastLayerConfiguration"/> class.
        /// </summary>
        /// <param name="face">The face.</param>
        public LastLayerConfiguration(IEnumerable<Cubie> face)
        {
            _face = new RubiksColor?[5, 5];

            foreach (Cubie c in face)
            {
                _face[DIM - c.Z, c.X + 1] = c.UpColor;

                if (c.X == 0)
                    _face[DIM - c.Z, 0] = c.LeftColor;
                else if (c.X == 2)
                    _face[DIM - c.Z, 4] = c.RightColor;

                if (c.Z == 0)
                    _face[4, c.X + 1] = c.FrontColor;
                else if (c.Z == 2)
                    _face[0, c.X + 1] = c.BackColor;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastLayerConfiguration"/> class.
        /// </summary>
        /// <param name="config">The matrix.</param>
        public LastLayerConfiguration(RubiksColor?[,] config)
        {
            _face = config;
        }

        /// <summary>
        /// Checks if this configuration matches another one.
        /// </summary>
        /// <param name="conf">The other configuration.</param>
        /// <returns>True if the two configurations match.</returns>
        public bool Matches(LastLayerConfiguration conf)
        {
            for (int x = 0; x < _face.GetLength(0); x++)
                for (int y = 0; y < _face.GetLength(1); y++)
                {
                    if (this[x, y] == RubiksColor.NonYellow || conf[x, y] == RubiksColor.NonYellow)
                    {
                        if (this[x, y] == RubiksColor.Yellow || conf[x, y] == RubiksColor.Yellow)
                            return false;
                    }

                    else if (this[x, y] != RubiksColor.Any && conf[x, y] != RubiksColor.Any && this[x, y] != conf[x, y])
                        return false;
                }

            return true;
        }
    }
}

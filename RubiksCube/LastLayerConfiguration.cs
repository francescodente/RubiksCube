using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class LastLayerConfiguration
    {
        const int DIM = 3;

        private RubiksColor?[,] _face;

        public RubiksColor? this[int x, int y] { get { return _face[x, y]; } }

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

        public LastLayerConfiguration(RubiksColor?[,] config)
        {
            _face = config;
        }

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

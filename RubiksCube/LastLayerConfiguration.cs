using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class LastLayerConfiguration
    {
        private RubiksColor?[,] _face;

        public LastLayerConfiguration(IEnumerable<Cubie> face)
        {
            _face = new RubiksColor?[5, 5];

            foreach (Cubie c in face)
            {
                _face[c.X + 1, 1 - c.Z] = c.UpColor;

                if (c.X == 0)
                    _face[0, 1 - c.Z] = c.LeftColor;
                else if (c.X == 2)
                    _face[4, 1 - c.Z] = c.RightColor;

                if (c.Z == 0)
                    _face[c.X + 1, 4] = c.FrontColor;
                else if (c.Z == 2)
                    _face[c.X + 1, 0] = c.BackColor;
            }
        }

        public LastLayerConfiguration(RubiksColor?[,] config)
        {
            _face = config;
        }

        public bool Matches(LastLayerConfiguration configuration)
        {
            for (int x = 0; x < _face.GetLength(0); x++)
                for (int y = 0; y < _face.GetLength(1); y++)
                    if (this._face[x, y] != RubiksColor.Any
                        && configuration._face[x, y] != RubiksColor.Any
                        && this._face[x, y] != configuration._face[x, y])
                        return false;

            return true;
        }
    }
}

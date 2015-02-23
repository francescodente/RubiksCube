using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public class CubeView
    {
        public RubiksColor FrontColor { get; set; }
        public RubiksColor UpColor { get; set; }

        public CubeView(RubiksColor front, RubiksColor up)
        {
            FrontColor = front;
            UpColor = up;
        }

        public static CubeView Any { get { return new CubeView(RubiksColor.Any, RubiksColor.Any); } }
    }
}

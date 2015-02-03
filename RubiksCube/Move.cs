using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public enum Move
    {
        Front = 1,
        FrontPrime = -1,
        Back = -2,
        BackPrime = 2,
        Right = 3,
        RightPrime = -3,
        Left = -4,
        LeftPrime = 4,
        Up = -5,
        UpPrime = 5,
        Down = 6,
        DownPrime = -6
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RubiksCube
{
    /// <summary>
    /// Identifies the colors of the faces and some helper colors.
    /// </summary>
    public enum RubiksColor
    {
        White = 1,
        Yellow = -1,
        Green = 2,
        Blue = -2,
        Red = 3,
        Orange = -3,
        Any = 0,
        NonYellow = 4
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Identifies all the possible moves.
    /// </summary>
    public enum Move
    {
        /// <summary>
        /// Moves the front face clockwise.
        /// </summary>
        Front = 1,

        /// <summary>
        /// Moves the front face counter-clockwise.
        /// </summary>
        FrontPrime = -1,

        /// <summary>
        /// Moves the back face clockwise.
        /// </summary>
        Back = -2,

        /// <summary>
        /// Moves the back face counter-clockwise.
        /// </summary>
        BackPrime = 2,

        /// <summary>
        /// Moves the right face clockwise.
        /// </summary>
        Right = 3,

        /// <summary>
        /// Moves the right face counter-clockwise.
        /// </summary>
        RightPrime = -3,

        /// <summary>
        /// Moves the left face clockwise.
        /// </summary>
        Left = -4,

        /// <summary>
        /// Moves the left face counter-clockwise.
        /// </summary>
        LeftPrime = 4,

        /// <summary>
        /// Moves the up face clockwise.
        /// </summary>
        Up = -5,

        /// <summary>
        /// Moves the up face counter-clockwise.
        /// </summary>
        UpPrime = 5,

        /// <summary>
        /// Moves the down face clockwise.
        /// </summary>
        Down = 6,

        /// <summary>
        /// Moves the down face counter-clockwise.
        /// </summary>
        DownPrime = -6
    }
}

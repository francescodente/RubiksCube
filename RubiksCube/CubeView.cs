using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Represents a view of the cube identified by the front and up color.
    /// </summary>
    public class CubeView
    {
        /// <summary>
        /// Gets or sets the front color.
        /// </summary>
        public RubiksColor FrontColor { get; set; }

        /// <summary>
        /// Gets or sets the up color.
        /// </summary>
        public RubiksColor UpColor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CubeView"/> class.
        /// </summary>
        /// <param name="front">The front color.</param>
        /// <param name="up">The up color.</param>
        public CubeView(RubiksColor front, RubiksColor up)
        {
            FrontColor = front;
            UpColor = up;
        }

        /// <summary>
        /// Gets a value that represents any view.
        /// </summary>
        public static CubeView Any { get { return new CubeView(RubiksColor.Any, RubiksColor.Any); } }
    }
}

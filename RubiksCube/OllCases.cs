using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    /// <summary>
    /// Contains the possible combinations that may occur during the OLL phase.
    /// </summary>
    public static class OllCases
    {
        public static LastLayerConfiguration YellowL = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.NonYellow, RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration YellowLine = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration YellowCenter = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Yellow,    RubiksColor.NonYellow, RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.NonYellow, RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration OLLSituation1 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow },
            { null,                  RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration OLLSituation2 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               }
        });

        public static LastLayerConfiguration OLLSituation3 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    null               },
            { RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration OLLSituation4 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               }
        });

        public static LastLayerConfiguration OLLSituation5 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration OLLSituation6 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               },
            { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Yellow,    null               }
        });

        public static LastLayerConfiguration OLLSituation7 = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Any,       RubiksColor.Yellow },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });

        public static LastLayerConfiguration OLLComplete = new LastLayerConfiguration(new RubiksColor?[,]
        {
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { RubiksColor.Any,       RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Yellow,    RubiksColor.Any    },
            { null,                  RubiksColor.Any,       RubiksColor.Any,       RubiksColor.Any,       null               }
        });
    }
}

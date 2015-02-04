using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube
{
    public static class OllCases
    {
        public static UpperLayerConfiguration YellowL = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration YellowLine = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration YellowCenter = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration OLLSituation1 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow },
            { null,               RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration OLLSituation2 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any,    null               },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               }
        });

        public static UpperLayerConfiguration OLLSituation3 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, null               },
            { RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration OLLSituation4 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               }
        });

        public static UpperLayerConfiguration OLLSituation5 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });

        public static UpperLayerConfiguration OLLSituation6 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Any    },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Yellow, null               }
        });

        public static UpperLayerConfiguration OLLSituation7 = new UpperLayerConfiguration(new RubiksColor?[,]
        {
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow },
            { RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Yellow, RubiksColor.Any    },
            { RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow, RubiksColor.Any,    RubiksColor.Yellow },
            { null,               RubiksColor.Any,    RubiksColor.Any,    RubiksColor.Any,    null               }
        });
    }
}

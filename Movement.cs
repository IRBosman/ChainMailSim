using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainmailSim
{
    public static class Movement
    {
        private static decimal[] movements =>
            [
                /*LF*/ 9,
                /*HF*/ 9,
                /*AF*/ 6,
                /*LH*/ 24,
                /*MH*/ 18,
                /*HH*/ 12,
            ];

        public static decimal Get(UnitTypes type) => movements[(int)type];
    }
}

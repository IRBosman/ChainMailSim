using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainmailSim
{
    public static class Attack_matrix
    {
        private static Attacks[][] matrix = new Attacks[][]{

            //      LF                              HF              AF                      LH                   MH                  HH
            /*LF*/ [new Attacks(1m,6), new Attacks(1/2m, 6), new Attacks(1/3m, 6), new Attacks(1/2m, 6), new Attacks(1/3m, 6), new Attacks(1/4m, 6)],
            /*HF*/ [new Attacks(1m,5), new Attacks(1m, 6),   new Attacks(1/2m, 6), new Attacks(1/2m, 6), new Attacks(1/3m, 6), new Attacks(1/4m, 6)],
            /*AF*/ [new Attacks(1m,4), new Attacks(1/2m, 5), new Attacks(1/3m, 6), new Attacks(1m, 6),   new Attacks(1/2m, 6), new Attacks(1/3m, 6)],
            /*LH*/ [new Attacks(2m,5), new Attacks(2m, 6),   new Attacks(1m, 6),   new Attacks(1m, 6),   new Attacks(1/2m, 6), new Attacks(1/3m, 6)],
            /*MH*/ [new Attacks(2m,4), new Attacks(2m, 5),   new Attacks(2m, 6),   new Attacks(1m, 5),   new Attacks(1m, 6),   new Attacks(1m, 6)],
            /*HH*/ [new Attacks(4m,5), new Attacks(3/1m, 5), new Attacks(2/1m, 5), new Attacks(2m, 6),   new Attacks(1/1m, 5), new Attacks(1m, 6)],
            };


        public static Attacks Get(UnitTypes attacker, UnitTypes defender) => matrix[(int)attacker][(int)defender];
}
}

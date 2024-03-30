using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainmailSim
{
    public struct Attacks
    {
        private decimal perMan;
        private int toHit;

        public Attacks(decimal perMan, int toHit)
        {
            this.perMan = perMan;
            this.toHit = toHit;
        }

        internal int Roll(int attackers, Random random)
        {
            var hits = 0;
            for (int i = 0; i < Math.Floor(attackers * perMan); i++)
            {
                if (random.Next(1, 7) >= toHit) hits++;
            }
            return hits;
        }
    }
}

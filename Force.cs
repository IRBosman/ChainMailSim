
namespace ChainmailSim
{
    public class Force
    {
        public int FigsLeft { get; internal set; }
        public UnitTypes Type { get; internal set; }
        public decimal ChargeLeft { get; internal set; }
        public bool MustRally { get; internal set; }

        public int Fatigue { get; set;  }

        internal int NumberFighters(int frontage)
        {
            if (MustRally) return 0;
            else return Math.Min(frontage, FigsLeft);
        }

    }
}
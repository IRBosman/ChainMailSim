namespace ChainmailSim
{
    public class Summary
    {
        public int Rounds;
        public bool? AttackerWon;
        public int AttackersLeft;
        public int DefendersLeft;

        public override string ToString() => $"{Rounds}, {AttackerWon}, {AttackersLeft}, {DefendersLeft}";
    }
}
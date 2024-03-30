namespace ChainmailSim
{
    public static class MoraleRating
    {
        public static int[] morale =
            [
                /*LF*/ 4,
                /*HF*/ 5,
                /*AF*/ 7,
                /*LH*/ 6,
                /*MH*/ 8,
                /*HH*/ 9,
            ];
        public static int Get(UnitTypes unitType) => morale[(int)unitType];
    }
}
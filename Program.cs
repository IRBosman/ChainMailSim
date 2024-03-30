using ChainmailSim;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

internal class Program
{
    [Argument(0)]
    [Required]
    private int NoBlues { get; set; }

    [Argument(1)]
    [Required]
    private string BlueString { get; set; }

    public UnitTypes BlueType => Enum.Parse<UnitTypes>(BlueString);

    [Argument(2)]
    [Required]
    private int NoReds { get; set; }

    [Argument(3)]
    [Required]
    private string RedString { get; set; }

    public UnitTypes RedType => Enum.Parse<UnitTypes>(RedString);

    [Argument(4)]
    private int Frontage { get; set; }

    [Option("-c", CommandOptionType.SingleValue, Description = "Charge distance left")]
    public int ChargeDistanceLeft { get; set; }

    [Option("-m", CommandOptionType.SingleValue, Description = "Monte Carlo simulation count. If larger than 1 no verbose logging will be generated.")]
    public int Sims { get; set; } = 1;

    private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

    private void OnExecute()
    {
      

        if (Sims <= 1)
        {
            var attacker = new Force
            {
                FigsLeft = NoBlues,
                Type = BlueType,
                ChargeLeft = ChargeDistanceLeft,
            };

            var defender = new Force
            {
                FigsLeft = NoReds,
                Type = RedType,
            };
            var res = new Battle(attacker, defender, Frontage, Console.WriteLine).Run(1);
            Console.WriteLine(res.ToString());
        }
        else
        {
            var range = Enumerable.Range(0, Sims).Select(t => 
                new Battle(new Force
                {
                    FigsLeft = NoBlues,
                    Type = BlueType,
                    ChargeLeft = ChargeDistanceLeft,
                },
                     new Force
                     {
                         FigsLeft = NoReds,
                         Type = RedType,
                     }, Frontage, null).Run(1));

            using (var f = new StreamWriter($"{NoBlues}{BlueString}_vs_{NoReds}{RedString}_{Frontage}Wide.csv"))
            {
                f.WriteLine($"Rounds,AttackerWon, AttackersLeft, DefendersLeft");
                foreach (var s in range)
                {
                    f.WriteLine(s.ToString());
                }
            }
        }
    }
}
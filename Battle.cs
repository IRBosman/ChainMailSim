using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainmailSim
{
    public class Battle
    {
        private readonly Force attacker;
        private readonly Force defender;
        private readonly int frontage;
        private readonly Action<string>? logger;
        private readonly Random random;

        public Battle(Force Attacker, Force Defender, int frontage, Action<string>? logger)
        {
            attacker = Attacker;
            defender = Defender;
            this.frontage = frontage;
            this.logger = logger;
            this.random = new Random();
        }

        public Summary Run(int round)
        {
            logger?.Invoke($"Round {round}");

            if (defender.MustRally)
            {
                logger?.Invoke("Defender must rally");
                if (random.Next(1, 7) <= 2)
                {
                    defender.MustRally = false;
                    logger?.Invoke("Defender rallies!");

                }
                else
                {
                    logger?.Invoke("Defender fails to rally. Cannot attack back");
                }
            }


            var attackers = attacker.NumberFighters(frontage);
            var defenders = defender.NumberFighters(frontage);


            var attacker_kills = Attack_matrix.Get(attacker.Type, defender.Type).Roll(attackers, random);
            var defender_kills = Attack_matrix.Get(defender.Type, attacker.Type).Roll(defenders, random);

            logger?.Invoke($"{attackers} attackers kill {attacker_kills}");
            logger?.Invoke($"{defenders} attackers kill {defender_kills}");

            attacker.FigsLeft = Math.Max(attacker.FigsLeft-  defender_kills, 0);
            defender.FigsLeft = Math.Max(defender.FigsLeft - attacker_kills, 0);

            if (attacker.FigsLeft == 0 && defender.FigsLeft == 0 )
            {
                logger?.Invoke("Tie. Both dead.");
                return MakeSummary(round, null);
            }
            if (attacker.FigsLeft == 0)
            {
                logger?.Invoke("Attackers wiped out");
                return MakeSummary(round, false);
            }
            if (attacker.FigsLeft == 0)
            {
                logger?.Invoke("Defenders wiped out");
                return MakeSummary(round, true);
            }

            var scoreAttacker = 0;
            var scoreDefender = 0;

            if (attacker_kills >= defender_kills)
            {
                scoreAttacker = (attacker_kills - defender_kills) * random.Next(1, 7);
            }
            else
            {
                scoreDefender = (defender_kills - attacker_kills) * random.Next(1, 7);
            }

            if (attacker.FigsLeft >= defender.FigsLeft)
            {
                scoreAttacker += (attacker.FigsLeft - defender.FigsLeft);
            }
            else
            {
                scoreDefender += (defender.FigsLeft - attacker.FigsLeft);
            }

            scoreAttacker += attacker.FigsLeft * MoraleRating.Get(attacker.Type);
            scoreDefender += defender.FigsLeft * MoraleRating.Get(defender.Type);

            if (attacker.FigsLeft < 20 && defender.FigsLeft < 20)
            {
                scoreAttacker *= 2;
                scoreDefender *= 2;
            }

            bool defenderLost = scoreAttacker >= scoreDefender;
            var diff = Math.Abs(scoreAttacker - scoreDefender);
            logger?.Invoke($"Atacker score: {scoreAttacker}. Defender score: {scoreDefender}. Difference: {diff}");


            if (diff <= 19)
            {
                logger?.Invoke("Melee continues");
                return Run(round + 1);
            }
            else if (diff <= 39)
            {
                if (defenderLost)
                {
                    return HandleMoveBackAndPossibleContinue(1 / 2m, round);
                }
                else
                {
                    logger?.Invoke("Attack lost round and moves back. Battle over");


                    return MakeSummary(round, defenderLost);
                }
            }
            else if (diff <= 59)
            {
                if (defenderLost)
                {
                    return HandleMoveBackAndPossibleContinue(1m, round);
                }
                else
                {
                    logger?.Invoke("Attack lost round and moves back. Battle over");
                    return MakeSummary(round, defenderLost);
                }

            }
            else if (diff <= 79)
            {
                if (defenderLost)
                {
                    return HandleRoutAndPossibleContinue(1.5m, round);
                }
                else
                {
                    logger?.Invoke("Attack lost round and moves back. Battle over");
                    return MakeSummary(round, defenderLost);
                }
            }
            else
            {
                if (defenderLost)
                {
                    return HandleRoutAndPossibleContinue(1.5m, round);
                }
                else
                {
                    logger?.Invoke("Attack lost round and moves back. Battle over");
                    return MakeSummary(round, defenderLost);
                }
            }
        }

        private Summary MakeSummary(int round, bool? defenderLost)
        {
            return new Summary
            {
                AttackersLeft = attacker.FigsLeft,
                DefendersLeft = defender.FigsLeft,
                AttackerWon = defenderLost,
                Rounds = round,
            };
        }

        private Summary HandleMoveBackAndPossibleContinue(decimal v, int round)
        {
            var dist = v * Movement.Get(defender.Type);
            logger?.Invoke($"defender moves back {dist}.");
            var attackerCanChase = attacker.ChargeLeft >= dist;

            if (attackerCanChase)
            {
                logger?.Invoke($"Attacker can chase.");
                attacker.ChargeLeft -= dist;

                return Run(round + 1);
            }
            else
            {
                logger?.Invoke($"Attacker cannot keep up. Battle ends");
                return MakeSummary(round, true);
            }
        }

        private Summary HandleRoutAndPossibleContinue(decimal v, int round)
        {
            var dist = v * Movement.Get(defender.Type);
            logger?.Invoke($"defender moves back {dist}.");
            var attackerCanChase = attacker.ChargeLeft >= dist;

            if (attackerCanChase)
            {
                logger?.Invoke($"Attacker can chase.");
                attacker.ChargeLeft -= dist;

                defender.MustRally = true;

                return Run(round + 1);
            }
            else
            {
                logger?.Invoke($"Attacker cannot keep up. Battle ends");
                return MakeSummary(round, true);
            }
        }
    }
}

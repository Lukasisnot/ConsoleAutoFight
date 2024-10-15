using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Player : Character
    {
        public ConsoleKeyInfo input;

        public Player(string name, ECharState state, bool isLeft, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, state, isLeft, color, hp, baseDamage, weapon)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!Console.KeyAvailable) return;
            if (State == ECharState.STAND)
                input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.D:
                    Attack();
                    break;
                case ConsoleKey.E:
                    Heal(EPotions.MEDIUM);
                    break;
                case ConsoleKey.S:
                    Dodge();
                    break;
                default:
                    break;
            }
            while(Console.KeyAvailable) 
                Console.ReadKey(false);
        }
    }
}
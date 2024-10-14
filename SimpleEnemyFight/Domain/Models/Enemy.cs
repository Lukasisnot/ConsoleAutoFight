using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Enemy : Character
    {
        public Enemy(string name, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, color, hp, baseDamage, weapon)
        {
        }

        public override string ToString() 
        {
            return (base.ToString() + $"") ;
        }
    }
}

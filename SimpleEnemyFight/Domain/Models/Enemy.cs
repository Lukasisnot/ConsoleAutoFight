using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Enemy : Character
    {
        public Enemy(string name, ECharState state, bool isLeft, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, state, isLeft, color, hp, baseDamage, weapon)
        {
        }
        
        public static class Factory
        {
            public static Enemy CreateEnemy1()
            {
                return new Enemy("JJ", ECharState.STAND, false, ConsoleColor.Cyan, 100, 10, EWeapons.STICK);
            }            
            public static Enemy CreateEnemy2()
            {
                return new Enemy("Jenda", ECharState.STAND, false, ConsoleColor.Red, 100, 10, EWeapons.STICK);
            }

            public static Enemy[] CreateEnemy = new[] { CreateEnemy1(), CreateEnemy2() };
        }
        
        public override string ToString() 
        {
            return (base.ToString() + $"") ;
        }
    }
}

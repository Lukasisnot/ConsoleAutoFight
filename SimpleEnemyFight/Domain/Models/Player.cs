using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Player : Character
    {
        public Player(string name, ConsoleColor color, float hp, float maxHp, float baseDamage, EWeapons weapon) : base(name, color, hp, baseDamage, weapon)
        {
        }
    }
}
using System;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Treasure : Entity
    {
        public Treasure(string name, ConsoleColor color, float hp) : base(name, color, hp)
        {
        }
    }
}
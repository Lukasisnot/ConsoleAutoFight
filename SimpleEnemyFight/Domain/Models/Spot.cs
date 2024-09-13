using System;

namespace SimpleEnemyFight.Domain.Models
{
    public class Spot
    {
        public char Char;
        public ConsoleColor Color;

        public Spot()
        {
            Char = ' ';
            Color = ConsoleColor.White;
        }
    }
}
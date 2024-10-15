using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Treasure : Entity
    {
        public EWeapons Weapon = EWeapons.NONE;
        public EPotions Potion = EPotions.NONE;
        
        public Treasure(string name = "Chest", ESprites sprite = ESprites.CHEST_CLOSED, ConsoleColor color = ConsoleColor.DarkYellow, float hp = 0.1f) : base(name, sprite, color, hp)
        {
            if (Rand.Next(2) == 0)
                Weapon = (EWeapons)Rand.Next((int)EWeapons.NONE);
            else
                Potion = (EPotions)Rand.Next((int)EPotions.NONE);
        }
    }
}
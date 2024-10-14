using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Character : Entity
    {
        public float BaseDamage { get; private set; }
        public EWeapons Weapon { get; private set; }

        public Character(string name, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, color, hp)
        {
            BaseDamage = baseDamage;
            Weapon = weapon;
        }

        public virtual void Attack(Entity entity)
        {
            float dmgMult = 1;

            switch (Weapon)
            {
                case EWeapons.STICK: 
                    dmgMult = 1f;
                    break;
                case EWeapons.DAGGER: 
                    dmgMult = 1.2f;
                    break;
                case EWeapons.SWORD: 
                    dmgMult = 1.5f;
                    break;
            }

            entity.Damage(Math.Max(0, entity.Hp - this.BaseDamage * dmgMult));
        }
        
        public virtual void Heal(EPotions potion)
        {
            if (!IsAlive) return;

            switch(potion)
            {
                case EPotions.SMALL: 
                    Hp += 5;
                    break;
                case EPotions.MEDIUM: 
                    Hp += 10;
                    break;
                case EPotions.LARGE: 
                    Hp += 15;
                    break;
            }

            Hp = Math.Min(Hp, MaxHp);
        }
        
        public override string ToString() 
        {
            string output = $"Name: {Name}" +
                            $"HP: {Hp}" +
                            $"Is Alive: {IsAlive}" +
                            $"Base DMG: {BaseDamage}" +
                            $"Weapon: {Weapon}";
            return output;
        }
    }
}
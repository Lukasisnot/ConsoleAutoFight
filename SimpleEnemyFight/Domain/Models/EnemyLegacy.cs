using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class EnemyLegacy : EntityLegacy
    {
        private Random rand;
        public static int EnemyCount;
        public float BaseDamage { get; private set; }
        public EWeapons Weapon { get; private set; }
        public float Hp { get; set; }
        public float MaxHp { get; private set; }
        public bool IsAlive
        {
            get { return Hp > 0; }
            set { }
        }
        public EEnemyState State { get; set; }
        public ConsoleColor Color { get; set; }

        public EnemyLegacy(string name, float baseDamage, EWeapons weapon, float hp, ConsoleColor color) 
        {
            EnemyCount++;
            this.IsAlive = true;
            this.Name = name;
            this.BaseDamage = baseDamage; 
            this.Weapon = weapon;
            this.Hp = hp;
            this.Color = color;
            this.MaxHp = hp;
            rand = new Random((int)DateTime.Now.Ticks * EnemyCount);
            Console.WriteLine(rand.Next(100));
        }

        ~EnemyLegacy()
        {
            EnemyCount--;
        }

        public void Attack(EnemyLegacy enemy)
        {
            float dmgMult = 1;

            switch (this.Weapon)
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

            enemy.Hp = Math.Max(0, enemy.Hp - this.BaseDamage * dmgMult);
        }

        public void Heal(EPotions potion)
        {
            if (!this.IsAlive) return;

            switch(potion)
            {
                case EPotions.SMALL: 
                    this.Hp += 5;
                    break;
                case EPotions.MEDIUM: 
                    this.Hp += 10;
                    break;
                case EPotions.LARGE: 
                    this.Hp += 15;
                    break;
            }

            this.Hp = Math.Min(this.Hp, this.MaxHp);
        }
        
        public void Heal()
        {
            if (!this.IsAlive) return;

            switch(rand.Next(3))
            {
                case 0: 
                    this.Hp += 5;
                    break;
                case 1: 
                    this.Hp += 12;
                    break;
                case 2: 
                    this.Hp += 15;
                    break;
            }

            this.Hp = Math.Min(this.Hp, this.MaxHp);
        }

        public override string ToString() 
        {
            string output = $"Name: {Name} | HP: {Hp} | Base DMG: {BaseDamage} | Weapon: {Weapon} | Is Alive: {IsAlive}";
            return output;
        }
    }
}

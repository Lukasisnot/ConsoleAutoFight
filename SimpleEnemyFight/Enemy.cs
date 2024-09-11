using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    internal class Enemy : Entity
    {
        Random rand;

        public static int EnemyCount;
        public float BaseDamage { get; private set; }
        public EWeapons Weapon { get; private set; }
        public float Hp { get; set; }
        private float maxHp;
        public bool IsAlive
        {
            get { return Hp > 0; }
            set { }
        }

        public Enemy(string name, float baseDamage, EWeapons weapon, float hp) 
        {
            EnemyCount++;
            this.IsAlive = true;
            this.Name = name;
            this.BaseDamage = baseDamage; 
            this.Weapon = weapon;
            this.Hp = hp;
            this.maxHp = hp;
            rand = new Random((int)DateTime.Now.Ticks * EnemyCount);
            Console.WriteLine(rand.Next(100));
        }

        ~Enemy()
        {
            EnemyCount--;
        }

        public void Attack(Enemy enemy)
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
            if (enemy.Hp == 0) enemy.IsAlive = false; 
        }

        public void Heal(EPotions potion)
        {
            if (!this.IsAlive) return;

            switch(potion)
            {
                case EPotions.SMALL: 
                    this.Hp += 25;
                    break;
                case EPotions.MEDIUM: 
                    this.Hp += 50;
                    break;
                case EPotions.LARGE: 
                    this.Hp += 75;
                    break;
            }

            this.Hp = Math.Min(this.Hp, this.maxHp);
        }

        public override string ToString() 
        {
            string output = $"Name: {Name} | HP: {Hp} | Base DMG: {BaseDamage} | Weapon: {Weapon} | Is Alive: {IsAlive}";
            return output;
        }
    }
}

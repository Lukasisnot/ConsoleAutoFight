using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Entity : GameObject
    {
        public ConsoleColor Color { get; set; }
        public string Name { get; set; }
        public float Hp { get; protected set; }
        public float MaxHp { get; protected set; }
        public bool IsAlive
        {
            get { return Hp > 0; }
            set { }
        }

        public Entity(string name, ConsoleColor color, float hp) : base()
        {
            Name = name;
            Color = color;
            Hp = hp;
            MaxHp = hp;
        }
        
        public virtual void Damage(float damage)
        {
            this.Hp -= damage;
        }
        
        public override string ToString() 
        {
            string output = $"Name: {Name}" +
                            $"HP: {Hp}" +
                            $"Is Alive: {IsAlive}";
            return output;
        }
    }
}

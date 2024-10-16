using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    public class Entity : GameObject
    {
        public virtual ESprites Sprite { get; set; }
        public int X = 0, Y = 0;
        public bool Visible = true;
        public ConsoleColor Color { get; set; }
        public string Name { get; set; }
        public float Hp { get; protected set; }
        public float MaxHp { get; protected set; }
        public bool IsAlive
        {
            get { return Hp > 0; }
            set { }
        }

        public delegate void DiedDelegate();
        public DiedDelegate DDied;

        public Entity(string name, ESprites sprite, ConsoleColor color, float hp) : base()
        {
            Name = name;
            Sprite = sprite;
            Color = color;
            Hp = hp;
            MaxHp = hp;
        }
        
        public virtual void Update()
        {
            Draw();
        }
        
        public virtual void Damage(float damage)
        {
            this.Hp -= damage;
            if (!(Hp > 0)) Die();
        }

        public virtual void Die()
        {
            DDied();
        }

        public virtual void Draw()
        {
            Renderer.Sprite(Sprite, X, Y, Color);
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

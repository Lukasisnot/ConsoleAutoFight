using System;
using System.Timers;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    public class Character : Entity
    {
        public float BaseDamage { get; private set; }
        public EWeapons Weapon;
        public ECharState State;
        public bool IsLeft;

        public override ESprites Sprite
        {
            get
            {
                return Renderer.ConvertCharSprite(State, IsLeft);
            }
            set {}
        }

        protected System.Timers.Timer AttackTimer = new System.Timers.Timer(500);
        protected System.Timers.Timer DodgeTimer = new System.Timers.Timer(550);
        protected System.Timers.Timer HealTimer = new System.Timers.Timer(1000);

        public Character(string name, ECharState state, bool isLeft, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, Renderer.ConvertCharSprite(state, isLeft), color, hp)
        {
            State = state;
            IsLeft = isLeft;
            BaseDamage = baseDamage;
            Weapon = weapon;

            AttackTimer.Elapsed += (Object source, ElapsedEventArgs e) => { State = ECharState.STAND; };
            AttackTimer.AutoReset = false;
            AttackTimer.Enabled = false;
            HealTimer.Elapsed += (Object source, ElapsedEventArgs e) => { State = ECharState.STAND; };
            HealTimer.AutoReset = false;
            HealTimer.Enabled = false;
            DodgeTimer.Elapsed += (Object source, ElapsedEventArgs e) => { State = ECharState.STAND; };
            DodgeTimer.AutoReset = false;
            DodgeTimer.Enabled = false;
        }

        public override void Draw()
        {
            base.Draw();
            Renderer.HealthBar(X, Y, 8, this, !IsLeft);
        }

        public virtual void Attack(Entity? entity = null)
        {
            State = ECharState.ATTACK;
            AttackTimer.Start();
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
            if (entity != null) entity.Damage(this.BaseDamage * dmgMult);
        }
        
        public virtual void Heal(EPotions potion)
        {
            if (!IsAlive) return;
            State = ECharState.HEAL;
            HealTimer.Start();
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

        public virtual void Dodge()
        {
            State = ECharState.DODGE;
            DodgeTimer.Start();
        }

        public override void Die()
        {
            base.Die();
            State = ECharState.DEAD;
            Color = ConsoleColor.Red;
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
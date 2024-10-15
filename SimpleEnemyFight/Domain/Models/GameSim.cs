using System;
using System.Threading;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    internal class GameSim
    {
        private readonly EnemyLegacy plL, plR;
        private readonly EnemyLegacy[] pls = new EnemyLegacy[2];
        private readonly RendererLegacy renderer;
        private Random rand = new Random();

        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public int StepInterval { get; set; }
        public int HealChance { get; set; }
        public GameSim(RendererLegacy renderer, EnemyLegacy plL, EnemyLegacy plR, int xpos, int ypos, int stepInterval, int healChance = 5)
        {
            this.renderer = renderer;
            this.plL = plL;
            this.plR = plR;
            pls[0] = plL;
            pls[1] = plR;
            Xpos = xpos;
            Ypos = ypos;
            StepInterval = stepInterval;
            HealChance = healChance;
        }

        public void Start()
        {
            InterStep();
        }
        
        void CombatStep()
        {
            switch (rand.Next(6))
            {
                case 0:
                    plL.State = ECharState.HIT;
                    plR.State = ECharState.ATTACK;
                    break;
                case 1:
                    plL.State = ECharState.ATTACK;
                    plR.State = ECharState.HIT;
                    break;
                case 2:
                    plL.State = ECharState.ATTACK;
                    plR.State = ECharState.DODGE;
                    break;
                case 3:
                    plL.State = ECharState.DODGE;
                    plR.State = ECharState.ATTACK;
                    break;
                case 4:
                    plL.State = ECharState.COLLISION;
                    plR.State = ECharState.COLLISION;
                    break;
                case 5:
                    plL.State = ECharState.COLLISION;
                    plR.State = ECharState.COLLISION;
                    break;
            }
            
            InterStep(true);
        }
        
        void InterStep(bool attacking = true)
        {
            Draw();
            
            foreach (var pl in pls)
            { 
                if (pl.State == ECharState.HIT) other(pl).Attack(pl);
                pl.State = pl.IsAlive ? ECharState.STAND : ECharState.DEAD;
            }
            
            Thread.Sleep(StepInterval / 2);
            
            Draw();
            Thread.Sleep(StepInterval / 2);

            if (!plL.IsAlive || !plR.IsAlive)
            {
                End();
                return;
            }
            
            if (rand.Next(HealChance) == 0 && (int)plL.Hp != (int)plL.MaxHp && (int)plR.Hp != (int)plR.MaxHp) HealStep();
            else CombatStep();
        }

        void HealStep()
        {
            foreach (var pl in pls)
            {
                pl.State = ECharState.HEAL;
                pl.Heal();
            }
            InterStep(false);
        }

        void End()
        {
            foreach (var pl in pls)
                if(pl.IsAlive) pl.State = ECharState.WIN;
            Draw();
        }

        void Draw()
        {
            // if (false)
            // {
            //     plL.State = EEnemyState.DODGE;
            //     plR.State = EEnemyState.ATTACK;
            // }
            // else
            // {
            //     plL.State = EEnemyState.HIT; 
            //     plR.State = EEnemyState.ATTACK;
            // }
            
            int xOffset = 0;
            int yOffset = 2;
            
            switch (plL.State)
            {
                case ECharState.ATTACK:
                    xOffset = 4;
                    break;
                case ECharState.COLLISION:
                    xOffset = 2;
                    break;
                case ECharState.DODGE:
                    xOffset = 2;
                    break;
            }
            renderer.Sprite(toESprites(plL.State, true), Xpos + xOffset, Ypos + yOffset, plL.Color);
            
            switch (plR.State)
            {
                
                case ECharState.ATTACK:
                    xOffset = 5;
                    break;
                case ECharState.COLLISION:
                    xOffset = 8;
                    break;
                case ECharState.HIT:
                    xOffset = 10;
                    break;
                default:
                    xOffset = 11;
                    break;
            }
            renderer.Sprite(toESprites(plR.State, false), Xpos + xOffset, Ypos + yOffset, plR.Color);
            
            renderer.HealthBar(0, 0, 10, plL);
            renderer.HealthBar(12, 0, 10, plR, true);
            
            renderer.Update();
        }

        ESprites toESprites(ECharState state, bool left)
        {
            switch (state)
            {
                case ECharState.STAND:
                    return left ? ESprites.LEFT_STAND : ESprites.RIGHT_STAND;
                    break;
                case ECharState.DODGE:
                    return left ? ESprites.LEFT_DODGE : ESprites.RIGHT_DODGE;
                    break;
                case ECharState.COLLISION:
                    return left ? ESprites.LEFT_COLLISION : ESprites.RIGHT_COLLISION;
                    break;
                case ECharState.ATTACK:
                    return left ? ESprites.LEFT_ATTACK : ESprites.RIGHT_ATTACK;
                    break;
                case ECharState.HEAL:
                    return left ? ESprites.LEFT_HEAL : ESprites.RIGHT_HEAL;
                    break;
                case ECharState.HIT:
                    return left ? ESprites.LEFT_HIT : ESprites.RIGHT_HIT;
                    break;
                case ECharState.DEAD:
                    return left ? ESprites.LEFT_DEAD : ESprites.RIGHT_DEAD;
                    break;
                case ECharState.WIN:
                    return left ? ESprites.LEFT_WIN : ESprites.RIGHT_WIN;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        EnemyLegacy other(EnemyLegacy current)
        {
            return current == plL ? plR : plL;
        }
    }
}

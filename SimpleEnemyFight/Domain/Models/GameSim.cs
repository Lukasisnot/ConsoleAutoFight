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
                    plL.State = EEnemyState.HIT;
                    plR.State = EEnemyState.ATTACK;
                    break;
                case 1:
                    plL.State = EEnemyState.ATTACK;
                    plR.State = EEnemyState.HIT;
                    break;
                case 2:
                    plL.State = EEnemyState.ATTACK;
                    plR.State = EEnemyState.DODGE;
                    break;
                case 3:
                    plL.State = EEnemyState.DODGE;
                    plR.State = EEnemyState.ATTACK;
                    break;
                case 4:
                    plL.State = EEnemyState.COLLISION;
                    plR.State = EEnemyState.COLLISION;
                    break;
                case 5:
                    plL.State = EEnemyState.COLLISION;
                    plR.State = EEnemyState.COLLISION;
                    break;
            }
            
            InterStep(true);
        }
        
        void InterStep(bool attacking = true)
        {
            Draw();
            
            foreach (var pl in pls)
            { 
                if (pl.State == EEnemyState.HIT) other(pl).Attack(pl);
                pl.State = pl.IsAlive ? EEnemyState.STAND : EEnemyState.DEAD;
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
                pl.State = EEnemyState.HEAL;
                pl.Heal();
            }
            InterStep(false);
        }

        void End()
        {
            foreach (var pl in pls)
                if(pl.IsAlive) pl.State = EEnemyState.WIN;
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
                case EEnemyState.ATTACK:
                    xOffset = 4;
                    break;
                case EEnemyState.COLLISION:
                    xOffset = 2;
                    break;
                case EEnemyState.DODGE:
                    xOffset = 2;
                    break;
            }
            renderer.Sprite(toESprites(plL.State, true), Xpos + xOffset, Ypos + yOffset, plL.Color);
            
            switch (plR.State)
            {
                
                case EEnemyState.ATTACK:
                    xOffset = 5;
                    break;
                case EEnemyState.COLLISION:
                    xOffset = 8;
                    break;
                case EEnemyState.HIT:
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

        ESprites toESprites(EEnemyState state, bool left)
        {
            switch (state)
            {
                case EEnemyState.STAND:
                    return left ? ESprites.LEFT_STAND : ESprites.RIGHT_STAND;
                    break;
                case EEnemyState.DODGE:
                    return left ? ESprites.LEFT_DODGE : ESprites.RIGHT_DODGE;
                    break;
                case EEnemyState.COLLISION:
                    return left ? ESprites.LEFT_COLLISION : ESprites.RIGHT_COLLISION;
                    break;
                case EEnemyState.ATTACK:
                    return left ? ESprites.LEFT_ATTACK : ESprites.RIGHT_ATTACK;
                    break;
                case EEnemyState.HEAL:
                    return left ? ESprites.LEFT_HEAL : ESprites.RIGHT_HEAL;
                    break;
                case EEnemyState.HIT:
                    return left ? ESprites.LEFT_HIT : ESprites.RIGHT_HIT;
                    break;
                case EEnemyState.DEAD:
                    return left ? ESprites.LEFT_DEAD : ESprites.RIGHT_DEAD;
                    break;
                case EEnemyState.WIN:
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

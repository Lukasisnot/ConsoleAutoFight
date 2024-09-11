using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    internal class GameSim
    {
        Random rand = new Random();
        public EGameStates State { get; private set; }
        public GameSim() { }
        void step()
        {
            switch (rand.Next(2))
            {
                case 0:
                    State = EGameStates.LEFT_ATTACKS;
                    break;
                case 1:
                    State = EGameStates.RIGHT_ATTACKS; 
                    break;
                default:
                    State = EGameStates.INTERMISSION; 
                    break;
            }
        }

        void update()
        {
        }
    }
}

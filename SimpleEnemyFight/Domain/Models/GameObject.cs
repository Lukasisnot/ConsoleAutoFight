using System;

namespace SimpleEnemyFight.Domain.Models
{
    public class GameObject
    {
        public static int GameObjectCount { get; protected set; }
        protected Random Rand;

        public GameObject()
        {
            GameObjectCount++;
            Rand = new Random((int)DateTime.Now.Ticks * GameObjectCount);
        }

        ~GameObject()
        {
            GameObjectCount--;
        }
    }
}
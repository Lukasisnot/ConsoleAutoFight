using System;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{
    public class Player : Character
    {
        public ConsoleKeyInfo Input;
        public Entity? RoomEntity;

        public Player(string name, ECharState state, bool isLeft, ConsoleColor color, float hp, float baseDamage, EWeapons weapon) : base(name, state, isLeft, color, hp, baseDamage, weapon)
        {
        }

        public override void Update()
        {
            base.Update();
            if (!Console.KeyAvailable || State != ECharState.STAND) return;
            Input = Console.ReadKey(true);
            
            switch (Input.Key)
            {
                case ConsoleKey.D:
                    Attack(RoomEntity);
                    break;
                case ConsoleKey.E:
                    Heal(EPotions.MEDIUM);
                    break;
                case ConsoleKey.S:
                    Dodge();
                    break;
                default:
                    break;
            }
                
            while(Console.KeyAvailable) 
                Console.ReadKey(false);
        }
    }
}
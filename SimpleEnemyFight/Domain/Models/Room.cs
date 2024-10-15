using System;

namespace SimpleEnemyFight.Domain.Models
{
    internal class Room : GameObject
    {
        // public int DoorNum { get; private set; }
        public static int RoomCount;
        public readonly int X, Y;
        public readonly Enemy? Enemy;
        public readonly Treasure? Treasure;
        public readonly Room?[] SurroundRooms = new Room[4]; // left down right up
        public int SurroundRoomsCount
        {
            get
            {
                int count = 0;
                foreach (Room room in SurroundRooms) if (room != null) count++;
                return count;
            }
        }

        public Room(int x, int y, Enemy? enemy = null, Treasure? treasure = null)
        {
            // DoorNum = doorNum;
            RoomCount++;
            this.X = x;
            this.Y = y;
            
            if (enemy is null && treasure is null)
            {
                if (Rand.Next(2) == 0)
                    enemy = Enemy.Factory.CreateEnemy[Rand.Next(Enemy.Factory.CreateEnemy.Length)];
                else
                    treasure = new Treasure();
            }
            else
            {
                Enemy = enemy;
                Treasure = treasure;
            }
        }
        
        ~Room()
        {
            RoomCount--;
        }

        // public void GenDoors()
        // {
        //     int doorCount = 0;
        //     foreach (bool door in Door) if (door) doorCount++;
        //
        //     for (int i = 0; i < Door.Length; i++)
        //     {
        //         if (!Door[i])
        //         {
        //             Door[i] = true;
        //             doorCount++;
        //         }
        //         if (doorCount == DoorNum) break;
        //     }
        // }
    }
}
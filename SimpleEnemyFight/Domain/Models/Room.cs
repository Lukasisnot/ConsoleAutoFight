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
            RoomCount++;
            X = x;
            Y = y;
            
            if (enemy is null && treasure is null)
            {
                if (Rand.Next(2) == 0)
                    Enemy = Enemy.Factory.CreateEnemy[Rand.Next(Enemy.Factory.CreateEnemy.Length)];
                else
                    Treasure = new Treasure();
            }
            else
            {
                Enemy = enemy;
                Treasure = treasure;
            }

            if (Enemy != null) Enemy.X = 10;
            if (Treasure != null) Treasure.X = 10;
        }
        
        ~Room()
        {
            RoomCount--;
        }

        public Entity GetRoomEntity()
        {
            return (Entity?)Enemy ?? Treasure;
        }
    }
}
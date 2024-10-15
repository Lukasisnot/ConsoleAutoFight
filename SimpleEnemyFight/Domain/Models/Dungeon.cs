using System;

namespace SimpleEnemyFight.Domain.Models
{
    public class Dungeon : GameObject
    {
        public readonly int RoomNum;
        internal readonly Room?[] Rooms;
        internal readonly Room?[,] Grid;

        public Dungeon(int rooms)
        {
            RoomNum = rooms;
            Rooms = new Room?[RoomNum];
            Grid = new Room?[RoomNum, RoomNum];
            GenerateRooms();
            
            for (int i = 0; i < RoomNum; i++)
            {
                for (int j = 0; j < RoomNum; j++)
                {
                    Console.Write(Grid[j, i] != null ? "#" : " ");
                }
                Console.WriteLine();
            }
        }

        private void GenerateRooms()
        {
            int x = Rand.Next(RoomNum);
            int y = Rand.Next(RoomNum);
            Grid[x, y] = new Room(x, y);
            Rooms[0] = Grid[x, y];

            for (int i = 1; i < RoomNum; i++)
            {
                do
                {
                    // Room? temp = Rooms[Rand.Next(Math.Max(0, i - 2), i)];
                    Room? temp = Rooms[Rand.Next(i)];
                    if (temp == null) continue;
                    x = temp.X;
                    y = temp.Y;

                    if (Rand.Next(2) == 0)
                        x += Rand.Next(2) == 0 ? 1 : -1;
                    else
                        y += Rand.Next(2) == 0 ? 1 : -1;

                } while (x < 0 || y < 0 || x > RoomNum - 1 || y > RoomNum - 1 || Grid[x, y] != null);
                
                Grid[x, y] = new Room(x, y);
                Rooms[i] = Grid[x, y];
            }

            foreach (Room room in Rooms)
            {
                if (room.X + 1 < RoomNum) room.SurroundRooms[0] = Grid[room.X + 1, room.Y];
                if (room.Y + 1 < RoomNum) room.SurroundRooms[1] = Grid[room.X, room.Y + 1];
                if (room.X - 1 >= 0) room.SurroundRooms[2] = Grid[room.X - 1, room.Y];
                if (room.Y - 1 >= 0) room.SurroundRooms[3] = Grid[room.X, room.Y - 1];
            }
        }
    }
}
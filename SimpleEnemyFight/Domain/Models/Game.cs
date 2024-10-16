using System.Data;

namespace SimpleEnemyFight.Domain.Models
{
    public class Game
    {
        Player _player;
        Dungeon _dungeon;
        Room _currentRoom;
        public delegate void UpdateDelegate();
        public UpdateDelegate DUpdate;
        
        public Game(Player player, Dungeon dungeon)
        {
            _player = player;
            _dungeon = dungeon;
            DUpdate = _player.Update;

            foreach (Room? room in _dungeon.Grid)
            {
                if (room == null) continue;
                _currentRoom = room;
                break;
            }
            
            EnterRoom(_currentRoom);
        }

        private void EnterRoom(Room room)
        {
            _player.RoomEntity = room.GetRoomEntity();
            if (!_player.RoomEntity.IsAlive) RoomCompleted();
            DUpdate += _player.RoomEntity.Update;
            _player.RoomEntity.DDied = RoomCompleted;
        }

        private void RoomCompleted()
        {
            Renderer.Text(18, 0, "Room Completed");
            Renderer.Text(18, 1, "Good Job!");
        }
    }
}
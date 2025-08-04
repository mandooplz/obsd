using System.Collections.Generic;

namespace Core
{
    public class ChatRoomManager
    {
        private readonly Dictionary<string, List<string>> _rooms = new Dictionary<string, List<string>>();

        public void CreateRoom(string roomName)
        {
            if (!_rooms.ContainsKey(roomName))
            {
                _rooms[roomName] = new List<string>();
            }
        }

        public void JoinRoom(string roomName, string connectionId)
        {
            if (_rooms.ContainsKey(roomName) && !_rooms[roomName].Contains(connectionId))
            {
                _rooms[roomName].Add(connectionId);
            }
        }

        public void LeaveRoom(string roomName, string connectionId)
        {
            if (_rooms.ContainsKey(roomName) && _rooms[roomName].Contains(connectionId))
            {
                _rooms[roomName].Remove(connectionId);
            }
        }

        public List<string> GetUsersInRoom(string roomName)
        {
            return _rooms.ContainsKey(roomName) ? _rooms[roomName] : new List<string>();
        }
    }
}

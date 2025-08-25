using System.Collections.Concurrent;

namespace Core;


// Object
public sealed class ChatRoom
{
    // core

    // state
    public readonly ID Id = new ID();

    // action

    // value
    public struct ID
    {
        public Guid value;

        public ID()
        {
            this.value = Guid.NewGuid();
        }
    }
}


// ObjectManager
internal static class ChatRoomManager
{
    internal static ConcurrentDictionary<ChatRoom.ID, ChatRoom> Container = new();
    internal static void Register(ChatRoom obj)
    {
        Container.TryAdd(obj.Id, obj);
    }
    internal static void Unregister(ChatRoom.ID objectId)
    {
        Container.TryRemove(objectId, out _);
    }
    internal static ChatRoom? Get(ChatRoom.ID objectId)
    {
        return Container.GetValueOrDefault(objectId);
    }
}
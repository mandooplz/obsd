using System.Collections.Concurrent;
namespace Core;


// Object
public sealed class ChatServer
{
    // core
    public ChatServer()
    {
        ChatServerManager.Register(this);
    }
    public void delete()
    {
        ChatServerManager.Unregister(this.Id);
    }


    // state
    public readonly ID Id = new();


    // action


    // value
    public struct ID
    {
        public readonly Guid value;

        public ID()
        {
            this.value = Guid.NewGuid();
        }

        public bool IsExist
        {
            get { return ChatServerManager.Get(this) != null; }
        }

        public ChatServer? Ref
        {
            get { return ChatServerManager.Get(this); }
        }
    }

}


// ObjectManager
internal static class ChatServerManager
{
    internal static ConcurrentDictionary<ChatServer.ID, ChatServer> Container = new();
    internal static void Register(ChatServer obj)
    {
        Container.TryAdd(obj.Id, obj);
    }
    internal static void Unregister(ChatServer.ID objectId)
    {
        Container.TryRemove(objectId, out _);
    }
    internal static ChatServer? Get(ChatServer.ID objectId)
    {
        return Container.GetValueOrDefault(objectId);
    }
}
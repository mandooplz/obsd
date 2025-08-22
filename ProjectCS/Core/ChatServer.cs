namespace Core;

// Object
public sealed class ChatServer
{
    // core


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
            get { return ChatServerManager.Container[this] != null; }
        }

        public ChatServer? Ref
        {
            get { return ChatServerManager.Container[this]; }
        }
    }

}


// ObjectManager
internal sealed class ChatServerManager
{
    internal static Dictionary<ChatServer.ID, ChatServer> Container = new();
    internal static void Register(ChatServer obj)
    {
        Container.Add(obj.Id, obj);
    }
    internal static void Unregister(ChatServer.ID objectId)
    {
        Container.Remove(objectId);
    }
    internal static ChatServer? Get(ChatServer.ID objectId)
    {
        return Container[objectId];
    
    }
}
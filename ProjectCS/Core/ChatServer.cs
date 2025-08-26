using System.Collections.Concurrent;
using System.Diagnostics;
using Values.IDs;

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
    public ID Id { get; } = ID.Random();
    public ServerID target { get; } = ServerID.Random();

    public ConcurrentQueue<string> TextQueue { get; private set; } = [];
    public void AddMessage(string message)
    {
        TextQueue.Enqueue(message);
    }

    public ConcurrentDictionary<MessageID, ChatMessage.ID> Messages { get; private set; } = [];

    // action
    public void InitMessages()
    {
        // mutate
        while (TextQueue.TryDequeue(out var newBody))
        {
            Debug.Assert(newBody is not null);

            // 货肺款 皋矫瘤 积己
            var chatMessageRef = new ChatMessage(newBody);
            this.Messages.TryAdd(chatMessageRef.Target, chatMessageRef.Id);
        }
    }




    // value
    public readonly record struct ID
    {
        // core
        public required Guid Value { get; init; }
        public static ID Random() => new ID { Value = Guid.NewGuid() };

        // operators
        public bool IsExist => ChatServerManager.Get(this) != null;
        public ChatServer? Ref => ChatServerManager.Get(this);
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
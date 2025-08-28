namespace Core;

using System.Collections.Concurrent;
using System.Diagnostics;
using Values.IDs;


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
    public ChatMessage.ID? GetMessage(MessageID message)
    {
        if (Messages.TryGetValue(message, out var value))
            return value;
        else
            return null;
    }
    public void RemoveMessage(MessageID message)
    {
        Messages.TryRemove(message, out _);
    }
    public List<ChatMessage.Data> GetMessageDatas()
    {
        return Messages
            .Values
            .Select(msgId => msgId.Ref())                     // ChatMessage.ID → ChatMessage?
            .OfType<ChatMessage>()                            // null 제거
            .OrderBy(msg => msg.CreatedAt)                    // CreatedAt 기준 정렬
            .Select(msg => new ChatMessage.Data { Body = msg.Body }) // Data 변환
            .ToList();
    }



    // action
    public void InitMessages()
    {
        // mutate
        while (TextQueue.TryDequeue(out var newBody))
        {
            Debug.Assert(newBody is not null);

            // 새로운 메시지 생성
            var chatMessageRef = new ChatMessage(this, newBody);
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
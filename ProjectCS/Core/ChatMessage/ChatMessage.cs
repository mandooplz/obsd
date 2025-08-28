using System.Collections.Concurrent;
using System.Diagnostics;
using Values.IDs;

namespace Core;

// Object 
public sealed class ChatMessage
{
    // core
    public ChatMessage(ChatServer owner, string body)
    {
        this.Owner = owner;
        this.Body = body;

        ChatMessageManager.Register(this);
    }
    internal void delete()
    {
        ChatMessageManager.Unregister(this.Id);
    }


    // state
    public ID Id { get; } = ID.Random();
    public ChatServer? Owner { get; } = null;
    public MessageID Target { get; } = MessageID.Random();

    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public string Body { get; }


    // action
    public void Remove()     
    {
        // capture
        Debug.Assert(Owner is not null);
        var message = this.Target;


        // mutate
        this.delete();
        Owner.RemoveMessage(message);
    }


    // value
    public readonly record struct ID
    {
        // core
        public required Guid value { get; init; }
        public static ID Random() => new() { value = Guid.NewGuid() };

        // operator
        public bool IsExist() => ChatMessageManager.Get(this) != null;
        public ChatMessage? Ref() => ChatMessageManager.Get(this);
    }
    public readonly record struct Data
    {
        // core
        public required string Body { get; init; }
    }
}


// ObjectManager
internal static class ChatMessageManager
{
    internal static ConcurrentDictionary<ChatMessage.ID, ChatMessage> Container = new();
    internal static void Register(ChatMessage obj)
    {
        Container.TryAdd(obj.Id, obj);
    }
    internal static void Unregister(ChatMessage.ID objectId)
    {
        Container.TryRemove(objectId, out _);
    }
    internal static ChatMessage? Get(ChatMessage.ID objectId)
    {
        return Container.GetValueOrDefault(objectId);
    }
}
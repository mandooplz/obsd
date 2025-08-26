using System.Collections.Concurrent;
using Values.IDs;

namespace Core;

// Object 
public sealed class ChatMessage
{
    // core
    public ChatMessage(string body)
    {
        this.Body = body;

        ChatMessageManager.Register(this);
    }
    internal void delete()
    {
        ChatMessageManager.Unregister(this.Id);
    }


    // state
    public ID Id { get; } = ID.Random();
    public MessageID Target { get; } = MessageID.Random();

    public string Body { get; }



    // action
    public void Remove()     
    {
        // mutate
        this.delete();
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
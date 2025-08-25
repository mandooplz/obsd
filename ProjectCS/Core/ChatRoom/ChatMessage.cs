namespace Core;

// Object 
public sealed class ChatMessage
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
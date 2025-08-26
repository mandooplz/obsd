using System;

namespace Values.IDs;

// MessageID
public readonly record struct MessageID
{
    // core
    public required Guid Value { get; init; }
    public static MessageID Random() => new MessageID { Value = Guid.NewGuid() };
}


// ServerID
public readonly record struct ServerID
{
    // core
    public required Guid Value { get; init; }
    public static ServerID Random() => new ServerID { Value = Guid.NewGuid() };
}
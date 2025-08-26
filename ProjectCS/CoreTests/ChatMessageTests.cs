namespace ChatMessage;

using Core;
using Values.IDs;
using System;
using Xunit;
using Xunit.Sdk;


// Tests
public class Remove 
{
    private readonly ChatServer chatServerRef = new();


    [Fact]
    public void DeleteChatMessage()
    {
        // given
        var chatMessageRef = chatServerRef.GetChatMessageForTest();
        var chatMessage = chatMessageRef.Id;
    
        Assert.True(chatMessage.IsExist());
        
        // when
        chatMessageRef.Remove();

        // then
        Assert.False(chatMessage.IsExist());
    }

    [Fact]
    public void RemoveChatMessage_ChatServer()
    {
        // given
        var chatMessageRef = chatServerRef.GetChatMessageForTest();
        var message = chatMessageRef.Target;

        Assert.NotNull(chatServerRef.GetMessage(message));

        // when
        chatMessageRef.Remove();

        // then
        Assert.Null(chatServerRef.GetMessage(message));
    }
}


// Helphers
file static class Helpher
{
    internal static ChatMessage GetChatMessageForTest(this ChatServer chatServerRef)
    {
        // create ChatMessage
        const string testMessage = "Hello, World!";
        chatServerRef.AddMessage(testMessage);
        chatServerRef.InitMessages();

        // return ChatMessage
        var chatMessage = Assert.Single(chatServerRef.Messages).Value;
        Assert.NotNull(chatMessage.Ref());

        return chatMessage.Ref()!;
    }
}

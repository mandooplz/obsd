namespace CoreTests.ChatServer; 

using Core;
using Values.IDs;
using System;


// Tests
public class InitMessages
{
    private readonly ChatServer chatServerRef;
    public InitMessages()
    {
        this.chatServerRef = new ChatServer();
    }

    [Fact]
    public void RemoveTestInQueue()
    {
        // given
        Assert.Empty(chatServerRef.TextQueue);

        var testMessage1 = "Hello, World!";
        var testMessage2 = "This is a test message.";

        chatServerRef.AddMessage(testMessage1);
        chatServerRef.AddMessage(testMessage2);

        Assert.Equal(2, chatServerRef.TextQueue.Count);

        // when
        chatServerRef.InitMessages();

        // then
        Assert.Empty(chatServerRef.TextQueue);
    }

    [Fact]
    public void AddChatMessageInMessages()
    {
        // given
        var testMessage1 = "Hello, World!";
        var testMessage2 = "This is a test message.";

        chatServerRef.AddMessage(testMessage1);
        chatServerRef.AddMessage(testMessage2);

        Assert.Equal(2, chatServerRef.TextQueue.Count);
        Assert.Empty(chatServerRef.Messages);

        // when
        chatServerRef.InitMessages();

        // then
        Assert.Equal(2, chatServerRef.Messages.Count);
    }

    [Fact]
    public void CreateChatMessageTest()
    {
        // given
        var testMessage1 = "Hello, World!";

        chatServerRef.AddMessage(testMessage1);

        Assert.Single(chatServerRef.TextQueue);
        Assert.Empty(chatServerRef.Messages);

        // when
        chatServerRef.InitMessages();

        // then
        var chatMessage = Assert.Single(chatServerRef.Messages).Value;

        Assert.True(chatMessage.IsExist());
    }

    [Fact]
    public void SetBodyTest()
    {
        // given
        var testMessage1 = "BODY_FOR_TEST";

        chatServerRef.AddMessage(testMessage1);

        Assert.Single(chatServerRef.TextQueue);
        Assert.Empty(chatServerRef.Messages);

        // when
        chatServerRef.InitMessages();

        // then
        ChatMessage.ID chatMessage = Assert.Single(chatServerRef.Messages).Value;
        ChatMessage chatMessageRef = chatMessage.Ref()!;

        Assert.Equal(testMessage1, chatMessageRef.Body);
    }
}


namespace CoreTests.ChatServer;

using Core;
using Xunit;


// Tests
public class Init
{
    
    private readonly ChatServer chatServerRef;
    public Init()
    {
        this.chatServerRef = new ChatServer();
    }


    [Fact]
    public void CreateChatServer()
    {
        // then
        var isExist = chatServerRef.Id.IsExist;

        Assert.True(isExist);
    }
}

public class Delete
{
    private readonly ChatServer chatServerRef;
    public Delete()
    {
        this.chatServerRef = new ChatServer();
    }

    [Fact]
    public void DeleteChatServer()
    {
        // given
        Assert.True(chatServerRef.Id.IsExist);

        // when
        chatServerRef.delete();

        // then
        Assert.False(chatServerRef.Id.IsExist);
    }
}


using Core;

namespace CoreTests;

public class UnitTest1
{
    [Fact]
    public void CreateRoom_ShouldCreateRoom()
    {
        // Arrange
        var chatRoomManager = new ChatRoomManager();

        // Act
        chatRoomManager.CreateRoom("test-room");

        // Assert
        Assert.NotNull(chatRoomManager.GetUsersInRoom("test-room"));
    }

    [Fact]
    public void JoinRoom_ShouldAddUserToRoom()
    {
        // Arrange
        var chatRoomManager = new ChatRoomManager();
        chatRoomManager.CreateRoom("test-room");

        // Act
        chatRoomManager.JoinRoom("test-room", "test-user");

        // Assert
        Assert.Contains("test-user", chatRoomManager.GetUsersInRoom("test-room"));
    }

    [Fact]
    public void LeaveRoom_ShouldRemoveUserFromRoom()
    {
        // Arrange
        var chatRoomManager = new ChatRoomManager();
        chatRoomManager.CreateRoom("test-room");
        chatRoomManager.JoinRoom("test-room", "test-user");

        // Act
        chatRoomManager.LeaveRoom("test-room", "test-user");

        // Assert
        Assert.DoesNotContain("test-user", chatRoomManager.GetUsersInRoom("test-room"));
    }
}


namespace App.Hubs;
using Microsoft.AspNetCore.SignalR;

public class ChatHub: Hub
{
    
    // 기본 루틴
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
    // 클라이언트가 이 메서드를 호출하여 특정 그룹을 구독합니다.
    public async Task SubscribeToGroup(string groupName)
    {
        // Context.ConnectionId: 현재 요청을 보낸 클라이언트의 고유 연결 ID
        // Groups.AddToGroupAsync: 지정된 연결을 그룹에 추가합니다.
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    
        // (선택 사항) 구독 완료를 클라이언트에게 다시 알려줄 수 있습니다.
        await Clients.Caller.SendAsync("SubscriptionConfirmed", $"성공적으로 '{groupName}' 그룹을 구독했습니다.");
    }
    
    // 클라이언트가 이 메서드를 호출하여 그룹 구독을 해지합니다.
    public async Task UnsubscribeFromGroup(string groupName)
    {
        // Groups.RemoveFromGroupAsync: 지정된 연결을 그룹에서 제거합니다.
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }
}
using App.Hubs;
namespace App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using App;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    // dependency
    private readonly IHubContext<ChatHub> _hubContext;


    // endpoint
    [HttpPost("send-to-group")]
    public async Task<IActionResult> SendMessageGroup([FromBody] MessagePayload payload)
    {
        await _hubContext.Clients
            .Group(payload.GroupName)
            .SendAsync("ReceiveMessage", payload.GroupName, payload.Message);

        return Ok($"'{payload.GroupName}' 그룹에 메시지를 성공적으로 전송했습니다.");
    }
    
    
    // value
    public record MessagePayload(string GroupName, string Message);
}
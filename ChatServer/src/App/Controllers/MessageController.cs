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
    // 생성자를 통해 IHubContext를 주입받습니다.
    public MessageController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }


    // endpoint
    // POST 메서드로 요청이 들어오면 
    [HttpPost("send-to-group")]
    public async Task<IActionResult> SendMessageGroup([FromBody] MessagePayload payload)
    {
        await _hubContext.Clients
            .Group(payload.GroupName)
            .SendAsync("ReceiveMessage", payload.User, payload.Message);

        return Ok($"'{payload.GroupName}' 그룹에 메시지를 성공적으로 전송했습니다.");
    }
    
    
    // value
    public record MessagePayload(string GroupName, string User, string Message);
}
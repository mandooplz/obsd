using App;
using Core;
using Microsoft.AspNetCore.SignalR;


// app 생성
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton<ChatRoomManager>();

var app = builder.Build();


// HTTP 라우팅
app.MapGet("/", () => "Hello World!");


// signalR 라우팅
app.MapHub<ChatHub>("/chathub");


// 시작
app.Run();
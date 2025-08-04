using App;
using Microsoft.AspNetCore.SignalR;


// app 생성
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();


// HTTP 라우팅
app.MapGet("/", () => "Hello World!");


// signalR 라우팅
app.MapHub<ChatHub>("/chathub");


// 시작
app.Run();
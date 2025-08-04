"use strict";

// SignalR 연결
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

// ReceiveMessage 메서드 등록
connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} says ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

connection.onclose(async () => {
   console.log("SignalR.Hub connection이 종료됩니다."); 
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    
    // connection을 통해 메시지를 보낸다. 
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("subscribeButton").addEventListener("click", async () => {
    // groupName을 가져온다. 
    const groupName = document.getElementById("groupNameInput").value;
    if (groupName) {
        try {
            // 4. 서버의 Hub 메서드 호출 (구독 요청)
            await connection.invoke("SubscribeToGroup", groupName);
            console.log(`'${groupName}' 그룹 구독 요청을 보냈습니다.`);
        } catch (err) {
            console.error(err);
        }
    }
});
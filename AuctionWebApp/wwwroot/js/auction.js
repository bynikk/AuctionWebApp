"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/auction").build();
var itemId = null;
//Disable the send button until connection is established.
document.getElementById("bitButton").disabled = true;

connection.on("ReceiveBit", function (bit, id) {
    if (id != document.getElementById("itemId").value) return;
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    
    li.textContent = `bit ${bit}$ for item#${id}`;
});

connection.on("ReceiveСurrPrice", function (currPrice, id) {
    if (id != document.getElementById("itemId").value) return;
    document.getElementById("price").value = currPrice;
});

connection.on("ReceiveNewTime", function (dateTime, id) {
    // изменение html кода (отправка данных в функцию, которая высчитывает и отображает время
});

connection.start().then(function () {
    document.getElementById("bitButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("bitButton").addEventListener("click", function (event) {
    var bit = document.getElementById("messageInput").value;
    //if (itemId == null) {
    //    itemId = document.getElementById("itemId").value;
    //}
    var itemId = document.getElementById("itemId").value;
    connection.invoke("Bit", bit, itemId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
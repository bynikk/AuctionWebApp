"use strict";
// три свойства занестия на страницу, создать методы для их обновления
var connection = new signalR.HubConnectionBuilder().withUrl("/auction").build();
var itemId = null;
//Disable the send button until connection is established.
document.getElementById("bitButton").disabled = true;

connection.on("ReceiveCurrPrice", function (currPrice, id) {
    // изменение html кода (отправка данных в функцию, которая высчитывает и отображает время
    if (id != document.getElementById("itemId").value) return;

    document.getElementById("price").innerText = currPrice;

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `bit ${bit}$ for item#${id}`;
});

connection.on("ReceiveBitTime", function (bitTime, id) {
    if (id != document.getElementById("itemId").value) return;
    var lastBitTime = document.getElementById("lastBitTime");
    lastBitTime.setAttribute('value', bitTime)
    setRemaningLiveTime(bitTime);
});
connection.on("ReceiveAuctionLiveData", function (id) {
    if (id != document.getElementById("itemId").value) return;
    var onlive = document.getElementById("onLive")
    var onwait = document.getElementById("onWait")
    onlive.value = true;
    onwait.value = false;
});

connection.start().then(function () {
    document.getElementById("bitButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("bitButton").addEventListener("click", function (event) {
    if (!!(document.getElementById("onWait").nodeValue)) return;
    var bit = document.getElementById("bitInput").value;
    var itemId = document.getElementById("itemId").value;
    connection.invoke("Bit", bit, itemId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
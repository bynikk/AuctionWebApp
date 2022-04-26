"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/auction").build();
var itemId = null;
document.getElementById("bitButton").disabled = true;

connection.on("ReceiveBitData", function (currPrice, bitTime, owner, id) {
    if (id != document.getElementById("itemId").value) return;

    document.getElementById("price").innerText = currPrice;

    var lastBitTime = document.getElementById("lastBitTime");
    lastBitTime.setAttribute('value', bitTime)
    // !    
    setRemaningLiveTime(bitTime);

    var lastBitTime = document.getElementById("owner");
    lastBitTime.innerText = owner

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `bit ${currPrice}$ for item#${id}`;
});

connection.on("ReceiveAuctionLiveData", function (id) {
    if (id != document.getElementById("itemId").value) return;
    var onlive = document.getElementById("onLive")
    var onwait = document.getElementById("onWait")
    onlive.value = true;
    onwait.value = false;
});

connection.on("ReceiveAuctionEndData", function (id) {
    if (id != document.getElementById("itemId").value) return;
    var onlive = document.getElementById("onLive")
    var onwait = document.getElementById("onWait")
    onlive.value = false;
    onwait.value = false;

    document.getElementById("bitButton").remove()
    document.getElementById("bitInput").remove()
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

function StatusRequest(id) {
    connection.invoke("StatusRequest", id).catch(function (err) {
        return console.error(err.toString());
    });
}

var timeInterval;

function getTimeRemaining(endtime) {
    var t = Date.parse(endtime) - Date.parse(new Date());
    var seconds = Math.floor((t / 1000) % 60);
    var minutes = Math.floor((t / 1000 / 60) % 60);
    var hours = Math.floor((t / (1000 * 60 * 60)) % 24);


    return {
        'total': t,
        'hours': hours,
        'minutes': minutes,
        'seconds': seconds
    };
}

function initializeClock(id, endtime) {
    var clock = document.getElementById(id);
    var hoursSpan = clock.querySelector('.hours');
    var minutesSpan = clock.querySelector('.minutes');
    var secondsSpan = clock.querySelector('.seconds');

    function updateClock() {
        var t = getTimeRemaining(endtime);

        hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
        minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
        secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

        if (t.total <= 0) {
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            let id = document.getElementById("itemId").value;
            StatusRequest(id)
        }
    }

    updateClock();
    timeInterval = setInterval(updateClock, 1000);
}

function setRemaningWaitTime(date) {
    var now = new Date()
    var start = new Date(date)
    if (start >= now) {
        const button = document.querySelector('#bitButton')
        button.setAttribute('disabled', false)

        clearInterval(timeInterval);
        initializeClock('countdown', new Date(start))
    } else {
        const button = document.querySelector('#bitButton')
        button.removeAttribute('disabled')
    }
}

function setRemaningLiveTime(date) {
    var now = new Date()
    var start = new Date(date)
    if (start >= now) {
        clearInterval(timeInterval);
        initializeClock('countdown', new Date(start))
    } else {
        const button = document.querySelector('#bitButton')
        button.setAttribute('disabled', true)
    }
}

function setTime(onWait, OnLive) {
    var onWaitBool = !!onWait;
    var OnLiveBool = !!OnLive;
    if (onWait) {
        setRemaningWaitTime(new Date(document.getElementById("startTime").value))
    }
    else if (OnLive) {
        setRemaningLiveTime(new Date(document.getElementById("lastBitTime").value));
    }
    else {
        document.getElementById("bitButton").remove()
        document.getElementById("countdown").remove()
        document.getElementById("bitInput").remove()
    }
}
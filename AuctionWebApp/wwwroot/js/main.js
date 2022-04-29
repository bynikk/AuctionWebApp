"use strict";
let connection = new signalR.HubConnectionBuilder().withUrl("/main").build();

function start(arrId) {
    connection.start().then(function () {
        arrId.forEach(id => connection.invoke("ItemStatusRequest", id).catch(function (err) {
            return console.error(err.toString());
        }))
        event.preventDefault();
    }).catch(function (err) {
        return console.error(err.toString());
    });
};

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

function ItemStatusUpdateRequest(id) {
    connection.invoke("ItemStatusUpdateRequest", id).catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceiveItemTimer", function (id, status, endtime) {
    var statusid = "status"+id
    document.getElementById(statusid).innerText = status;
    if (status != "Ended" && status != "Waiting first bid") {
        initializeItemClock(id, endtime)
    }
});

function initializeItemClock(id, endtime) {
    var clock = document.getElementById("clock" + id);
    var hoursSpan = clock.querySelector('.hours');
    var minutesSpan = clock.querySelector('.minutes');
    var secondsSpan = clock.querySelector('.seconds');

    function updateClock() {
        var t = getTimeRemaining(endtime);

        hoursSpan.innerHTML = ('0' + t.hours).slice(-2);
        minutesSpan.innerHTML = ('0' + t.minutes).slice(-2);
        secondsSpan.innerHTML = ('0' + t.seconds).slice(-2);

        if (t.total <= 0) {
            ItemStatusUpdateRequest(id.toString());
        }
    }

    updateClock();
    timeInterval = setInterval(updateClock, 1000);
}
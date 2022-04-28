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

function initializeItemClock(id, endtime) {
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
        }
    }

    updateClock();
    timeInterval = setInterval(updateClock, 1000);
}
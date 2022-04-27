import { StatusRequest as auc} from './auction';

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
            clearInterval(timeInterval);
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            let id = document.getElementById("itemId").value;
            auc(id)
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

function setTime() {
    var onWaitBool = !!onWait;
    var onWait = document.getElementById("onWait").getAttribute("value");
    var onLive = document.getElementById("onLive").getAttribute("value");
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
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
            clearInterval(timeinterval);
        }
    }

    updateClock();
    var timeinterval = setInterval(updateClock, 1000);

}
function setRemaningWaitTime(date) {
    var now = new Date()
    var start = new Date(date)
    if (start >= now) {
        const button = document.querySelector('#bitButton')
        button.setAttribute('disabled', true)
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
        initializeClock('countdown', new Date(start))
    } else {
        const button = document.querySelector('#bitButton')
        button.setAttribute('disabled', true)
    }
}

function setTime(onWait, OnLive, date) {
    if (onWait) {
        setRemaningWaitTime(date)
    }
    else if (OnLive) {
        minutesSpan.innerHTML = "end";
    }
    else {
        // not OnWait, not OnLive =>
        setRemaningLiveTime(new Date())
    }
}
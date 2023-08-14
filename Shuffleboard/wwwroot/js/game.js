$(function setPlayers() {
    try {
        $("#red-player").val(JSON.parse(localStorage.getItem("red_player"))["Nick"]);
        $("#blue-player").val(JSON.parse(localStorage.getItem("blue_player"))["Nick"]);
    } catch {
        /* Redirect to player select page if one or both players are missing */
        alert(
            "NOOOO! YOU CAN'T JUST START A MATCH WITHOUT BOTH PLAYERS!") /* TODO: Add visual popup for user */
        var url = '@Url.Action("Index", "Game")';
        window.location.href = url;
    }
});
/* Current time in datetime2(3) format w/ currect timezone */
var startTime = moment().toISOString(true).replace('T', ' ').replace('Z', ' ').replace('+02:00', '');

$("input#submit").on("click", function () {
    /* Match cannot end with a stalemate */
    if (rdSc.value === blSc.value) {
        return false; /* TODO: Add visual message for user */
    }

    localStorage.red_score = rdSc.value;
    localStorage.blue_score = blSc.value;

    /* If this is added before the if statement or set earlier, it will result in a successful POST */
    $("input#start-time").val(startTime);
});

/* Red and blue score input fields */
const rdSc = document.getElementById("red-score"),
    blSc = document.getElementById("blue-score");
/* Red and blue + and - buttons */
const rdAdd = document.getElementById("redAddscore"),
    rdRm = document.getElementById("redRemovescore"),
    blAdd = document.getElementById("blueAddscore"),
    blRm = document.getElementById("blue-remove-score");
/* Min and max score */
var minSc = 0,
    maxSc = 99;

$("button#red-add-score").on("click", function () {
    if (rdSc.value < maxSc) {
        rdSc.value++;
    } else {
        rdAdd.disabled = true;
        return;
    }
});

$("button#red-remove-score").on("click", function () {
    if (rdSc.value > minSc) {
        rdSc.value--;
    } else {
        rdSc.disabled = true;
        return;
    }
});

$("button#blue-add-score").on("click", function () {
    if (blSc.value < maxSc) {
        blSc.value++;
    } else {
        return;
    }
});

$("button#blue-remove-score").on("click", function () {
    if (blSc.value > minSc) {
        blSc.value--;
    } else {
        return;
    }
});

const timer = document.getElementById("stopwatch");

var min = 0;
var sec = 0;

function timerCycle() {
    sec = parseInt(sec);
    min = parseInt(min);

    sec = sec + 1;

    if (sec === 60) {
        min = min + 1;
        sec = 0;
    }

    if (sec < 10 || sec === 0) {
        sec = '0' + sec;
    }
    if (min < 10 || min === 0) {
        min = '0' + min;
    }

    timer.innerHTML = min + ':' + sec;

    setTimeout("timerCycle()", 1000);
}
timerCycle();
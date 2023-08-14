// Display the winner
if (parseInt(localStorage.red_score) > parseInt(localStorage.blue_score)) {
	$("span#winner").text(JSON.parse(localStorage.red_player)["Nick"]);
} else if (parseInt(localStorage.red_score) < parseInt(localStorage.blue_score)) {
	$("span#winner").text(JSON.parse(localStorage.blue_player)["Nick"]);
}

/* Cleared so that the player cannot go back to Game/Play and submit another game w/ same start time, matchId, etc. */
localStorage.clear();

const countdown = document.getElementById("countdown");
var sec = 10;

/**
 * Redirect the user to the home page after the time specified with `sec`
 */
function timerCycle() {
	if (sec >= 0) {
		countdown.innerHTML = sec;
		sec = sec - 1;

		setTimeout("timerCycle()", 1000);
	} else {
		window.location.href = "/Home/Index";
	}
}
/* Redirects user to Home if there is activity */
function activity() {
    window.location.href = '/Home/Index';
}

/* An array of DOM events that should be interpreted as user activity. */
var activityEvents = [
    'mousedown', 'keydown',
    'scroll', 'touchstart'
];

/* Add these events to the document. Register the activity function as the listener parameter. */
activityEvents.forEach(function (eventName) {
    document.addEventListener(eventName, activity, true);
});

timerCycle();

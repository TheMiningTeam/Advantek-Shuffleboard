function activityWatcher() {
    var secondsSinceLastActivity = 0;
    var maxInactivity = 180; /* In seconds */

    setInterval(function () {
        secondsSinceLastActivity++;
        if (secondsSinceLastActivity == maxInactivity) {
            /* Clear the background so that a new random one can be chosen in js/site.js */
            sessionStorage.removeItem("background");
            var url = '/Home/AFK';
            window.location.href = url;
        }
    }, 1000);

    /* The function that will be called whenever a user is active */
    function activity() {
        /* Reset the secondsSinceLastActivity variable back to 0 */
        secondsSinceLastActivity = 0;
    }

    /* An array of DOM events that should be interpreted as user activity. */
    var activityEvents = [
        'mousedown', 'mousemove', 'keydown',
        'scroll', 'touchstart'
    ];

    /* Add these events to the document. Register the activity function as the listener parameter. */
    activityEvents.forEach(function (eventName) {
        document.addEventListener(eventName, activity, true);
    });
}

activityWatcher();
function check() {
	/* Shuffleboard cannot result in a stalemate. This is also checked on the previous page. */
	if (localStorage.red_score == localStorage.blue_score) {
		$("#winner").text("The match resulted in a stalemate. How is that even possible? Try again.");
		/* Redirects to Game/Play after 5 seconds */
		setTimeout(function () {
			var url = '@Url.Action("Play", "Game")';
			window.location.href = url;
		}, 5000);
	} else if (localStorage.red_score > parseInt(localStorage.blue_score)) {
		var red_won = true;
	} else if (localStorage.red_score < parseInt(localStorage.blue_score)) {
		var blue_won = true;
	}

	// What even? `red` or `blue` is never even defined...
	if (!localStorage.red) {
		submitForms(localStorage.red_player, "RED", localStorage.red_score, red_won);
	} else if (!localStorage.blue) {
		submitForms(localStorage.blue_player, "BLUE", localStorage.blue_score, blue_won);
	}
}
// Submit scores for both players.
/* Before page is loaded, form is filled out for red then submitted, page will reload and do the same for blue. As a result, the page is refreshed twice, but content is not loaded */
function submitForms(player, color, score = 0, won = false) {
	if (!localStorage.red_submit) {
		$("#PlayerId").val(JSON.parse(player)["Id"]);
		$("#Score").val(score);
		$("#Color").val(color);
		$("#Won").prop('checked', won);
		localStorage.setItem(color.toLowerCase(), true)
		document.forms["player-frm"].submit(false);
	}
}

check();
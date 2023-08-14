// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if (sessionStorage.background == undefined) {
    var images = ['1.jpg', '2.jpg', '3.jpg', '4.jpg', 'no_1.jpg', 'no_2.jpg', 'no_3.jpg'];
    sessionStorage.background = images[Math.floor(Math.random() * images.length)]
}
//$('body').css({ 'background-image': 'url(images/backgrounds/' + sessionStorage.background + ')' });
//$("body").css({ "background-image": "linear-gradient(rgba(255, 255, 255, 0.5), rgba(255, 255, 255, 0.5)), url(" + sessionStorage.background + ");" });
$("body").css({
    'background-image': '-moz-linear-gradient(top,rgba(0,0,0,0)80%,rgba(0,0,0,0.65)100%), url("/images/backgrounds/' + sessionStorage.background + '")'
}).css({
    'background-image': '-webkit-gradient(linear,left top,left bottom,color-stop(80%,rgba(0,0,0,0)),color-stop(100%,rgba(0,0,0,0.65))), url("/images/backgrounds/' + sessionStorage.background + '")'
}).css({
    'background-image': '-webkit-linear-gradient(top,rgba(0,0,0,0)80%,rgba(0,0,0,0.65)100%), url("/images/backgrounds/' + sessionStorage.background + '")'
}).css({
    'background-image': '-o-linear-gradient(top,rgba(0,0,0,0)80%,rgba(0,0,0,0.65)100%), url("/images/backgrounds/' + sessionStorage.background + '")'
}).css({
    'background-image': 'linear-gradient(to bottom,rgba(0,0,0,0)80%,rgba(0,0,0,0.65)100%), url("/images/backgrounds/' + sessionStorage.background + '")'
});
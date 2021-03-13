// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function SetEndIndex(endIndexValue) {
    $.ajax({
        url: "Index/OnGetShowMoreAsync",
        type: "get", //send it through get method
        data: { "endIndexValue": endIndexValue },
        success: function (data) {
            alert(value)
        },
        error: function (xhr) {
        }
    });
}

function clickCounter() {
    if (typeof (Storage) !== "undefined") {
        if (sessionStorage.clickcount) {
            sessionStorage.clickcount = Number(sessionStorage.clickcount) + 1;
            return 
        } else {
            sessionStorage.clickcount = 1;
        }
        document.getElementById("result").innerHTML = "You have clicked the button " + sessionStorage.clickcount + " time(s) in this session.";
    } else {
        document.getElementById("result").innerHTML = "Sorry, your browser does not support web storage...";
    }
}
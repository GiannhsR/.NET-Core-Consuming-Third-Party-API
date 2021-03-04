// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function SetEndIndex(value) {
    $.ajax({
        url: "/Index/SetEndIndex",
        type: "get", //send it through get method
        data: { "value": value },
        success: function (data) {
            alert(value)
        },
        error: function (xhr) {
        }
    });
}
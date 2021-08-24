var connection = new signalR.HubConnectionBuilder()
    .withUrl("/notify")
    .build();

connection.on("NotifyOrders", (message) => {
    customAlert(message);
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});

function customAlert(msg) {
    $("#messageContainer").html(msg);
    $("#messageContainer").slideDown(2000);
    setTimeout('$("#messageContainer").slideUp(2000);', 5000);
}
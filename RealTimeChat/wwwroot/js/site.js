//Create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/user").build();

//Methods that is invoked by the hub
connection.on("DisplaySearchResults", (values) => {
    $('#results').empty(); 
    values.forEach(function (value) {
        // Append each string to the #results div
        
        $('#results').append('<div><a href="/User/GetUser/' + value + '"><b>' + value + '</b></a></div><br>');



    });

})

//Methods to invoke method in hub
$('#fir').on('input', function () {
   
    var query = $(this).val();
    connection.send("DynamicSearch", query);
});

function fullfiled() { console.log("Success"); }
function notfullfiled() { console.log("NotSucccess"); }
//Start Connection
connection.start().then(fullfiled, notfullfiled)
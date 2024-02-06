//Create connection
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/user").build();

//Methods that is invoked by the hub
connection.on("DisplaySearchResults", (values) => {
    $('#results').empty(); 
    values.forEach(function (value) {
        // Append each string to the #results div
<<<<<<< HEAD
        console.log("Value Before:" + value);
=======
        
>>>>>>> 122d20511a43c7b31552552180044f743114b4bb
        $('#results').append('<div><a href="/User/GetUser/' + value + '"><b>' + value + '</b></a></div><br>');



<<<<<<< HEAD

    });
  
=======
    });
>>>>>>> 122d20511a43c7b31552552180044f743114b4bb

})

//Methods to invoke method in hub
$('#fir').on('input', function () {
   
    var query = $(this).val();
    connection.send("DynamicSearch", query);
});

<<<<<<< HEAD

=======
>>>>>>> 122d20511a43c7b31552552180044f743114b4bb
function fullfiled() { console.log("Success"); }
function notfullfiled() { console.log("NotSucccess"); }
//Start Connection
connection.start().then(fullfiled, notfullfiled)
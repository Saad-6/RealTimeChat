//Create connection
var connectionMessage = new signalR.HubConnectionBuilder().withUrl("/hubs/message").build();

//Methods that is invoked by the hub




//Methods to invoke method in hub
function startChat(query) {
    
    connectionMessage.send('FetchChat', query);
        
}


//Start the connection
function fullfiled() { console.log("Success"); }
function notfullfiled() { console.log("NotSucccess"); }
//Start Connection
connectionMessage.start().then(fullfiled, notfullfiled);
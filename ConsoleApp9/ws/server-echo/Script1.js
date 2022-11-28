const WebSocket = require('ws');

const Port1 = 5000;
const turn = 3;

const wsServer = new WebSocket.Server({
    port: Port1
});

wsServer.on('connection', function (socket) {

    console.log("New Connection");
    
    socket.on('message', function (msg) {


        wsServer.clients.forEach(function (client) {
            
            client.send(msg);
            console.log("move received" + msg);

        });



    });
});



console.log((new Date()) + " server is listening on port " + Port1);
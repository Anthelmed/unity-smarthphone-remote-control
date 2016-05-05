var http = require('http');
var fs = require('fs');

var server = http.createServer(function(req, res) {
    fs.readFile('./index.html', 'utf-8', function(error, content) {
        res.writeHead(200, {"Content-Type": "text/html"});
        res.end(content);
    });
});

var io = require('socket.io').listen(server, { log: false });
server.listen(8080);

io.on('connection', function (socket) {

    socket.on('deviceY', function (data) {
        socket.broadcast.emit('deviceY', data);
    });

    socket.on('deviceX', function (data) {
        socket.broadcast.emit('deviceX', data);
    });

    socket.on('carSpeed', function (data) {
        socket.broadcast.emit('carSpeed', data);
    });

    socket.on('disconnect', function () {
        console.log('disconnect.');
    });
});

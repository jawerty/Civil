var express = require('express');
var app = express();
var path = require('path');
var logger = require('morgan');
var methodOverride = require('method-override');
var session = require('express-session');
var bodyParser = require('body-parser');
var multer = require('multer');
var errorHandler = require('errorhandler');
var mongoose = require("mongoose");
var schema = require("./schema");

/* Importing routing functions 8 */
var Movements = require("./routes/movements");
var Users = require("./routes/users");

var env = app.settings.env; 

app.use(function (req, res, next) {

	res.set({
	  'Content-Type': 'application/json',
	});

	next()
});

app.set('port', process.env.PORT || 3000);
app.set('views', path.join(__dirname, 'views'));
app.use(logger('dev'));
app.use(methodOverride());
app.use(session({ resave: true,
                  saveUninitialized: true,
                  secret: 'ellogovna' }));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(multer());
app.use(express.static(path.join(__dirname, 'public')));

if (env == "development") {
	app.use(errorHandler());
}

/* Movement discovery */
app.get('/movements', Movements.movementsGET);

/* Returning data from movements and users */
app.get('/movements/:id', Movements.movementsIdGET)
app.get('/users/:id', Users.usersIdGET)

/* Creating movement and users */
app.post('/movements', Movements.movementsPOST)
app.post('/users', Users.usersPOST)

/* User logging in and authentication */
app.post('/usersAuth', Users.usersAuth)

/* Updating movement and user information */
app.post('/movements/:id', Movements.movementsIdPOST)
app.post('/users/:id', Users.usersIdPOST)

/* Deleting movements and users */
app.delete('/movements/:id', Movements.movementsIdDELETE)
app.delete('/users/:id', Users.usersIdDELETE)

/* Creating announcements and events */
app.post('/movements/:id/announcements', Movements.movementsIdAnnuncementsPOST);
app.post('/movements/:id/events', Movements.movementsIdEventsPOST);

/* logout */
app.post('/usersLogout', Users.usersLogout);

/* yays and nays */
app.put('/movements/:id/yay', Movements.movementsIdYayPUT);
app.put('/movements/:id/nay', Movements.movementsIdNayPUT);
app.put('/movements/:id/join', Movements.movementsIdJoinPUT);

var server = app.listen(3000, function () {
  var host = server.address().address;
  var port = server.address().port;
  console.log('Civil server at http://%s:%s', host, port);
});
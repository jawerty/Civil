var mongoose = require("mongoose");
var users = mongoose.model('users');
var movements = mongoose.model('movements');

exports.movementsPOST = function(req, res, next) {
  	var data = req.body;

  	console.log(data);
  	res.end();
}

exports.movementsIdPOST = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}

exports.movementsIdGET = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}

exports.movementsIdDELETE = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}
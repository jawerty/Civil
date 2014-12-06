var mongoose = require("mongoose");
var users = mongoose.model('user');
var movements = mongoose.model('movement');

function sendERR(err, res) {
	res.send("{ \"message\": \""+err+"\" }");
}

exports.movementsPOST = function(req, res, next) {
  	var data = req.body;
  	console.log(data);

  	try {
  		var newMovement = new movements({
			founder: data.founder,
			title: data.title,
			description: data.description
		});  

		newMovement.save();

		res.send("{ \"message\": \"Movement created\" }");

  	} catch (err) {
  		sendERR(err, res);
  	}
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

exports.movementsIdAnnuncementsPOST = function(req, res) {
	var movementId = req.params.id;

}

exports.movementsIdEventsPOST = function(req, res) {
	var movementId = req.params.id;
	
}
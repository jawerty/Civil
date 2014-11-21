var mongoose = require("mongoose");
var users = mongoose.model('users');
var movements = mongoose.model('movements');

exports.movementsPOST = function(req, res, next) {
  	var data = req.body;

  	try {
  		var newMovement = new users({
			founder: data.founder,
			title: data.title,
			description: data.description
		});  
  	} catch (err) {
  		sendERR(err);
  	}
			

	newMovement.save();

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

exports.movementsIdAnnuncementsPOST = function(req, res) {
	var movementId = req.params.id;

}

exports.movementsIdEventsPOST = function(req, res) {
	var movementId = req.params.id;
	
}
exports.usersPOST = function(req, res) {
  	res.send("movements");
}

exports.usersIdPOST = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}

exports.usersIdGET = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}

exports.usersIdDELETE = function(req, res) {
	var movementId = req.params.id;
  	res.send(movementId);
}
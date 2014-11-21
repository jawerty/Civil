exports.movementsPOST = function(req, res) {
  	res.send("movements");
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
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
			description: data.description,
			yays: 0,
			nays: 0,
			tags: data.tags
		});  

		newMovement.save();

		res.send("{ \"message\": \"Movement created\", \"id\": \""+newMovement._id+"\" }");

  	} catch (err) {
  		sendERR(err, res);
  	}
}

exports.movementsIdPOST = function(req, res) {
	var movementId = req.params.id;
  	var data = req.body;

    try {
		movements.findOne({_id: movementId}, function(err, doc) {
			if (err) console.log(err);
		  	if (doc) {

		  		for (var key in data) {
		  			if (key != "_id" || key != "dateCreated" || key != "location" || key != "founder") {
		  				if (data[key] != null) {
			  				doc[key] = data[key];
			  			} 
		  			}
		  			
		  		}

		  		doc.save(function(err) {
		  			if (err) {
		  				sendERR("Could not save the data", res)
		  			} else {
		  				res.send("{ \"message\": \"Movement documents updated successfully\" }");
		  			}
		  		});

		  	} else {
		  		sendERR("Movement not found", res)
		  	}
		});
	} catch (err) {
		sendERR(err, res)
	}
}

exports.movementsIdGET = function(req, res) {
	var movementId = req.params.id;

	console.log(movementId)
	try {
		movements.findOne({_id: movementId}, function(err, doc) {
			if (err) console.log(err);
	  		if (doc) {
	  			res.send("{ \"message\": \"Movement found\", \"document\": "+JSON.stringify(doc)+" }");
	  		} else {
	  			res.send("{ \"message\": \"Movement not found\" }");
	  		};
	  	});
	} catch (err) {
		sendERR(err, res)
	}
}

exports.movementsIdDELETE = function(req, res) {
	var movementId = req.params.id;
	try {
		movements.findOne({_id: movementId}).remove();
		res.send("{ \"message\": \"Movement deleted\" }");
	} catch (err) {
		sendERR(err, res)
	}
}

exports.movementsIdAnnuncementsPOST = function(req, res) {
	var movementId = req.params.id;

}

exports.movementsIdEventsPOST = function(req, res) {
	var movementId = req.params.id;
	
}

exports.movementsIdYayPUT = function(req, res) {
	var movementId = req.params.id;

	movements.findOneAndUpdate({ _id: movementId }, { $inc: { yays: 1 } }).exec(function(err, response) { 
		if (err) 
			sendERR(err, res);
		else 
			console.log(response); 
			res.send("{ \"message\": \""+movementId+" received a yay\" }");
	});

}

exports.movementsIdNayPUT = function(req, res) {
	var movementId = req.params.id;
	movements.findOneAndUpdate({ _id: movementId }, { $inc: { nays: 1 } }).exec(function(err, response) { 
		if (err) 
			sendERR(err, res);
		else 
			console.log(response); 
			res.send("{ \"message\": \""+movementId+" received a nay\" }");

	});
}
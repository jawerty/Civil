var mongoose = require("mongoose");
var users = mongoose.model('user');
var movements = mongoose.model('movement');

function sendERR(err, res) {
	res.send("{ \"message\": \""+err+"\" }");
}	
	
exports.movementsGET = function(req, res, next) {
	function discover(type, location, skip, distance) {
		skip = skip * 20;

		if (type == "pop") {
			sort = {yays: -1, nays: 1};

		} else if (type == "hot") {
			sort = {};

		} else if (type == "new") {
			sort = {dateCreated: -1};
		} else {
			sort = {yays: -1, nays: 1};
		}

		console.log(sort)

		if (location == "all") {
			if (type == "hot") {
				movements.aggregate([
				  {
					  $match: { 
				        	yays: { $gt: 0 }
				      }
			  	  },	
				  { 
				    $project: {
				    	yays: "$yays",
				    	nays: "$nays",
				    	difference: {$subtract: ["$yays", "$nays"] },
					   	net: {
					   		$cond: [ 
						      	{$lt:[{ $subtract: ["$yays", "$nays"] },0]},
								{$subtract: [0,  { $subtract: ["$yays", "$nays"] }] }, 
								{ $subtract: ["$yays", "$nays"] }
							] 
					  	} 	
				    }
				  },  
				  { 
				    $sort: { net: 1 } 
				  }
				]).limit(20).skip(skip).exec(function(err, movementsFound) {
					if (err) sendERR(err, res);
					if (movementsFound) {
						res.send(movementsFound)
					}
				});
			} else {
				movements.find({}).sort(sort).limit(20).skip(skip).exec(function(err, movementsFound) { 
					if (err) sendERR(err, res);
					if (movementsFound) {
						res.send(movementsFound);
					} else {
						sendERR("Movements not found", res);
					}
				});
			}
		} else {
			/* lat and long 'find' function */
			sendERR("Location functionality not optimal", res);
		}

		
	}

	var query = req.query;

	var type = query.type;
	console.log(type)
	var skip =  query.skip * 20;
	var location = query.location || "all";
	
	//var tags = query.tags || [];

	if (location == "all") {
		var distance = undefined;
	} else {
		var distance = query.distance || "25";
	}

	discover(type, location, skip, distance)

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
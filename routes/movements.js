var mongoose = require("mongoose");
var users = mongoose.model('user');
var movements = mongoose.model('movement');

function sendERR(err, res) {
	res.send("{ \"message\": \""+err+"\" }");
}	
	
exports.movementsGET = function(req, res, next) {
	function rate(y, n) { //"pop" algorithm in javascript
		var r;

		if ((y-n) >= 0) {
			r = 1-(n/y);
		} else {
			r = -1 * (1-(y/n));
		}

		return r; // returns ratio from -1 to 1
	}

	function discover(type, location, skip, distance, searchQuery) {
		var tquery;
		new_skip = skip * 20;

		if (type == "pop") {
			sort = {yays: -1, nays: 1};
			tquery = [	
		  	{
				$project: {
			    	yays: "$yays",
			    	nays: "$nays",	
					rate: {
						$cond: [ 
					      	{ $and: [{$eq: ["$nays", 1]}, {$eq: ["$nays", 1]}] },
							-1, 
							{
								$cond: [ 
							      	{$gte:[{ $subtract: ["$yays", "$nays"] },0]},
									{$subtract: [1,  { $divide: ["$nays", "$yays"] }] }, 
									{$multiply: [
										-1, 
										{$subtract: [1, {$divide: ["$yays", "$nays"]}]}
									]}
								] 
							}
						] 
						
					}
				}
			},
			{
				$sort: { rate: -1 }
			}]
		} else if (type == "hot") {
			tquery = [{
				$match: { 
					yays: { $gt: 1 }
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
			}];
		} else if (type == "new") {
			tquery = [{
				$sort: { dateCreated: -1 }
			}];
		} else if (type == "search") {
			if (searchQuery != null) {
				tquery = [{
					$match: { $text: { $search: searchQuery } },

				}, { 
					$sort: { score: { $meta: "textScore" } } 
				}];
			} else {
				tquery = [{
					$sort: { yays: -1 }
				}];
			}
		} else {
			tquery = [{
				$sort: { yays: -1 }
			}];
		}
		console.log(new_skip);
		console.log([parseFloat(location[0]), parseFloat(location[1])])
		if (location != "all") {
			tquery.unshift({
				$geoNear: {
			        near: { type: "Point", coordinates: [parseFloat(location[0]), parseFloat(location[1])] },
			        distanceField: distance,
			        query: { type: "public" },
			        includeLocs: "dist.location"
			    }
			});
			console.log(location)
			console.log(tquery)
		}

		movements.aggregate(tquery).skip(new_skip).limit(20).exec(function(err, movementsFound) {
			if (err) {
				console.log(err);
				sendERR(err, res);
			} else {
				if (movementsFound) {
					console.log(movementsFound)
					res.send(movementsFound);
				}
			}
			
		});

		
	}

	var query = req.query;
	var type = query.type;
	console.log(type)
	var skip =  query.skip;
	var searchQuery = query.q || null;
	if (typeof query.long == "undefined" || typeof query.lat == "undefined") {
		var location = "all";
	} else {
		var location = [query.long, query.lat] || "all";
	}
	
	
	//var tags = query.tags || [];

	if (location == "all") {
		var distance = undefined;
	} else {
		var distance = query.distance || "25";
	}

	discover(type, location, skip, distance, searchQuery)

}	
	
exports.movementsPOST = function(req, res, next) {
  	var data = req.body;
  	console.log(data);

  	try {
  		var newMovement = new movements({
			founder: data.founder,
			title: data.title,
			description: data.description,
			members: [],
			yays: 1,
			nays: 1,
			tags: data.tags,
			location: data.location
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
	console.log(movementId);
	
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

exports.movementsIdJoinPUT = function(req, res) {
	var data = req.body;
	var movementId = req.params.id;
	if (data.username) {
		movements.findOne({_id: movementId}, function(err, foundMovement){
			if (err) sendERR(err, res);
			if (foundMovement) {
				if (foundMovement.members.indexOf(data.username) > -1) {
					sendERR("User already joined movement", res);
				} else {
					users.findOne({username: data.username}, function(err1, foundUser) {
						if (err1) sendERR(err1, res);
						if (foundUser) {
							if (foundUser.movements.indexOf(foundMovement._id) > -1) {
								sendERR("User already joined movement", res);
							} else {
								foundMovement.members.push(data.username);								
								foundUser.movements.push(foundMovement._id);

								foundMovement.save();
								foundUser.save();

								res.send("{ \"message\": \"User "+foundUser.username+" joined movement "+foundMovement._id+"\" }")

							}

						} else {
							sendERR("User not found", res);
						}
					});
				}
				

				
			} else {
				sendERR("Movement not found", res);
			}
		})
	} else {
		sendERR("Username not sent in body", res);
	}
	
}
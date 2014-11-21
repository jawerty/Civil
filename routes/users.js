var mongoose = require("mongoose");
var users = mongoose.model('users');
var movements = mongoose.model('movements');

exports.usersPOST = function(req, res) {
	var data = req.body;
	console.log(data)
	try {
		var newUser = new users({
			firstName: data.firstName,
			lastName: data.lastName,
			email: data.email,
			username: data.username,
			password: data.password
		});

	  	newUser.save();

	  	console.log(newUser);

	  	res.send("{ \"message\": \"User "+data.username+" Created\", \"id\": \""+newUser._id+"\" }");

	} catch (err) {
		console.log(err)
		res.send(err)
	}
  	
}

exports.usersIdPOST = function(req, res) {
	var usersId = req.params.id;
  	res.send(usersId);
}

exports.usersIdGET = function(req, res) {
	var usersId = req.params.id;
	console.log(usersId)
	try {
		users.findOne({_id: usersId}, function(err, doc) {
	  		console.log(doc);
	  		res.send(doc);
	  	});
	} catch (err) {
		res.send(err)
	}
}

exports.usersIdDELETE = function(req, res) {
	var usersId = req.params.id;
  	res.send(usersId);
}
var mongoose = require("mongoose");
var users = mongoose.model('users');
var movements = mongoose.model('movements');
var bcrypt = require("bcrypt");

function sendERR(err) {
	res.send("{ \"message\": \""+err+"\" }");
}

exports.usersAuth = function(req, res) {
	sendERR("Auth function yet to be made.")
}

exports.usersPOST = function(req, res) {
	var data = req.body;
	console.log(data);	

	try {
		if (data.password.length < 8) {
			sendERR("Password must be at least 8 characters");
		} else {
			bcrypt.genSalt(10, function(err, salt) {
			    bcrypt.hash(data.password, salt, function(err, hash) {
			        bcrypt.compare(data.passwordCheck, hash, function(err, res) {
						if (res == true) {
							var newUser = new users({
								firstName: data.firstName,
								lastName: data.lastName,
								email: data.email,
								username: data.username,
								password: hash
							});

						  	newUser.save();

						  	console.log(newUser);

						  	res.send("{ \"message\": \"User "+data.username+" Created\", \"id\": \""+newUser._id+"\" }");
						} else if (res == false) {
							sendERR("Passwords must match")
						} else {
							sendERR("Miscellaneous error in password encryption")
						}
					});
			    });
			});

			
	  	}
	} catch (err) {
		console.log(err)
		sendERR(err)
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
	  		if (doc) {
	  			res.send("{ \"message\": \"User found\", \"document\": \""+doc+"\" }");
	  		} else {
	  			res.send("{ \"message\": \"User not found\" }");
	  		}
	  		
	  	});
	} catch (err) {
		sendERR(err)
	}
}

exports.usersIdDELETE = function(req, res) {
	var usersId = req.params.id;
  	res.send(usersId);
  	try {
		users.findOne({_id: usersId}).remove();
		res.send("{ \"message\": \"User deleted\" }");
	} catch (err) {
		sendERR(err)
	}
}
var mongoose = require("mongoose");
var users = mongoose.model('user');
var movements = mongoose.model('movement');
var bcrypt = require("bcrypt");
var crypto = require('crypto');
var validator = require('validator');
var gravatar = require('gravatar');

function sendERR(err, res) {
	res.send("{ \"message\": \""+err+"\" }");
}

exports.usersAuth = function(req, res) {
	var data = req.body;

	users.findOne({username: data.username}, function(err, foundUser) {
		if (err) console.log(err);
		if (foundUser) {
			bcrypt.compare(data.password, foundUser.password, function(err1, response) {
				if (response === true) {
					console.log(foundUser)
					var token = crypto.randomBytes(32).toString("hex");

					sess = req.session
					sess.token = token;

					res.send("{ \"message\": \"User authenticated\", \"token\": \""+token+"\", \"userID\": \""+foundUser._id+"\"}");

				} else if (response === false){
					sendERR("User not authenticated; invalid password", res);
				} else {
					sendERR("User not authenticated; miscellaneous error", res);
				}
			});
		} else {
			sendERR("User not found in database", res);
		}
		
	});
	
}

exports.usersPOST = function(req, res, next) {
	var data = req.body;
	console.log(data);	
	console.log("IMAGE: "+data.avatar)
	//validation
	var emailCheck = validator.isEmail(data.email);
	var usernameCheck = validator.isAlphanumeric(data.username);

	if (emailCheck === true && usernameCheck === true) {
		try {
			if (data.password.length < 8) {
				sendERR("Password must be at least 8 characters", res);
			} else {
				users.findOne({username: data.username}, function(err, foundUser) {
					if (err) console.log(err);
					if (foundUser) {
						sendERR("Username already in use", res);
					} else {
						users.findOne({email: data.email}, function(err, foundEmail) {
							if (err) console.log(err);
							if (foundEmail) {
								sendERR("Email already in use", res);
							} else {
								bcrypt.genSalt(10, function(err, salt) {
								    bcrypt.hash(data.password, salt, function(err1, hash) {
								        bcrypt.compare(data.passwordCheck, hash, function(err2, response) {
												if (response == true) {
														var secureUrl = gravatar.url(data.email, {s: '100', r: 'x', d: 'retro'}, true);

														var newUser = new users({
														w	lastName: data.lastName,
															email: data.email,
															username: data.username,
															password: hash,
															avatar: data.avatar,
															skills: data.skills
														});

														newUser.save();

														console.log(newUser);

														res.send("{ \"message\": \"User "+data.username+" Created\", \"id\": \""+newUser._id+"\" }");
														next();
											} else if (response == false) {
												sendERR("Passwords do not match.", res)
											} else {
												sendERR("Miscellaneous error in password validation", res)
											}
										});
									});
								});	
							}
						})
							
					}
					
				})
				
		  	}
		} catch (err) {
			console.log(err)
			sendERR(err, res)
		}	
	} else if (emailCheck === false) {
		sendERR("Email not valid", res);
	} else if (usernameCheck === false) {
		sendERR("Username must be alphanumeric", res);
	} else {
		sendERR("Your email and username are not valid", res)
	}
  	
}

exports.usersIdPOST = function(req, res, next) {
	var usersId = req.params.id;
	var data = req.body;

    try {
		users.findOne({_id: usersId}, function(err, doc) {
			if (err) console.log(err);
	  	if (doc) {

	  		for (var key in data) {
	  			if (key != "_id" || key != "dateCreated" || key != "password" || key != "username" || key != "email") {
	  				if (data[key] != null) {
		  				doc[key] = data[key];
		  			} 
	  			}
	  			
	  		}

	  		doc.save(function(err) {
	  			if (err) {
	  				sendERR("Could not save the data", res)
	  			} else {
	  				res.send("{ \"message\": \"User documents updated successfully\" }");
	  			}
	  		});

	  	} else {
	  		sendERR("User not found", res)
	  	}
	  });
	} catch (err) {
		sendERR(err, res)
	}
}

exports.usersIdGET = function(req, res) {
	var usersId = req.params.id;
	console.log(usersId)
	try {
		users.findOne({_id: usersId}, function(err, doc) {
			if (err) console.log(err);
	  		if (doc) {
	  			res.send("{ \"message\": \"User found\", \"document\": "+JSON.stringify(doc)+" }");
	  		} else {
	  			res.send("{ \"message\": \"User not found\" }");
	  		}
	  	});
	} catch (err) {
		sendERR(err, res)
	}
}

exports.usersIdDELETE = function(req, res) {
	var usersId = req.params.id;
	try {
		users.findOne({_id: usersId}).remove();
		res.send("{ \"message\": \"User deleted\" }");
	} catch (err) {
		sendERR(err, res)
	}
}

exports.usersLogout = function(req, res) {
	var token = req.body.token;
	console.log(token);
	if (req.session.token == token) {
		delete req.session.token;
		console.log("User session <"+token+"> successfully deleted");
		res.send("{ \"message\": \"User session <"+token+"> successfully deleted\" }");
	} else {
		sendERR("Token is invalid", res);
	}
}

exports.usersSkillsSearch = function(req, res) {
	//example query = http://localhost:3000/usersSkills?skills=this,that,those&skip=1
	var query = req.query;

	var skip = query.skip * 20 || 0;
	var skills = query.skills.split(',') || null;

	var tquery = [{
		skills: { $in: skills }
	}];

	users.aggregate(tquery).skip(skip).limit(20).exec(function(err, usersFound) {
		if (err) sendERR(err, res);
		if (usersFound) {
			res.send(usersFound)
		}
	});
}

exports.usersRequest = function(req, res) {
	var data = req.body;
	
	var movementId = data.movementId;
	var username = data.username;

	sendERR("Not functional yet", res);
}
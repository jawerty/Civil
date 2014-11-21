var mongoose = require('mongoose')
,Schema = mongoose.Schema
,ObjectId = Schema.ObjectId;
 
var usersSchema = new Schema({
	id: ObjectId,
	dateCreated: {type: Date, default: Date.now},
	firstName: {type: String},
	lastName: {type: String},
	email: {type: String, index: { unique: true }, required: true},
	username: {type: String, index: { unique: true }, required: true},
	password: {type: String, required: true}
});

var events = new Schema({
    Id: ObjectId,
    date: {type: Date, default: Date.now}, 
    name: {type: String},
    description: {type: String}
});

var announcements = new Schema({
    Id: ObjectId,
    title: {type: String},
    description: {type: String}
});

var movementsSchema = new Schema({
	id: ObjectId,
	dateCreated: {type: Date, default: Date.now},
	founder: {type: String},
	title: {type: String},
	description: {type: String},
	events: [events],
	announcements: [announcements]
});



module.exports = mongoose.model('users', usersSchema); 
module.exports = mongoose.model('movements', movementsSchema); 
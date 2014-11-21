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

var movementsSchema = new Schema({
	Id: ObjectId,
	dateCreated: {type: Date, default: Date.now},
	founder: {type: String},
	title: {type: String},
	description: {type: String},
	events: {type: String}
});

module.exports = mongoose.model('Post', postSchema); 
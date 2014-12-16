var mongoose = require('mongoose')
,Schema = mongoose.Schema
,ObjectId = Schema.ObjectId;
 
var db_url = process.env.MONGOHQ_URL || "mongodb://localhost:27017/Civil",
db = mongoose.connect(db_url);
conn = mongoose.connection
conn.on('error', console.error.bind(console, 'connection error:'));

var usersSchema = new Schema({
	id: ObjectId,
	dateCreated: {type: Date, default: Date.now},
	firstName: {type: String},
	lastName: {type: String},
	skills:  [{type: String}],
	movements: [{type: String}],
	email: {type: String, index: { unique: true }, required: true},
	username: {type: String, index: { unique: true }, required: true},
	password: {type: String, required: true},
	gravatar: {type: String},
	avatar: { data: Buffer, contentType: String }
});

var eventSchema = new Schema({
    Id: ObjectId,
    date: {type: Date, default: Date.now}, 
    name: {type: String},
    description: {type: String}
});

var announcementSchema = new Schema({
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
	events: [eventSchema],
	announcements: [announcementSchema]
});

module.exports = db.model('user', usersSchema, 'user'); 
module.exports = db.model('movement', movementsSchema, 'movement'); 
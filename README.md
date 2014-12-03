<<<<<<< HEAD
# Civil
Socializing democracy

# API
The API is based around two root endpoints

```
/users
```
and
```
/movements
```

## Movements
### POST /movements
DATA
```
{
	founder: ...,
	title: ...,
	description: ...
}
```

### GET /movements
TBD....

### GET /movements/:id
RESPONSE
```
{
	id: ...,
	dateCreated: ...,
	founder: ...,
	title: ...,
	description: ...,
	events: [...],
	announcements: [...]
}
```

### POST /movements/:id
DATA
```
{
	//any combination of values: 'founder', 'title', 'events', etc.
}
```
### DELETE /movements/:id
RESPONSE
```
{
	message: "Movement <id> deleted"
}
```

### POST /movements/:id/announcements
TBD....

### GET /movements/:id/announcements
TBD....

### POST /movements/:id/events
TBD....

### GET /movements/:id/events
TBD....

## Users
### POST /users
DATA 
```
{
	firstName: ...,
	lastName: ...,
	email: ...,
	username: ...,
	password: ...,
	passwordCheck: ...
}
```

### GET /users/:id
RESPONSE 
```
{
	firstName: ...,
	lastName: ...,
	email: ...,
	username: ...,
	movements: [...],
	//TBD
}
```

### POST /users/:id
DATA
```
{
	//any combination of values: 'movements', 'email', 'firstname', 'lastname', etc.
}
```

### DELETE /users/:id
RESPONSE 
```
{
	message: "User <id> deleted"
}
```
=======
# CIVIL
Socializing Democracy

<INSERT DOCS HERE>
>>>>>>> 75a82fda59112a9eede9ddb186fe5a706fb42674

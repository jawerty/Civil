# Civil
Socializing democracy

# Running Civil
```

mongod --setParameter textSearchEnabled=true
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

### POST /users/auth
DATA
```
{
	username: ...,
	password: ...
}
```

### GET /movements
```
Example query

http://localhost:3000/movements?type=hot&skip=0&long=2&lat=2&distance=25

^returns hot movements at 2,2 with a 25 mile radius
```
# API Design

## Authentication

```
POST: /api/auth/login

POST: /api/auth/register
```

## Working schedule


### General
```
```
### Staff
```
GET: /api/shifts/{staff_id}/current-week

GET: /api/shifts/{staff_id}/next-week

POST: /api/shifts/register

POST: /api/shifts/change/swap

GET: /api/shifts/day-off/{id}

POST: /api/shifts/day-off

POST: /api/shifts/change/register

POST: /api/shifts/change/accept
```
### Manager
```
GET: /api/shifts/day-off

PUT: /api/shifts/schedule

POST: /api/shifts/schedule/approve

POST: /api/shifts/schedule/new

POST: /api/shifts/day-off/approve

POST: /api/shifts/day-off/deny

POST: /api/shifts/change/approve

POST: /api/shifts/change/deny
```















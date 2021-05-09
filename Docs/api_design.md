# API Design

## Authentication

```
POST: /api/auth/login

POST: /api/auth/register
```

## Working schedule

```
GET: /api/shifts/{staff_id}/current-week

GET: /api/shifts/{staff_id}/next-week

GET: /api/shifts/day-off

POST: /api/shifts/register

POST: /api/shifts/day-off

POST: /api/shifts/change-shift/request

POST: /api/shifts/change-shift/accept
```

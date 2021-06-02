# API Design

## Authentication

```
POST: /api/auth/login

POST: /api/auth/register
```

## Management

### Users

```
GET: /api/users?position=brand-manager
GET: /api/users?position=store-manager&brandid=2
GET: /api/users?position=normal-staff&storeid=23
GET: /api/users/{id}
POST: /api/users
PUT: /api/users/{id}
DELETE: /api/users/{id}
```

### Brands

```
GET: /api/brands
GET: /api/brands/{id}
POST: /api/brands
PUT: /api/brands/{id}
DELETE: /api/brands/{id}
```

### Stores

```
GET: /api/stores
GET: /api/stores/{id}
POST: /api/stores
PUT: /api/stores/{id}
DELETE: /api/stores/{id}
```

### Skills

```
GET: /api/skills?brandid=1
GET: /api/skills/{id}
POST: /api/skills
PUT: /api/skills/{id}
DELETE: /api/skills/{id}
```

## Working schedule

### Shift Register

```
GET: /api/shift-registers?storeid=1

GET: /api/shift-registers/{staff_id}

POST: /api/shift-registers

PUT: /api/shift-registers/{staff_id}

DELETE: /api/shift-registers/{staff_id}

```

### Shift Assignment

- some api may required authorized.

```
GET: /api/shift-assignments?storeid=1

GET: /api/shift-assignments/{staff_id}

GET: /api/shift-assignments/{staff_id}/attendance

POST: /api/shift-assignments

PUT: /api/shift-assignments/{staff_id}

DELETE: /api/shift-assignments/{staff_id}
```

### Shift Attendance

```
GET: /api/shift-attendance?storeid=1

GET: /api/shift-attendance/{id}

PUT: /api/shift-attendance/{id}
```

## Store set up

```
GET: /api/week-schedule
GET: /api/week-schedule/{id}
```

```
GET: /api/week-schedule-detail
GET: /api/week-schedule-detail/{id}
POST: /api/week-schedule-detail
PUT: /api/week-schedule-detail/{id}
DELETE: /api/week-schedule-detail/{id}
```

```
GET: /api/staff-schedule-detail
GET: /api/staff-schedule-detail/{id}
POST: /api/staff-schedule-detail
PUT: /api/staff-schedule-detail/{id}
DELETE: /api/staff-schedule-detail/{id}
```

### Staff

```
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

# sts-api

More documentation, go to Docs/

## FirebaseAdmin

Download your firebase credential and paste it to STS/

## RabbitMQ

Config your RabbitMQ credential at STS/appsettings.json

## Database

Config your Database connection string at STS/appsettings.json

### PostGres

### MS SQL Server

Uncomment MS SQL connection code at

- ConfigureDevelopmentServices
- ConfigureProductionServices
  Comment
- services.AddDbConnectionServices(Configuration);

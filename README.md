# sts-api

More documentation, go to Docs/

## Config

config files stored at STS/

appsetting.json
```json
{
  "ConnectionStrings": {
    "ProductionConnection": "--Enter your production connection string here--",
    "DevelopmentConnection": "--Enter your development connection string here--",
    "PostGresConnection": "--Enter your postgres connection string here--"
  },
  "TokenKey": "--Provide your token key here--",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "EmailConfiguration": {
    "From": "<youremail@example.com>",
    "SmtpServer": "smtp.gmail.com",
    "Port": 465,
    "Username": "<youremail@example.com>",
    "Password": "<your-email-password>"
  },
  "FCMConfiguration": {
    "ServerKey": "--Your FCM server key--",
    "SenderId": "--Sender Id--"
  },
  "RabbitMqLocal": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  },
  "RabbitMqHeroku": {
    "VirtualHost": "--your virtual host--",
    "HostName": "--your host name--",
    "Port": 5672,
    "UserName": "--your username--",
    "Password": "--your password--"
  },
  "AllowedHosts": "*"
}

```
sts-manager-firebase-adminsdk.json
```json
{
  "type": "service_account",
  "project_id": "sts-manager-e9639",
  "private_key_id": "---PRIVATE KEY ID---",
  "private_key": "-----BEGIN PRIVATE KEY-----PRIVATE KEY-----END PRIVATE KEY-----\n",
  "client_email": "firebase-adminsdk-wfp4r@sts-manager-e9639.iam.gserviceaccount.com",
  "client_id": "110936881240097553941",
  "auth_uri": "https://accounts.google.com/o/oauth2/auth",
  "token_uri": "https://oauth2.googleapis.com/token",
  "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
  "client_x509_cert_url": "https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-wfp4r%40sts-manager-e9639.iam.gserviceaccount.com"
}

```

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

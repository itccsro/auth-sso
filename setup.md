## Requirements
npm https://www.npmjs.com/
.net core https://www.microsoft.com/net/core
mysql http://www.mysql.com/

## Steps
branch from master
create database sso
modify \\govithub-auth-sso\src\GovITHub.Auth.Identity\appsettings.json to reflect database connection string

### Identity server

open shell (command promt)
go to \\govithub-auth-sso\src\GovITHub.Auth.Identity
npm install
dotnet ef database update
npm install
dotnet run

### Javascript client sample
open shell (command promt)
go to \\govithub-auth-sso\src\samples\JavaScriptClient
npm install
dotnet run

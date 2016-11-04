# Setup Facebook authentication #

## Prerequisites ##

Install [Secret Manager tool](https://docs.asp.net/en/latest/security/app-secrets.html#secret-manager) to be able to store Facebook credentials.

## Register Facebook app ##

- Navigate to [https://developers.facebook.com/apps](https://developers.facebook.com/apps) and add new Facebook app
- Open the `Settings` tab and click on `Add new platform`
- Select `Website` as the platform
- In `Site URL` box put `https://localhost:44301/`

## Add Facebook app credentials ##

- Open shell
- Navigate to `./src/GovITHub.Auth.Identity/`
- Execute the following commands:
```
dotnet user-secrets set Authentication:Facebook:AppId <app-Id>
dotnet user-secrets set Authentication:Facebook:AppSecret <app-secret>
```

## Google app setup
Setup app on https://console.developers.google.com/

On credentials edit form, set Authorized redirect URIs to [appURL]/signin-google (e.g. http://localhost:5000/signin-google)


## Dev setup
open shell

dotnet user-secrets set Authentication:Google:GoogleClientId **valueOfGoogleClientId**

dotnet user-secrets set Authentication:Google:GoogleClientSecret **valueOfGoogleClientSecret**

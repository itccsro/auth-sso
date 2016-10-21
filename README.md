# Citizen Single Sign On - GovIT Hub Authenticator 
Un singur set de credentiale pentru cetatean. La baza este un identity server ce expune un endpoint Outh2.

## [1]Instalare
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

## Tehnologii folosite
- [.NET Core] https://www.microsoft.com/net/core
- [AngularJS] https://angularjs.org/
- [Docker] https://docs.docker.com/engine/installation/
- [npm] https://github.com/npm/npm
- [1][MySQL] http://www.mysql.com/

```
[1] Posibil să se schimbe în viitorul apropiat
```

### Extra (SDKs, samples & integration)
- [React]
- [PHP]
- [Java]
- [Python]

## Contribuie

Dacă vrei să contribui - ești binevenit(ă) - but we don't have cookies (yet) 

### Proces
- Vezi lista de tehnologii folosite - îți sunt familiare?
- Dacă nu ești încă înscris(ă) în comunitate, te rog fă-o pe [site-ul GovITHub](http://ithub.gov.ro/formular-de-aplicatie/)
- Aruncă o privire la [guidelines](https://github.com/gov-ithub/guidelines/blob/master/CODE_REVIEW.md) pentru code review 
- Caută în issues un task interesant pentru tine (sau, dacă ai o propunere, deschide un issue / trimite un pull request). 
- După ce trecem prin code review - celebrate! :star: :star2: :star:

## Cum poti intra in contact cu echipa?
- Email: alexandru.chiraples@ithub.gov.ro
- [Slack](https://govithub.slack.com/messages/gov-authenticator/details/) 

Mai multe detalii în curând! 
## Postmark app setup
Setup account on https://account.postmarkapp.com/login
Add server
Add new Sender Signature email address

## Dev setup
add appsettings.Development.json file with the following content

{
    "Postmark": {
        "ServerToken": "POSTMARK_SERVER_TOKEN_AS_GUID",
        "OriginEmail": "EMAIL_ADDRESS_CONFIGURED_AS_SENDER_SIGNATURE"
    }
}


enjoy :)
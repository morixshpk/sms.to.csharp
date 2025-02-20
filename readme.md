# morix sms.to.csharp

## Overview

sms.to.csharp is a .NET library that integrates with the SMS.to API making it easy to send SMS messages from C# applications.

## Initialization

To use sms.to.csharp, you need to need to call `Init` method with the required parameters.

```csharp
using sms.to;

ApiClient.Init(new Config
{
    ApiKey = "your api key from http://sms.to account",
    ApiUrl = "https://api.sms.to",
    SenderId = "Morix"
});

```

### Parameters

- `ApiKey` _(string, required)_ - The API key for authentication, from [sms.to](http://sms.to) account.
- `ApiUrl` _(string, required)_ - The API URL.
- `SenderId` _(string, required)_ - "From Address", usually your **Brand Name** or **Company Name**, [more about sender id](https://intergo.freshdesk.com/support/solutions/articles/43000513909)

## Usage

### Sending SMS

Create a SMS object and send a message:

```csharp
var sms = new SMS
{
    To = "+355690123456",
    Message = "Your code 123654 to login to accounts.al! " ,
    SenderId = "Morix"
};

var sent = sms.Send();
```

**Parameters:**

- `To` _(string, required )_ - The recipient's phone number.
- `Message` _(string, required)_ - The SMS text to send.
- `SenderId` _(string, required)_ - The sender's identifier.

**Returns:**

- `bool` - A value indicating whether the SMS was sent successfully.

### Estimating SMS Cost

Create a SMS object and estimate cost:

```csharp
var sms = new SMS
{
    To = "+355690123456",
    Message = "Your code 123654 to login to accounts.al! " ,
    SenderId = "Morix"
};
```

**Parameters:**

- `To` _(string, required )_ - The recipient's phone number.
- `Message` _(string, required)_ - The SMS text to send.
- `SenderId` _(string, required)_ - The sender's identifier.

**Returns:**

- `decimal` - The estimated cost of sending the SMS message.

## Full Example Usage

```csharp
using sms.to;

ApiClient.Init(new Config
{
    ApiKey = "your api key from http://sms.to account",
    ApiUrl = "https://api.sms.to",
    SenderId = "Morix"
});

var sms = new SMS
{
    To = "+355690123456",
    Message = "Your code 123654 to login to accounts.al! " ,
    SenderId = "Morix"
};

var sent = sms.Send();
```

## License

This library is licensed under the [MIT License](LICENSE).

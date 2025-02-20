# sms.to.csharp

## Overview

sms.to.csharp is a .NET library that integrates with the SMS.to API making it easy to send SMS messages from C# applications.

## Initialization

To use sms.to.csharp, you need to need to call `Init` method with the required parameters.

```csharp
using sms.to.csharp;
using sms.to.csharp.Models;

Manager.Init(new InitConfigs
{
	SenderId = "your_sender_id",
    ApiKey = "your_api_key",
    ApiUrl = "https://api.sms.to"
});
```

### Parameters

- `SenderId` _(string, optional)_ - "From Address", usually your **Brand Name** or **Company Name**, [more about sender id](https://intergo.freshdesk.com/support/solutions/articles/43000513909), Optional if sender id specified in sms.to dashboard.
- `ApiKey` _(string, required)_ - The API key for authentication.
- `ApiUrl` _(string, required)_ - The API URL.

## Usage

### Method 1: `SendSMS`

This method sends a single SMS

```csharp
var results = Manager.SendSMS(new SingleSMSRequest
{
    To = "+1234567890",
    Message = "Hello, There!"
});
```

**Parameters:**

- `model` _(SingleSMSRequest, required)_ - The input model.
  - `SingleSMSRequest`
    - `To` _(string, required )_ - Phone number.
    - `Message` _(string, required)_ - SMS message

**Returns:**

- `SingleSMSResponse` - The response model.

### Method 2: `EstimateSingleSms`

Estimates the cost of SMS.

```csharp
var results = Manager.EstimateSingleSms(new SingleSMSRequest
{
    To = "+1234567890",
    Message = "Hello, There!"
});
```

**Parameters:**

- `model` _(SingleSMSRequest, required)_ - The input model.
  - `SingleSMSRequest`
    - `To` _(string, required )_ - Phone number.
    - `Message` _(string, required)_ - SMS message

**Returns:**

- `EstimatedResponse` - The response model data.

## Full Example Usage

```csharp
using sms.to.csharp;
using sms.to.csharp.Models;

Manager.Init(new InitConfigs
{
	SenderId = "your_sender_id",
    ApiKey = "your_api_key",
    ApiUrl = "https://api.sms.to",
});

var result = Manager.SendSMS(new SingleSMSRequest
{
    To = "+1234567890",
    Message = "Hello, There!"
});
```

dwolla.net-v2
=========

[![Join the chat at https://gitter.im/Dwolla/dwolla.net](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Dwolla/dwolla.net?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

A .NET library for the Dwolla v2 API based on the WCF HTTP client. Based off the [official Dwolla v1 library](https://github.com/Dwolla/dwolla.net).

## Version

(no release yet)

## Installation

`dwolla.net-v2` is not yet available on [NuGet](https://www.nuget.org/).
<!-- To install, drop to a package-management shell in VS or similar and execute: -->

<!-- ```powershell -->
<!-- Install-Package dwolla.net -->
<!-- ``` -->

## Quickstart

`dwolla.net-v2` makes it easy for developers to hit the ground running with Dwolla's v2 API. Before attempting the following, you should ideally create [an application key and secret](https://www.dwolla.com/applications).

* Configure your application via `App.config`.
* Instantiate `dwolla.net-v2` with the class that contains the endpoints you require.
* Use at will!

### Application Settings

`dwolla.net-v2` uses the `Config` class to easily pull and change settings located in your application's `App.config` file.

#### Changing settings

An instance of the `Config` class is instantiated in `Rest()` as below:

```cs
public Config C = new Config();
```

For example, if you had an instance of the `Accounts()` class, you could change your `client_id` by doing the following:

```cs
var a = new Accounts();
a.C.client_id = "Some Client ID";
```

#### App.config boilerplate

It is recommended that you do not change any of the values which have not been left empty.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="client_id" value="" />
    <add key="client_secret" value="" />
    <add key="pin" value=""/>
    <add key="access_token" value=""/>
    <add key="oauth_scope" value="Send|Transactions|Balance|Request|Contacts|AccountInfoFull|Funding|ManageAccount" />
    <add key="production_host" value="https://api.dwolla.com" />
    <add key="sandbox_host" value="https://api-uat.dwolla.com" />
    <add key="sandbox" value="true"/>
    <add key="debug" value="true"/>
    <add key="dwolla_accept" value="application/vnd.dwolla.v1.hal+json" />
  </appSettings>
</configuration>
```

#### Example: List Transactions

To list 10 transactions from a user with the OAuth token set in the configuration settings:
```cs
using DwollaV2;

var t = new Transactions();
var list = t.Get();
```

...or, to specify an alternate OAuth token:
```cs
using DwollaV2;

var t = new Transactions();
var list = t.Get(altToken: "Some Alternate OAuth Token");
```

## Example Applications

`ExampleMVC` is an ASP.NET C# MVC application that can send money, and uses Razor/cshtml and partial views. It automatically handles the OAuth handshake as you log in with your Dwolla account.

![Logon](http://puu.sh/gGIVJ/ced4ede64f.png)
![OAuth](http://puu.sh/gGIWK/1331885e42.png)
![Send](http://puu.sh/gGIXT/7d21c7d5c5.png)

`ExampleApp` is an ASP.NET C# application that can send money and list transaction history. 

![ExampleApp](http://puu.sh/gcgVw/42b3d4d7a1.png)

## Exceptions

`dwolla.net-v2` has two exception classes to help identify invalid API responses:

* `DwollaV2.APIException` will be returned when the server returns `false` for the `Success` variable in the standard ["Dwolla envelope"](https://docs.dwolla.com/?json#responses)

* `DwollaV2.OAuthException` will be returned when `access_token` is returned null - the library will then attempt to re-serialize the response as an `OAuthError` to return the error body.

## Serializable Types

In order to make the developer's life easier, we have included various serializable types of objects needed to make requests to our API in `DwollaV2.SerializableTypes`. Nullable types for primitives have been used so that the WCF serializer does not throw an exception when the API returns a null value for a non-nullable primitive.

```cs
public class DwollaResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Response { get; set; }
}

public class MassPayJob
{
    public string Id { get; set; }
    public string UserJobId { get; set; }
    public bool? AssumeCosts { get; set; }
    public string FundingSource { get; set; }
    // TODO: Floats as currency is bad, will change when it's due time.
    public double? Total { get; set; }
    public double? Fees { get; set; }
    public string CreatedDate { get; set; }
    public string Status { get; set; }
    public MassPayItemSummary ItemSummary { get; set; }
}


public class MassPayItem
{
    public double? amount { get; set; }
    public string destination { get; set; }
    public string destinationType { get; set; }
    public string notes { get; set; }
    public Dictionary<string, string> metadata { get; set; }
}

public class MassPayRetrievedItem
{
    public string JobId { get; set; }
    public string ItemId { get; set; }
    public string Destination { get; set; }
    public string DestinationType { get; set; }
    public double? Amount { get; set; }
    public string Status { get; set; }
    public int? TransactionId { get; set; }
    public string Error { get; set; }
    public string CreatedDate { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

public class MassPayItemSummary
{
    public int? Count { get; set; }
    public int? Completed { get; set; }
    public int? Successful { get; set; }
}

public class FundingSource
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public bool? Verified { get; set; }
    public string ProcessingType { get; set; }
}

public class Contact
{
    public string Name { get; set; }
    public string Id { get; set; }
    public string Type { get; set; }
    public Uri Image { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}

public class UserNearby
{
    public string Id { get; set; }
    public Uri Image { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Delta { get; set; }
}

public class UserBasic
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public class UserFull
{
    public string City { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public class AutoWithdrawalStatus
{
    public bool? Enabled { get; set; }
    public string FundingId { get; set; }
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public Uri Image { get; set; }
}

public class Request
{
    public string Id { get; set; }
    public User Source { get; set; }
    public User Destination { get; set; }
    public double? Amount { get; set; }
    public string Notes { get; set; }
    public string DateRequested { get; set; }
    public string Status { get; set; }
    public string Transaction { get; set; }
    public User CancelledBy { get; set; }
    public string DateCancelled { get; set; }
    public bool? SenderAssumeFee { get; set; }
    public bool? SenderAssumeAdditionalFee { get; set; }
    public List<string> AdditionalFees { get; set; }
    public Dictionary<string, string> Metadata { get; set; }

}

public class RequestFulfilled
{
    public string Id { get; set; }
    public int? RequestId { get; set; }
    public double? Amount { get; set; }
    public string SentDate { get; set; }
    public string ClearingDate { get; set; }
    public string Status { get; set; }
    public User Source { get; set; }
    public User Destination { get; set; }
}

public class OAuthResponse
{
    public string access_token { get; set; }
    public int? expires_in { get; set; }
    public string refresh_token { get; set; }
    public int? refresh_expires_in { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }
    public string account_id { get; set; }
}

public class OAuthCatalog
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Response { get; set; }
    public List<Dictionary<string, Dictionary<string, string>>> _links { get; set; }
}

public class OAuthError
{
    public string error { get; set; }
    public string error_description { get; set; }
}

public class Transaction
{
    public string Id { get; set; }
    public double? Amount { get; set; }
    public string Date { get; set; }
    public string Type { get; set; }
    public string UserType { get; set; }
    public string DestinationId { get; set; }
    public string DestinationName { get; set; }
    public User Destination { get; set; }
    public string SourceId { get; set; }
    public string SourceName { get; set; }
    public User Source { get; set; }
    public string ClearingDate { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
    public Dictionary<string, string> Fees { get; set; }
    public string OriginalTransactionId { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

public class Refund
{
    public int? TransactionId { get; set; }
    public string RefundDate { get; set; }
    public double? Amount { get; set; }
}

public class TransactionStats
{
    public int? TransactionsCount { get; set; }
    public double? TransactionsTotal { get; set; }
}

public class PurchaseOrder
{
    public string destinationId { get; set; }
    public double? total { get; set; }
    public double? tax { get; set; }
    public double? discount { get; set; }
    public double? shipping { get; set; }
    public string notes { get; set; }
    public double? facilitatorAmount { get; set; }
    public Dictionary<string, string> metadata { get; set; }
    public List<CheckoutItem> orderItems { get; set; }
}

public class CheckoutItem
{
    public string name { get; set;}
    public string description { get; set; }
    public int? quantity { get; set; }
    public double? price { get; set; }
}

public class Checkout
{
    public string CheckoutId { get; set; }
    public double? Discount { get; set; }
    public double? Shipping { get; set; }
    public double? Tax { get; set; }
    public double? Total { get; set; }
    public string Status { get; set; }
    public string FundingSource { get; set; }
    public int? TransactionId { get; set; }
    public string ProfileId { get; set; }
    public int? DestinationTransactionId { get; set; }
    public List<CheckoutItem> OrderItems { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

public class CheckoutID
{
    public string CheckoutId { get; set; }
}

public class CheckoutComplete
{
    public double? Amount { get; set; }
    public string CheckoutId { get; set; }
    public string ClearingDate { get; set; }
    public string OrderId { get; set; }
    public bool? TestMode { get; set; }
    public int? TransactionId { get; set; }
    public int? DestinationTransactionId { get; set; }
}

public class ScheduledTransaction
{
    public string Id { get; set; }
    public string ScheduledDate { get; set; }
    public string ExpectedClearingDate { get; set; }
    public string TransactionId { get; set; }
    public double? Amount { get; set; }
    public string FundingSource { get; set; }
    public bool? AssumeCosts { get; set; }
    public User Destination { get; set; }
    public string Notes { get; set; }
    public string Status { get; set; }
    public string CreatedDate { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

public class ScheduledRecurrence
{
    public string frequency { get; set; }
    public string endDate { get; set; }
    public string endAfter { get; set; }
    public int? repeatEvery { get; set; }
    public string onDays { get; set; }
}

public class ScheduledTransactions
{
    public int? Total { get; set; }
    public int? Count { get; set; }
    public List<ScheduledTransaction> Results { get; set; }
}
```

## Structure

`dwolla.net-v2` is a conglomerate of multiple classes; each file in the `lib/` directory contains a class which contains all the endpoints for that certain category ([similar to Dwolla's developer documentation](https://docs.dwolla.com)).

### Endpoint Classes / Methods

Each endpoint class extends `Rest` located in `Rest.cs`.

* `Account()`:
 * `Basic()`: Retrieves basic account information
 * `Full()`: Retrieve full account information
 * `Balance()`: Get user balance
 * `Nearby()`: Get nearby users
 * `GetAutoWithdrawalStatus()`: Get auto-withdrawal status
 * `ToggleAutoWithdrawalStatus()`: Toggle auto-withdrawal
* `Checkouts()`:
 * `Create()`: Creates a checkout session.
 * `Get()`: Gets status of existing checkout session.
 * `Complete()`: Completes a checkout session.
 * `Verify()`: Verifies a checkout session.
* `Contacts()`:
 * `Get()`: Retrieve a user's contacts.
 * `Nearby()`: Get spots near a location.
* `FundingSources()`:
 * `Info()`: Retrieve information regarding a funding source via ID.
 * `Get()`: List all funding sources.
 * `Add()`: Add a funding source.
 * `Verify()`: Verify a funding source.
 * `Withdraw()`: Withdraw from Dwolla into funding source.
 * `Deposit()`: Deposit to Dwolla from funding source.
* `MassPay()`:
 * `Create()`: Creates a MassPay job.
 * `GetJob()`: Gets a MassPay job.
 * `GetJobItems()`: Gets all items for a specific job.
 * `GetItem()`: Gets an item from a specific job.
 * `ListJobs()`: Lists all MassPay jobs.
* `OAuth()`:
 * `GenAuthUrl()`: Generates OAuth permission link URL
 * `Get()`: Retrieves OAuth + Refresh token pair from Dwolla servers.
 * `Refresh()`: Retrieves OAuth + Refresh pair with refresh token.
 * `Catalog()`: Returns a catalog of endpoints available for use with the current OAuth token
* `Requests()`:
 * `Create()`: Request money from user.
 * `Get()`: Lists all pending money requests.
 * `Info()`: Retrieves info for a pending money request.
 * `Cancel()`: Cancels a money request.
 * `Fulfill()`: Fulfills a money request.
* `Transactions()`:
 * `Send()`: Sends money
 * `Refund()`: Refunds money
 * `Get()`: Lists transactions for user
 * `Info()`: Get information for transaction by ID.
 * `Refund()`: Refund a transaction.
 * `Stats()`: Get transaction statistics for current user.
 * `Schedule()`: Schedule a transaction for a later date.

## Integration Testing

`dwolla.net-v2` uses [MSTest](https://msdn.microsoft.com/en-us/library/ms182489.aspx) for its tests, the preferred way to run them is by using the Visual Studio Test Explorer. Integration tests have been written for most endpoints, with unit tests coming on the way for endpoints which are impractical to do live tests for.

Travis-Ci build verification is planned when the tests are going to be migrated to a framework such as X-Test which does not require Microsoft Windows or Visual Studio. As of now, the maintainer runs MSTest to validate each build before pushing.

## Credits

This wrapper is based on [HttpClient](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient%28v=vs.118%29.aspx) for REST capability and [MSTest](https://msdn.microsoft.com/en-us/library/ms182489.aspx) for unit testing.

Initially written by [David Stancu](http://davidstancu.me) (david@dwolla.com).

Rewrite for v2 API by [Scott Weldon](https://scott-weldon.com) (open-source@scott-weldon.com).

## License

Copyright (c) 2015 Dwolla Inc, David Stancu  
Copyright (c) 2016 Outlyer Technologies, Scott Weldon

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

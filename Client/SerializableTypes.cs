using System;

// This namespace contains WCF-serializable types for
// different Dwolla responses.
using System.Collections.Generic;

namespace DwollaV2.SerializableTypes
{
  /// <summary>
  ///   We do not use nullable primitives here
  ///   because we assume the API will always return
  ///   a value.
  /// </summary>
  /// <typeparam name="T"></typeparam>
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
}

using System;
using System.Runtime.Serialization;

namespace Dwolla
{
  /// <summary>
  ///   Custom exception class for invalid API responses.
  /// </summary>
  [Serializable]
  public class ApiException : ApplicationException
  {
    public ApiException() : base() { }
    public ApiException(string message) : base(message) { }
    protected ApiException(SerializationInfo info, StreamingContext context) { }
  }

  /// <summary>
  ///   Custom exception class for invalid OAuth responses.
  /// </summary>
  [Serializable]
  public class OAuthException : ApplicationException
  {
    public OAuthException() : base() { }
    public OAuthException(string message) : base(message) { }
    protected OAuthException(SerializationInfo info, StreamingContext context) { }
  }
}

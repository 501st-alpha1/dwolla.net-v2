using System;
using System.Runtime.Serialization;
using Dwolla.SerializableTypes;

namespace DwollaV2
{
  /// <summary>
  ///   Custom exception class for invalid API responses.
  /// </summary>
  [Serializable]
  public class ApiException : ApplicationException
  {
    private ErrorResponse _response;

    public ErrorResponse Response
    {
      get { return _response; }
    }

    public ApiException() : base() { }
    public ApiException(string message) : base(message) { }

    public ApiException(string message, ErrorResponse response) : base(message)
    {
      this._response = response;
    }

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

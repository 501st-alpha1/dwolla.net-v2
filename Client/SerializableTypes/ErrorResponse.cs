using System.Collections.Generic;

namespace DwollaV2.SerializableTypes
{
  public class ErrorResponse
  {
    public string Code { get; set; }
    public string Message { get; set; }
    public EmbeddedErrorList _Embedded { get; set; }
  }
}

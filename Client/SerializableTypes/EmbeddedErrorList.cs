using System.Collections.Generic;

namespace DwollaV2.SerializableTypes
{
  public class EmbeddedErrorList
  {
    public IList<EmbeddedErrorResponse> Errors { get; set; }
  }
}

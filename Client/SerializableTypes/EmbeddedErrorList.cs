using System.Collections.Generic;

namespace Dwolla.SerializableTypes
{
  public class EmbeddedErrorList
  {
    public IList<EmbeddedErrorResponse> Errors { get; set; }
  }
}

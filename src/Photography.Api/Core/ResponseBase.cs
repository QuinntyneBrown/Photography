using System.Collections.Generic;

namespace Photography.Api.Core
{
    public class ResponseBase
    {
        public List<string> Errors { get; set; } = new();
    }
}

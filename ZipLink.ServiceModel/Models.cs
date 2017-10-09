using System;
using System.Collections.Generic;
using ServiceStack;

namespace ZipLink.ServiceModel
{
    
    [Route("/zippedLink/all")]
    public class GetAllZippedLinkRequest : IReturn<GetAllZippedLinkResponse>
    {
        
    }

    public class GetAllZippedLinkResponse
    {
        public IEnumerable<GetZippedLinkResponse> Links { get; set; }
    }

    public class GetZippedLinkResponse
    {
        public string Hash { get; set; }
        public string OriginalLink { get; set; }
        public DateTime Created { get; set; }
        public int Followed { get; set; }
        public string ZippedLink { get; set; }
    }
    
    [Route("/zippedLink/create")]
    public class CreateZippedLinkRequest : IReturn<CreateZippedLinkResponse>
    {
        public string Link { get; set; }
    }

    public class CreateZippedLinkResponse
    {
        public CreatedStatusEnum Status { get; set; }
        public string StatusText { get; set; }
        public string ZippedLink { get; set; }
        
        public enum CreatedStatusEnum {
            Success = 1,
            Fail = 0
        }
    }

}

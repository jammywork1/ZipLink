using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ServiceStack;
using ZipLink.BusinessLogic;
using ZipLink.ServiceModel;

namespace ZipLink.ServiceInterface
{
    
    public class ZippedLinkService : Service
    {
        private readonly ZippedLinkFacade _zippedLinksFacade;

        public ZippedLinkService(ZippedLinkFacade zippedLinksFacade)
        {
            _zippedLinksFacade = zippedLinksFacade;
        }


        public async Task<object> Any(GetAllZippedLinkRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var models = await _zippedLinksFacade.GetAllZippedLinks();
            return new GetAllZippedLinkResponse()
            {
                Links = models.Select(i => new GetZippedLinkResponse()
                {
                    ZippedLink = $"{GetFullZippedLink(i.Hash)}"
                }.PopulateWithNonDefaultValues(i))
            };
        }
        
        public async Task<object> Any(CreateZippedLinkRequest request) {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var response = new CreateZippedLinkResponse();
            try
            {
                var model = await _zippedLinksFacade.ZipLink(request.ConvertTo<ZipLinkDTO>());
                response.Status = CreateZippedLinkResponse.CreatedStatusEnum.Success;
                response.StatusText = $"Your link zipped: <a href='{GetFullZippedLink(model.Hash)}'>{model.Hash}</a>!";
            }
            catch (ValidationException e)
            {
                response.Status = CreateZippedLinkResponse.CreatedStatusEnum.Fail;
                response.StatusText = e.Message;
            }
            return response;
        }
        
        private string GetFullZippedLink(string hash)
        {
            return $"{this.Request.GetApplicationUrl()}/{hash}";
        }        
    }


}

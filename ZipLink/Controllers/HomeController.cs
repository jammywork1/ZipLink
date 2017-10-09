using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZipLink.BusinessLogic;

namespace ZipLink.Controllers
{
    public class HomeController : Controller
    {
        private ZippedLinkFacade _linkFacade;

        public HomeController(ZippedLinkFacade linkFacade)
        {
            _linkFacade = linkFacade;
            //LinkFacade = this.ResolveService<ZippedLinkFacade>();
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{hash}")]
        public async Task<IActionResult> Index(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                return View();
            var model = await _linkFacade.GetByHash(hash);
            if (model != null)
            {
                await _linkFacade.IncrementLinkFollowByHash(hash);
                return Redirect(model.OriginalLink);
            }
            return View();
            
        }

    }
}

using System.Reflection;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ZipLink.DataAccessLayer;
using ZipLink.ServiceInterface;

namespace ZipLink
{
    public class AppHost : AppHostBase
    {
        private readonly IConfigurationRoot _configuration;

        public AppHost(IConfigurationRoot configuration) 
            : base("ZipLink", new Assembly[]
            {
                typeof(ZippedLinkService).GetTypeInfo().Assembly
            })
        {
            _configuration = configuration;
        }

        public override void Configure(Funq.Container container)
        {
            //ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            //ServiceStackController.CatchAllController = reqCtx => container.TryResolve<HomeController>();
            SetConfig(new HostConfig
            {
                //HandlerFactoryPath = "api",
                DebugMode = _configuration.GetValue<bool>("DebugMode", false),
                AddRedirectParamsToQueryString = true,
                UseCamelCase = true,
            });

            
#if DEBUG
            if (Config.DebugMode) 
            {
                container.Resolve<IZippedLinksRepository>().ReInitSchema();
            }
#else
            container.Resolve<IZippedLinksRepository>().InitSchema();
#endif
        }
    }
}
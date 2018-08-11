using Microsoft.AspNet.Identity.Owin;
using Shop.Service;
using Shop.Web.App_Start;
using Shop.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;

        public HomeController(IErrorService errorService): base(errorService)
        {
            _errorService = errorService;
        }

        [Route("TestMethod")]
        [HttpGet]
        public string TestMothod()
        {
            return "Hello, tedu member";
        }
    }
}

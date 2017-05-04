using System.Threading.Tasks;
using System.Web.Http;

namespace MessageEncryptor.WebApi.Controllers
{
    public class EncryptionController : ApiController
    {
        [HttpPost]
        public virtual async Task<IHttpActionResult> CreateImageWithMessage()
        {
            return Ok();
        }
    }
}

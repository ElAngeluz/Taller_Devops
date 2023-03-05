using Microsoft.AspNetCore.Mvc;

namespace taller.devops.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}

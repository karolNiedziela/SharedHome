using Microsoft.AspNetCore.Mvc;

namespace SharedHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}

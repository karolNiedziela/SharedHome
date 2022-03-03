using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SharedHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator = default!;

        protected IMediator Mediator
        {
            get
            {
                return _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
            }
        }
    }
}

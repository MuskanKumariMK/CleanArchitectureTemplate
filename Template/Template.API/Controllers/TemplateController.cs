using Microsoft.AspNetCore.Mvc;
namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Welcome()
        {
            return Ok("Welcome to the Clean Architecture Microservices Template!");
        }
    }
}

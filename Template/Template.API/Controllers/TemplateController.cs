using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using Template.Application.DTO.Request;
using Template.Application.DTO.Response;
using Template.Application.Restaurant.Commands;
using Template.Application.Restaurant.Queries;

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

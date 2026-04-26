using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Template.Application.Query.Welcome;
using Template.Applications.Common;
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
          /// <summary>
          /// Gets the welcome message 
          /// </summary>
          /// <summary>
          /// Gets the welcome message.
          /// </summary>
          [HttpGet("welcome")]
          [EnableRateLimiting("fixed")]
          [ProducesResponseType(typeof(ApiResponse<GetWelcomeMessageResult>), StatusCodes.Status200OK)]
          [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
          public async Task<IActionResult> Welcome()
          {
               // Send query to MediatR
               var result = await _mediator.Send(new GetWelcomeMessageQuery());

               // Wrap result in standardized response
               return Ok(ApiResponse<GetWelcomeMessageResult>.SuccessResponse(result));

          }
     }
}

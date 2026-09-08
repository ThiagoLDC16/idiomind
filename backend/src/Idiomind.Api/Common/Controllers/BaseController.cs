namespace Idiomind.Api.Common.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult MapResponse<T>(Response<T> response)
    {
        if (response.Data is not null)
            return StatusCode((int)response.Status, response.Data);

        if (response.Messages.Any())
            return StatusCode((int)response.Status, new
            {
                errors = response.Messages
            });

        return StatusCode((int)response.Status);
    }

    protected IActionResult MapResponse(Response response)
    {
        if (response.Messages.Any())
            return StatusCode((int)response.Status, new
            {
                errors = response.Messages
            });

        return StatusCode((int)response.Status);
    }
}
using Ardalis.Result;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineLibrary.API.Controllers
{

    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.Status == ResultStatus.NotFound)
                return NotFound(result.Errors);

            return BadRequest(result.Errors);
        }
    }
}

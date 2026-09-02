using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AINexus.Api.Controllers;

[ApiController]
public sealed class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error() => Problem(statusCode: 500, title: "An unexpected error occurred.");
}

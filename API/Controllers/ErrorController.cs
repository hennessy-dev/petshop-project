using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ErrorController
{
    [HttpGet]
    public IActionResult Error(int code)
    {
        return new ObjectResult(new ApiResponse(code));
    }
}
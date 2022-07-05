using Microsoft.AspNetCore.Mvc;

// Declares that a file is a controller for easy implmentation across new controllers
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

// Declares that a file is a controller for easy implmentation across new controllers
namespace API.Controllers
{
    [ServiceFilter(typeof(trackUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        
    }
}
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Ordering.API.Controller
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[ControllerBase]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
    }
}
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    // Author: Sefa Maril - Date: 05.08.2024
    // Description: Base controller class for API versioning and routing.

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
    }
}

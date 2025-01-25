using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.CustomAttributes;
using ECommerceAPI.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/app-services")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IAppService _applicationService;

        public ApplicationServicesController(IAppService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}

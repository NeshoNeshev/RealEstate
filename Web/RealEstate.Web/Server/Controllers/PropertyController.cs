using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Web.Shared.PropertyModels;
using RealEstate.Web.Shared.ViewModels;

namespace RealEstate.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService propertyService;
        public PropertyController(IPropertyService propertyService = null)
        {
            this.propertyService = propertyService;
        }
        [HttpGet("GetPropertiyById")]
        public async Task<PropertyViewModel> Get(string Id)
        {
            var model = await this.propertyService.Get(Id);

            return model;
        }
    }
}

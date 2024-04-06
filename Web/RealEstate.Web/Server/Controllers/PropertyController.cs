using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Web.Shared.InputModels;
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
        [HttpPost("SearchProperties")]
        public async Task<IEnumerable<PropertyViewModel>> SearchProperties([FromBody] IndexInputModel model)
        {
            var response = await this.propertyService.SearchProperties(model);

            return response;

            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Web.Shared;
using RealEstate.Web.Shared.PropertyTypeModels;
using RealEstate.Web.Shared.ViewModels;

namespace RealEstate.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        private readonly ITownService townService;
        private readonly IPropertyTypeService propertyTypeService;
        public IndexController(ITownService townService, IPropertyTypeService propertyTypeService)
        {
            this.townService = townService;
            this.propertyTypeService = propertyTypeService;
        }

        [HttpGet]
        public async Task<IndexViewModel> Get()
        {

            var indexModel = new IndexViewModel();
            indexModel.Towns = await this.townService.GetAll<TownViewModel>(null, false);
            indexModel.Types = await this.propertyTypeService.GetAll<PropertyTypeViewModel>(null, false);
            return indexModel;
        }
    }
}

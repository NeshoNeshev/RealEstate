using RealEstate.Web.Shared.PropertyTypeModels;

namespace RealEstate.Services
{
    public interface IPropertyTypeService
    {
        public Task<PropertyTypeViewModel> Get(string id);

        public Task<string> Create(PropertyTypeInputModel model);

        public Task<string> Update(PropertyTypeUpdateModel model);

        public Task Delete(string id);
    }
}

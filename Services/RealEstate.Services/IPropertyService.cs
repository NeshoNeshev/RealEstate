using RealEstate.Web.Shared;
using RealEstate.Web.Shared.PropertyModels;

namespace RealEstate.Services
{
    public interface IPropertyService
    {
        public Task<string> Create(PropertyInputModel model);

        public Task<bool> Delete(string properyId);

        public Task<PropertyViewModel> Get(string properyId);

        public Task Update (PropertyUpdateModel model);
        Task<IEnumerable<T>> GetAll<T>(string townId);
        public Task Requsts(RequestModel model);
        public Task Message(MessageModel model);
    }
}

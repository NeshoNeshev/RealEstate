using RealEstate.Web.Shared;
using RealEstate.Web.Shared.InputModels;
using RealEstate.Web.Shared.PropertyModels;

namespace RealEstate.Services
{
    public interface IPropertyService
    {
        public Task<IEnumerable<PropertyViewModel>> SearchProperties(IndexInputModel model);
        public Task<string> Create(PropertyInputModel model);

        public Task<bool> Delete(string properyId);

        public Task<PropertyViewModel> Get(string properyId);
        Task<IEnumerable<T>> GetTopProperties<T>(int? count = null);

        public Task Update (PropertyUpdateModel model);
        Task<IEnumerable<T>> GetAll<T>(string distinctId);
        public Task Requsts(RequestModel model);
        public Task Message(MessageModel model);
    }
}

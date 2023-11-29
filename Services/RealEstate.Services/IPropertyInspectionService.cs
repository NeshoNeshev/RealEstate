using RealEstate.Web.Shared.PropertyInspectionModels;

namespace RealEstate.Services
{
    public interface IPropertyInspectionService
    {
       public Task Create(PropertyInspectionInputModel model);
       public Task Delete(string id);
    }
}

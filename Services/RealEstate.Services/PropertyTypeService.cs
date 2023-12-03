using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Services.Mapping;
using RealEstate.Web.Shared.PropertyTypeModels;

namespace RealEstate.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IDistrictService districtService;
        private readonly ApplicationDbContext dbContext;

        public PropertyTypeService(IDistrictService districtService, ApplicationDbContext dbContext)
        {
            this.districtService = districtService;
            this.dbContext = dbContext;
        }

        public async Task<PropertyTypeViewModel> Get(string id)
        {

            var type = await this.dbContext.PropertyTypes.Where(x => x.Id == id).To<PropertyTypeViewModel>().FirstOrDefaultAsync();

            return type;
        }
        public async Task<string> Create(PropertyTypeInputModel model)
        {
            var district = await this.districtService.Get(model.DistrictId);
            if (district == null)
            {
                throw new InvalidOperationException($"Квартал с такова Id: {model.DistrictId}  не съществува");
            }
            var type = new PropertyType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name,
                DistrictId = model.DistrictId
            };
            await this.dbContext.PropertyTypes.AddAsync(type);
            await this.dbContext.SaveChangesAsync();

            return type.Id;
        }
        public async Task<string> Update(PropertyTypeUpdateModel model)
        {
            var district = await this.districtService.Get(model.DistrictId);
            var type =await this.dbContext.PropertyTypes.FirstOrDefaultAsync(x=>x.Id == model.PropertyTypeId);
            if (district == null || type == null)
            {
                throw new InvalidOperationException("Id: не може да е null");
            }
            type.Name = model.Name;
            type.DistrictId = model.DistrictId;

            this.dbContext.Update(type);
            await this.dbContext.SaveChangesAsync();

            return type.Id;
        }
        public async Task Delete(string id)
        {
            var type = await this.dbContext.PropertyTypes.FirstOrDefaultAsync(x => x.Id == id);

            type.IsDeleted = true;
            type.DeletedOn = DateTime.UtcNow;

            this.dbContext.PropertyTypes.Update(type);
            await this.dbContext.SaveChangesAsync();
        }
    }
}

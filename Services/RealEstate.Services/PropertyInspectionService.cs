using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Web.Shared.PropertyInspectionModels;

namespace RealEstate.Services
{
    public class PropertyInspectionService : IPropertyInspectionService
    {
        private readonly ApplicationDbContext dbContext;

        public PropertyInspectionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Create(PropertyInspectionInputModel model)
        {
            var property = await this.dbContext.Properties.FirstOrDefaultAsync(x=>x.Id == model.PropertyId);
            if (property == null)
            {
                throw new InvalidOperationException($"Property with this id :{model.PropertyId} not exist");
            }
            var propertyInspection = new PropertyInspection()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                Inquiry = model.Inquiry,
                Email = model.Email,
                Date = model.Date,
                Hour = model.Hour,
                PropertyId = model.PropertyId
            };
            await this.dbContext.PropertyInspections.AddAsync(propertyInspection);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var propertyInspection = await this.dbContext.PropertyInspections.FirstOrDefaultAsync(x => x.Id == id);
            if (propertyInspection == null)
            {
                throw new InvalidOperationException($"PropertyInspection with this id :{id} not exist");
            }
            propertyInspection.IsDeleted = true;
            propertyInspection.DeletedOn = DateTime.Now;

            this.dbContext.PropertyInspections.Update(propertyInspection);
            await this.dbContext.SaveChangesAsync();
        }
        public async Task<string> Update(string id)
        {
            var propertyInspection = await this.dbContext.PropertyInspections.FirstOrDefaultAsync(x => x.Id == id);
            if (propertyInspection == null)
            {
                return $"PropertyInspection with this id :{id} not exist" ;
            }
            propertyInspection.IsDeleted = false;

            this.dbContext.PropertyInspections.Update(propertyInspection);
            await this.dbContext.SaveChangesAsync();
            return propertyInspection.Id;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Services.Mapping;
using RealEstate.Web.Shared.PropertyModels;

namespace RealEstate.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly ApplicationDbContext dbContext;

        public PropertyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Create(PropertyInputModel model)
        {
            var user = await this.dbContext.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (user == null)
            {
                return "This user not exist";
            }
            var property = new Property()
            {
                Id = Guid.NewGuid().ToString(),
                Code = model.Code,
                Price = model.Price,
                Area = model.Area,
                Floor = model.Floor,
                Heating = model.Heating,
                FurnishedLevel = model.FurnishedLevel,
                Description = model.Description,
                Seen = 0,
                Statute = model.Statute,
                Status = model.Status,
                IsBuying = false,
                IsSolded = false,
                IsRental = false,
                UserId = user.Id,
                //add to ProperyType
            };
            await this.dbContext.Properties.AddAsync(property);
            await this.dbContext.SaveChangesAsync();

            return property.Id;
        }
        public async Task<bool> Delete(string properyId)
        {
            var property = await this.dbContext.Properties.FirstOrDefaultAsync(x => x.Id == properyId);
            if (property == null)
            {
                return false;
            }
            property.IsDeleted = true;
            property.DeletedOn = DateTime.UtcNow;

            this.dbContext.Update(property);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<PropertyViewModel> Get(string properyId)
        {
            //todo: fill PropertyViewModel
            var proppery = await this.dbContext.Properties.Where(x => x.Id == properyId).To<PropertyViewModel>().FirstOrDefaultAsync();
            return proppery;
        }
        public async Task Update(PropertyUpdateModel model)
        {
            var property = await this.dbContext.Properties.FirstOrDefaultAsync(x => x.Id == model.PropertyId);
            if (property == null)
            {
                throw new InvalidOperationException($"Property with this id: {model.PropertyId} not exist");
            }
            property.Price = model.Price;
            property.Area = model.Area;
            property.Floor = model.Floor;
            property.Heating = model.Heating;
            property.FurnishedLevel = model.FurnishedLevel;
            property.Description = model.Description;
            property.IsBuying = model.IsBuying;
            property.IsSolded = model.IsSolded;
            property.IsRental = model.IsRental;
            property.Statute = model.Statute;
            property.Status = model.Status;
            property.PropertyTypeId = model.PropertyTypeId;
            this.dbContext.Properties.Update(property);
            await this.dbContext.SaveChangesAsync();

        }
    }
}
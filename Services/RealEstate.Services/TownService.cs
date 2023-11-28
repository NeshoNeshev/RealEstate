﻿using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Services.Mapping;
using RealEstate.Web.Shared;
using System.Xml.Linq;

namespace RealEstate.Services
{
    public class TownService : ITownService
    {
        private readonly ApplicationDbContext dbContext;

        public TownService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<TownViewModel> GetTown(string townId)
        {
            var town = await this.dbContext.Towns.Where(x => x.Id == townId).To<TownViewModel>().FirstOrDefaultAsync();
            return town;
        }
        public async Task<string> Create(string name)
        {
            var existedTown = await this.dbContext.Towns.AnyAsync(x => x.Name == name);
            if (existedTown)
            {
                return $"Town with this {name} existed";
            }
            else
            {
                var town = new Town()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name
                };

                await this.dbContext.Towns.AddAsync(town);
                await this.dbContext.SaveChangesAsync();
                return town.Id;

            };
        }
        public async Task Update(string townId, string name)
        {
            var existedTown = await this.dbContext.Towns.FirstOrDefaultAsync(x => x.Id == townId);
            if (existedTown != null)
            {
                existedTown.Name = name;
                this.dbContext.Towns.Update(existedTown);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(string townId)
        {
            var existedTown = await this.dbContext.Towns.FirstOrDefaultAsync(x => x.Id == townId);
            if (existedTown == null)
            {
                return false;
            }
            else
            {
                existedTown.IsDeleted = true;
                existedTown.DeletedOn = DateTime.UtcNow;
                this.dbContext.Towns.Update(existedTown); 
                await this.dbContext.SaveChangesAsync();

                return true;
            }

        }
    }
}

using RealEstate.Web.Shared;

namespace RealEstate.Services
{
    public interface ITownService
    {
        public Task<TownViewModel> GetTown(string townId);

        public Task<string> Create(string name);

        public Task Update(string townId, string name);

        public Task<bool> Delete(string townId);
    }
}

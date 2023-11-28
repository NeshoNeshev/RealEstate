using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Services.Mapping;

namespace RealEstate.Web.Shared
{
    public class TownViewModel : IMapFrom<Town>
    {
        public string? Name { get; set; }
    }
}

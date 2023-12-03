using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Services.Mapping;

namespace RealEstate.Web.Shared.DistrictModels
{
    public class DistrictViewModel : IMapFrom<District>
    {
        public string? Name { get; set; };
    }
}

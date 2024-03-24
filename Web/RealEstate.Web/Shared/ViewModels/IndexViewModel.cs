

using RealEstate.Web.Shared.PropertyTypeModels;

namespace RealEstate.Web.Shared.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<TownViewModel> Towns { get; set; }

        public IEnumerable<PropertyTypeViewModel> Types { get; set; }
    }
}
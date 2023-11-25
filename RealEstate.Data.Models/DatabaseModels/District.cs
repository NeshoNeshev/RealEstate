using RealEstate.Data.Models.ApplicationModels;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class District : BaseDeletableModel<string>
    {
        public District()
        {
            this.PropertyTypes = new HashSet<PropertyType>();
        }

        public string? Name { get; set; }

        public string? TownId { get; set; }

        public Town? Town { get; set; }

        public virtual ICollection<PropertyType> PropertyTypes { get; set; }
    }
}

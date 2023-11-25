using RealEstate.Data.Models.ApplicationModels;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class PropertyType : BaseDeletableModel<string>
    {
        public PropertyType()
        {
            this.Properties = new HashSet<Property>();
        }

        public string? Name { get; set; }
        public string? DistrictId { get; set; }

        public District? District { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}

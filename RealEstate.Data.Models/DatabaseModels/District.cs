using RealEstate.Data.Models.ApplicationModels;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class District : BaseDeletableModel<string>
    {
        public District()
        {
            this.Properties = new HashSet<Property>();
        }

        public string? Name { get; set; }

        public string? TownId { get; set; }

        public Town? Town { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}

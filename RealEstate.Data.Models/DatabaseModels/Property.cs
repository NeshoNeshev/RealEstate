using RealEstate.Data.Models.ApplicationModels;
using RealEstate.Data.Models.Enumerations;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {

        }
        public int Code { get; set; }

        public double Price { get; set; }

        public double Area { get; set; }

        public int Floor { get; set; }

        public string Heating { get; set; }

        public string FurnishedLevel { get; set; }

        public string Description { get; set; }

        public int Seen { get; set; }

        public string Statute { get; set; }

        public Status Status { get; set; }

        public string? DistrictId { get; set; }

        public District? District { get; set; }

        public string PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }
    }
}

using RealEstate.Data.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Web.Shared.PropertyModels
{
    public class PropertyInputModel
    {
        [Required]
        public double Price { get; set; }

        [Required]
        public double Area { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public string? Heating { get; set; }

        [Required]
        public string? FurnishedLevel { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string Statute { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? PropertyTypeId { get; set; }

        public string? DistrictId { get; set; }

        public string TownId { get; set; }
    }
}

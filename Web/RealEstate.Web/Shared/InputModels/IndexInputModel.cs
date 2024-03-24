using System.ComponentModel.DataAnnotations;

namespace RealEstate.Web.Shared.InputModels
{
    public class IndexInputModel
    {

        public string? selectedDistrictId { get; set; }
        [Required(ErrorMessage = "Полето е задължително")]
        public string? propertyId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string? selectedTypeId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string? heating { get; set; }

        [Required(ErrorMessage = "Полето е задължително")] 
        public string? furnitureLevel { get; set; }

        [Required(ErrorMessage = "Полето е задължително")] 
        public string? selectedTown { get; set; }


        public string? from { get; set; }
      
        public string? to { get; set; }
    }
}

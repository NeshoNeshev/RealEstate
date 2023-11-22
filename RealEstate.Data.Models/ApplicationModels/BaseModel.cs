using RealEstate.Data.Models.BaseDeletableModels;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Data.Models.ApplicationModels
{
    public abstract class BaseModel<TKey> : IDateInfo
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

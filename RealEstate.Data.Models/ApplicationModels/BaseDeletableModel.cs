using RealEstate.Data.Models.BaseDeletableModels;

namespace RealEstate.Data.Models.ApplicationModels
{
    public abstract class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}

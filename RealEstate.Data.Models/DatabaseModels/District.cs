using RealEstate.Data.Models.ApplicationModels;

namespace RealEstate.Data.Models.DatabaseModels
{
    // таблица за базата данни
    public class District : BaseDeletableModel<string>
    {
        public District()
        {
            this.PropertyTypes = new HashSet<PropertyType>();
        }

        public string? Name { get; set; }

        // тези двете правят връзка към таблица град като казват,че в един квартал може да  е само в един град
        public string? TownId { get; set; }

        public Town? Town { get; set; }
        // връзка към таблица в базата прави,че в един квартал може да има много типове имоти
        public virtual ICollection<PropertyType> PropertyTypes { get; set; }
    }
}

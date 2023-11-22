﻿using RealEstate.Data.Models.ApplicationModels;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class Town : BaseDeletableModel<string>
    {
        public Town() => this.Districts = new HashSet<District>();

        public string? Name { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}

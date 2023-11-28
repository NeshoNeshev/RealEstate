﻿using RealEstate.Data.Models.ApplicationModels;
using RealEstate.Data.Models.Enumerations;

namespace RealEstate.Data.Models.DatabaseModels
{
    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Views = new HashSet<View>();
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

        public bool IsBuying { get; set; }

        public bool IsSolded { get; set; }

        public bool IsRental { get; set; }

        public string PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        public string? UserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public virtual ICollection<View> Views { get; set; }
    }
}
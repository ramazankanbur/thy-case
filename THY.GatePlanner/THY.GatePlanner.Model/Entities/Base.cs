using System;

namespace THY.GatePlanner.Model.Entities
{
    public abstract class Base
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}


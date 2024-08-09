using System;

namespace THY.GatePlanner.Model.Entities
{
    public abstract class Base
    {
        public Guid Id { get; set; } = new Guid();
        public bool IsDeleted { get; set; } = false;
        public int CreatedBy { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; } = null;
        public DateTime? ModifiedAt { get; set; } = null;
    }
}


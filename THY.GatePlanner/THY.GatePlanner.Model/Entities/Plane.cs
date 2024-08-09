using System;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Entities
{
    public class Plane : Base
    {
        public string Code { get; set; }
        public int PlaneStatus { get; set; }
        public int Size { get; set; }

        public ICollection<PlaneGate> PlaneGates { get; set; }

    }

    public class PlaneEntityConfiguration : IEntityTypeConfiguration<Plane>
    {

        public void Configure(EntityTypeBuilder<Plane> entity)
        {  
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THY.GatePlanner.Model.Enums;
using System.Reflection.Emit;

namespace THY.GatePlanner.Model.Entities
{
    public class PlaneGate : Base
    {
        public Guid PlaneId { get; set; }
        public Plane Plane { get; set; }
        public Guid GateId { get; set; }
        public Gate Gate { get; set; }
        public int PassengerOffboardingDuration { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class PlaneGateEntityConfiguration : IEntityTypeConfiguration<PlaneGate>
    {
        public void Configure(EntityTypeBuilder<PlaneGate> entity)
        {

            entity
                 .HasOne(pg => pg.Plane)
                .WithMany(p => p.PlaneGates)
                .HasForeignKey(pg => pg.PlaneId);

            entity
                .HasOne(pg => pg.Gate)
                .WithMany(g => g.PlaneGates)
                .HasForeignKey(pg => pg.GateId);
        }
    }

}

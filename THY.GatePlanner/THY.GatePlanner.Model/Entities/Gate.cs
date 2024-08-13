using System;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using THY.GatePlanner.Model.Enums;

namespace THY.GatePlanner.Model.Entities
{
	public class Gate : Base
	{
		public string Code { get; set; }
		public int Size { get; set; }
		public string Location { get; set; } //"x:y"
        public int GateStatus { get; set; }


        public ICollection<PlaneGate> PlaneGates { get; set; }
    }

    public class GateEntityConfiguration : IEntityTypeConfiguration<Gate>
    {
        public void Configure(EntityTypeBuilder<Gate> entity)
        {
            entity.HasData(
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code="GT001",
                     IsDeleted=false,
                      Location="10:9",
                       Size=SizeEnum.S.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT002",
                    IsDeleted = false,
                    Location = "5:8",
                    Size = SizeEnum.S.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT003",
                    IsDeleted = false,
                    Location = "12:1",
                    Size = SizeEnum.S.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT004",
                    IsDeleted = false,
                    Location = "8:8",
                    Size = SizeEnum.M.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT005",
                    IsDeleted = false,
                    Location = "11:3",
                    Size = SizeEnum.M.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT006",
                    IsDeleted = false,
                    Location = "3:9",
                    Size = SizeEnum.M.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT007",
                    IsDeleted = false,
                    Location = "7:3",
                    Size = SizeEnum.L.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT008",
                    IsDeleted = false,
                    Location = "10:10",
                    Size = SizeEnum.L.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT009",
                    IsDeleted = false,
                    Location = "10:13",
                    Size = SizeEnum.L.GetHashCode(),
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                  new Gate
                  {
                      Id = Guid.NewGuid(),
                      Code = "GT010",
                      IsDeleted = false,
                      Location = "6:3",
                      Size = SizeEnum.L.GetHashCode(),
                      CreatedBy = 0,
                      CreatedAt = DateTime.Now
                  }
               );
        }
    }
}


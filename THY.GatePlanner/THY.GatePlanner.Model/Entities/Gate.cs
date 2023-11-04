using System;
using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace THY.GatePlanner.Model.Entities
{
	public class Gate : Base
	{
		public string Code { get; set; }
		public string Size { get; set; }
		public string Location { get; set; } //"x:y"
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
                       Size="S",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT002",
                    IsDeleted = false,
                    Location = "5:8",
                    Size = "S",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT003",
                    IsDeleted = false,
                    Location = "12:1",
                    Size = "S",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT004",
                    IsDeleted = false,
                    Location = "8:8",
                    Size = "M",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT005",
                    IsDeleted = false,
                    Location = "11:3",
                    Size = "M",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT006",
                    IsDeleted = false,
                    Location = "3:9",
                    Size = "M",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT007",
                    IsDeleted = false,
                    Location = "7:3",
                    Size = "L",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT008",
                    IsDeleted = false,
                    Location = "10:10",
                    Size = "L",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                new Gate
                {
                    Id = Guid.NewGuid(),
                    Code = "GT009",
                    IsDeleted = false,
                    Location = "10:13",
                    Size = "L",
                    CreatedBy = 0,
                    CreatedAt = DateTime.Now
                },
                  new Gate
                  {
                      Id = Guid.NewGuid(),
                      Code = "GT010",
                      IsDeleted = false,
                      Location = "6:3",
                      Size = "L",
                      CreatedBy = 0,
                      CreatedAt = DateTime.Now
                  }
               );
        }
    }
}


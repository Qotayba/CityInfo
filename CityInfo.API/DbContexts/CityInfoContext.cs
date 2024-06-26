﻿using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfIntrest> PointOfIntrest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) 
        { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cityInfo2.db");
            base.OnConfiguring(optionsBuilder);
        }

    }
}

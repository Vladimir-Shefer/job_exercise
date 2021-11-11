using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Classes;




namespace Server
{
    public class DataContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        public string DbPath { get; private set; }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder )
        {
            
            // configures one-to-many relationship
            modelBuilder.Entity<Device>()
            .HasMany(p => p.Sensors)
            .WithOne(e => e.Device);

            // configures one-to-many relationship
            modelBuilder.Entity<Sensor>()
            .HasMany(p => p.Measurements)
            .WithOne(e => e.Sensor);
        }
    
   

    public DataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}DSM.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

    
}
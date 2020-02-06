using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EtteplanMORE.ServiceManual.ApplicationCore.Entities;

namespace EtteplanMORE.ServiceManual.ApplicationCore.DBContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }


        // DbSets or tables however you call them
        public DbSet<FactoryDevice> FactoryDevices { get; set; }
        public DbSet<RepairTask> RepairTasks { get; set; }


        /// <summary>
        /// Defines entities (and their constraints) for EntityFramework
        /// that work between DB and this REST-api
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<FactoryDevice>(entity =>
            {
                entity.HasKey(e => e.Id); // Set Id-field as primary key
                entity.Property(e => e.Name).IsRequired(); // Set other fields required
                entity.Property(e => e.Year).IsRequired();
                entity.Property(e => e.Type).IsRequired();
            });

            modelBuilder.Entity<RepairTask>(entity =>
            {
                entity.HasKey(e => e.TaskId); // Set taskId-field as primary key
                entity.Property(e => e.DeviceId).IsRequired(); // Set other fields required
                entity.Property(e => e.DateAdded).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(100); // Set max-length for comments to 100 chars
                entity.Property(e => e.Criticality).IsRequired();
                entity.Property(e => e.Completed).IsRequired(); 
            });
        }
    }
}

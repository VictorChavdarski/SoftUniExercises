using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        protected HospitalContext(DbContextOptions options)
            :base(options)
        {

        }


        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Hospital;Integrated Security=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnose>(x =>
            
                x.HasKey(x => x.DagnoseId)
            );

            modelBuilder.Entity<PatientMedicament>(x =>
            {
                x.HasKey(x => new { x.PatientId, x.MedicamentId });
            });

            modelBuilder.Entity<Visitation>(x =>

               x.HasKey(x => x.VisitationsId)
           );

            base.OnModelCreating(modelBuilder);
        }

    }
}

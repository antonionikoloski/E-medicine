using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_medicine.Models;
using E_medicine.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_medicine.Data
{
    public class E_medicineContext : IdentityDbContext<E_medicineUser>
    {
        public E_medicineContext (DbContextOptions<E_medicineContext> options)
            : base(options)
        {
        }

        public DbSet<E_medicine.Models.Disase>? Disase { get; set; }

        public DbSet<E_medicine.Models.Drugs>? Drugs { get; set; }

        public DbSet<E_medicine.Models.Company>? Company { get; set; }

        public DbSet<E_medicine.Models.Card>? Card { get; set; }
        public DbSet<DisaseDrugs>? DisaseDrugs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

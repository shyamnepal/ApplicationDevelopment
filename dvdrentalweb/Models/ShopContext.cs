﻿using dvdrentalweb.Models;
using Microsoft.EntityFrameworkCore;

namespace dvdrentalweb.Models

{
    public class ShopContext : DbContext
    {

        //public ShopContext() : base()
        //{
        //    Database.EnsureCreated();
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors{ get; set; }
        public DbSet<CastMember> CastMembers { get; set; }
        public DbSet<DVDCopy> DVDCopys{ get; set; }
        public DbSet<DVDTitle> DVDTitles{ get; set; }
        public DbSet<Loan> Loans{ get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Member> Members{ get; set; }
        public DbSet<MembershipCategory> MembershipCategories { get; set; }
        public DbSet<Producer> Producers{ get; set; }
        public DbSet<Studio> Studios { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
         {
             modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
         }*/
    }
}
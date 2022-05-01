﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dvdrentalweb.Models;

#nullable disable

namespace dvdrentalweb.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20220419015608_AddTablesToDB")]
    partial class AddTablesToDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("dvdrentalweb.Models.Actor", b =>
                {
                    b.Property<int>("ActorNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorNumber"), 1L, 1);

                    b.Property<string>("ActorFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActorSurName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorNumber");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("dvdrentalweb.Models.CastMember", b =>
                {
                    b.Property<int>("CastMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CastMemberId"), 1L, 1);

                    b.Property<int>("ActorNumber")
                        .HasColumnType("int");

                    b.Property<int>("DVDNumber")
                        .HasColumnType("int");

                    b.HasKey("CastMemberId");

                    b.ToTable("CastMembers");
                });

            modelBuilder.Entity("dvdrentalweb.Models.DVDCategory", b =>
                {
                    b.Property<int>("CategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryNumber"), 1L, 1);

                    b.Property<string>("AgeRestricted")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryNumber");

                    b.ToTable("DVDCategory");
                });

            modelBuilder.Entity("dvdrentalweb.Models.DVDCopy", b =>
                {
                    b.Property<int>("CopyNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CopyNumber"), 1L, 1);

                    b.Property<string>("DVDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatePurchase")
                        .HasColumnType("datetime2");

                    b.HasKey("CopyNumber");

                    b.ToTable("DVDCopys");
                });

            modelBuilder.Entity("dvdrentalweb.Models.DVDTitle", b =>
                {
                    b.Property<int>("DVDNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DVDNumber"), 1L, 1);

                    b.Property<int>("CategoryNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReleased")
                        .HasColumnType("datetime2");

                    b.Property<string>("DvdTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PenaltyCharge")
                        .HasColumnType("float");

                    b.Property<int>("ProducerNumber")
                        .HasColumnType("int");

                    b.Property<double>("StandardCharge")
                        .HasColumnType("float");

                    b.Property<int>("StudioNumber")
                        .HasColumnType("int");

                    b.HasKey("DVDNumber");

                    b.ToTable("DVDTitles");
                });

            modelBuilder.Entity("dvdrentalweb.Models.Loan", b =>
                {
                    b.Property<int>("LoanNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanNumber"), 1L, 1);

                    b.Property<int>("CopyNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDue")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReturned")
                        .HasColumnType("datetime2");

                    b.Property<int>("LoanTypeNumber")
                        .HasColumnType("int");

                    b.Property<int>("MemberNumber")
                        .HasColumnType("int");

                    b.HasKey("LoanNumber");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("dvdrentalweb.Models.LoanType", b =>
                {
                    b.Property<int>("LoanTypeNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanTypeNumber"), 1L, 1);

                    b.Property<int>("LoanDuration")
                        .HasColumnType("int");

                    b.Property<string>("Loantype")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoanTypeNumber");

                    b.ToTable("LoanTypes");
                });

            modelBuilder.Entity("dvdrentalweb.Models.Member", b =>
                {
                    b.Property<int>("MemberNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberNumber"), 1L, 1);

                    b.Property<string>("MemberAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MemberDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemberFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberLastNamae")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembershipCategoryNumber")
                        .HasColumnType("int");

                    b.HasKey("MemberNumber");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("dvdrentalweb.Models.MembershipCategory", b =>
                {
                    b.Property<int>("MembershipCategoryNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MembershipCategoryNumber"), 1L, 1);

                    b.Property<string>("MembershipCategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MembershipCategoryTotalLoans")
                        .HasColumnType("int");

                    b.HasKey("MembershipCategoryNumber");

                    b.ToTable("MembershipCategories");
                });

            modelBuilder.Entity("dvdrentalweb.Models.Producer", b =>
                {
                    b.Property<int>("ProducerNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProducerNumber"), 1L, 1);

                    b.Property<string>("ProducerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProducerNumber");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("dvdrentalweb.Models.Studio", b =>
                {
                    b.Property<int>("StudioNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudioNumber"), 1L, 1);

                    b.Property<string>("StudioName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudioNumber");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("dvdrentalweb.Models.User", b =>
                {
                    b.Property<int>("UserNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserNumber"), 1L, 1);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserNumber");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
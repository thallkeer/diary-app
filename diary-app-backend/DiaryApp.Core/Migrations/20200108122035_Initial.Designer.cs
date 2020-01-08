﻿// <auto-generated />
using System;
using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiaryApp.Core.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200108122035_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiaryApp.Core.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DiaryApp.Core.DesiresArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Header")
                        .IsRequired();

                    b.Property<int>("PageID");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.ToTable("DesiresAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.EventItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<bool>("FullDay");

                    b.Property<int>("OwnerID");

                    b.Property<string>("Subject");

                    b.Property<string>("Url");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DiaryApp.Core.EventList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DesiresAreaID");

                    b.Property<int?>("IdeasAreaID");

                    b.Property<int>("PageID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("DesiresAreaID");

                    b.HasIndex("IdeasAreaID")
                        .IsUnique()
                        .HasFilter("[IdeasAreaID] IS NOT NULL");

                    b.HasIndex("PageID");

                    b.ToTable("EventLists");
                });

            modelBuilder.Entity("DiaryApp.Core.GoalsArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Header")
                        .IsRequired();

                    b.Property<int>("PageID");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.ToTable("GoalsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.HabitsTracker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GoalName")
                        .IsRequired();

                    b.Property<int>("GoalsAreaID");

                    b.Property<int>("Month");

                    b.Property<string>("SelectedDays");

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.HasAlternateKey("Year", "Month", "GoalName");

                    b.HasIndex("GoalsAreaID");

                    b.ToTable("HabitsTrackers");
                });

            modelBuilder.Entity("DiaryApp.Core.IdeasArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Header")
                        .IsRequired();

                    b.Property<int>("PageID");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.ToTable("IdeasAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.PageBase", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Month");

                    b.Property<string>("UserId");

                    b.Property<int>("Year");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("PageBase");

                    b.HasDiscriminator<string>("Discriminator").HasValue("PageBase");
                });

            modelBuilder.Entity("DiaryApp.Core.PurchasesArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Header")
                        .IsRequired();

                    b.Property<int>("PageID");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.ToTable("PurchasesAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.TodoItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Done");

                    b.Property<int>("OwnerID");

                    b.Property<string>("Subject");

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("DiaryApp.Core.TodoList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PageID");

                    b.Property<int?>("PurchasesAreaID");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.HasIndex("PageID");

                    b.HasIndex("PurchasesAreaID");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DiaryApp.Core.MainPage", b =>
                {
                    b.HasBaseType("DiaryApp.Core.PageBase");

                    b.Property<int?>("EventListID");

                    b.Property<int?>("TodoListID");

                    b.HasIndex("EventListID");

                    b.HasIndex("TodoListID");

                    b.HasDiscriminator().HasValue("MainPage");
                });

            modelBuilder.Entity("DiaryApp.Core.MonthPage", b =>
                {
                    b.HasBaseType("DiaryApp.Core.PageBase");

                    b.Property<int?>("DesiresAreaID");

                    b.Property<int?>("GoalsAreaID");

                    b.Property<int?>("IdeasAreaID");

                    b.Property<int?>("PurchasesAreaID");

                    b.HasIndex("DesiresAreaID");

                    b.HasIndex("GoalsAreaID");

                    b.HasIndex("IdeasAreaID");

                    b.HasIndex("PurchasesAreaID");

                    b.HasDiscriminator().HasValue("MonthPage");
                });

            modelBuilder.Entity("DiaryApp.Core.DesiresArea", b =>
                {
                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.EventItem", b =>
                {
                    b.HasOne("DiaryApp.Core.EventList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.EventList", b =>
                {
                    b.HasOne("DiaryApp.Core.DesiresArea", "DesiresArea")
                        .WithMany("DesiresLists")
                        .HasForeignKey("DesiresAreaID");

                    b.HasOne("DiaryApp.Core.IdeasArea", "IdeasArea")
                        .WithOne("IdeasList")
                        .HasForeignKey("DiaryApp.Core.EventList", "IdeasAreaID");

                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.GoalsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.HabitsTracker", b =>
                {
                    b.HasOne("DiaryApp.Core.GoalsArea", "GoalsArea")
                        .WithMany("GoalsLists")
                        .HasForeignKey("GoalsAreaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.IdeasArea", b =>
                {
                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.PageBase", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DiaryApp.Core.PurchasesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.TodoItem", b =>
                {
                    b.HasOne("DiaryApp.Core.TodoList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.TodoList", b =>
                {
                    b.HasOne("DiaryApp.Core.PageBase", "Page")
                        .WithMany()
                        .HasForeignKey("PageID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiaryApp.Core.PurchasesArea", "PurchasesArea")
                        .WithMany("PurchasesLists")
                        .HasForeignKey("PurchasesAreaID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DiaryApp.Core.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DiaryApp.Core.MainPage", b =>
                {
                    b.HasOne("DiaryApp.Core.EventList", "ImportantEvents")
                        .WithMany()
                        .HasForeignKey("EventListID");

                    b.HasOne("DiaryApp.Core.TodoList", "ThingsTodo")
                        .WithMany()
                        .HasForeignKey("TodoListID");
                });

            modelBuilder.Entity("DiaryApp.Core.MonthPage", b =>
                {
                    b.HasOne("DiaryApp.Core.DesiresArea", "DesiresArea")
                        .WithMany()
                        .HasForeignKey("DesiresAreaID");

                    b.HasOne("DiaryApp.Core.GoalsArea", "GoalsArea")
                        .WithMany()
                        .HasForeignKey("GoalsAreaID");

                    b.HasOne("DiaryApp.Core.IdeasArea", "IdeasArea")
                        .WithMany()
                        .HasForeignKey("IdeasAreaID");

                    b.HasOne("DiaryApp.Core.PurchasesArea", "PurchasesArea")
                        .WithMany()
                        .HasForeignKey("PurchasesAreaID");
                });
#pragma warning restore 612, 618
        }
    }
}

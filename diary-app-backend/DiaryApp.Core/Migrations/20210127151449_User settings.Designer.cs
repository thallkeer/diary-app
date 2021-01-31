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
    [Migration("20210127151449_User settings")]
    partial class Usersettings
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DiaryApp.Core.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.CommonList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CommonLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.DesiresArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("DesiresAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.DesiresList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID");

                    b.HasIndex("ListID");

                    b.ToTable("DesireLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("EventLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.GoalsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("GoalsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.HabitDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("HabitTrackerId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HabitTrackerId");

                    b.ToTable("HabitDays");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.HabitTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("GoalsAreaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoalsAreaID");

                    b.ToTable("HabitTrackers");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.IdeasArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("IdeasAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.IdeasList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID")
                        .IsUnique();

                    b.HasIndex("ListID");

                    b.ToTable("IdeaLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ImportantEventsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ImportantEventsID")
                        .HasColumnType("int");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImportantEventsID");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("ImportantEventsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ImportantThingsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ImportantThingsID")
                        .HasColumnType("int");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImportantThingsID");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("ImportantThingsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("ListItems");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MainPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Year", "Month")
                        .IsUnique();

                    b.ToTable("MainPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MonthPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Year", "Month")
                        .IsUnique();

                    b.ToTable("MonthPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreaTransferSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("TransferDesiresArea")
                        .HasColumnType("bit");

                    b.Property<bool>("TransferGoalsArea")
                        .HasColumnType("bit");

                    b.Property<bool>("TransferIdeasArea")
                        .HasColumnType("bit");

                    b.Property<bool>("TransferPurchasesArea")
                        .HasColumnType("bit");

                    b.Property<int>("UserSettingsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId")
                        .IsUnique();

                    b.ToTable("PageAreaTransferSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PurchaseList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID");

                    b.HasIndex("ListID");

                    b.ToTable("PurchaseLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PurchasesArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("PurchasesAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.DesiresArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.MonthPage", "Page")
                        .WithOne("DesiresArea")
                        .HasForeignKey("DiaryApp.Core.Models.DesiresArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.DesiresList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.DesiresArea", "AreaOwner")
                        .WithMany("DesiresLists")
                        .HasForeignKey("AreaOwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.EventList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.GoalsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.MonthPage", "Page")
                        .WithOne("GoalsArea")
                        .HasForeignKey("DiaryApp.Core.Models.GoalsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.HabitDay", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.HabitTracker", "HabitTracker")
                        .WithMany("SelectedDays")
                        .HasForeignKey("HabitTrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HabitTracker");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.HabitTracker", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.GoalsArea", "GoalsArea")
                        .WithMany("GoalLists")
                        .HasForeignKey("GoalsAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoalsArea");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.IdeasArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.MonthPage", "Page")
                        .WithOne("IdeasArea")
                        .HasForeignKey("DiaryApp.Core.Models.IdeasArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.IdeasList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.IdeasArea", "AreaOwner")
                        .WithOne("IdeasList")
                        .HasForeignKey("DiaryApp.Core.Models.IdeasList", "AreaOwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ImportantEventsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.EventList", "ImportantEvents")
                        .WithMany()
                        .HasForeignKey("ImportantEventsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.MainPage", "Page")
                        .WithOne("ImportantEventsArea")
                        .HasForeignKey("DiaryApp.Core.Models.ImportantEventsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantEvents");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ImportantThingsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "ImportantThings")
                        .WithMany()
                        .HasForeignKey("ImportantThingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.MainPage", "Page")
                        .WithOne("ImportantThingsArea")
                        .HasForeignKey("DiaryApp.Core.Models.ImportantThingsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantThings");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.ListItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.CommonList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MainPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MonthPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreaTransferSettings", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.UserSettings", "UserSettings")
                        .WithOne("PageAreaTransferSettings")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreaTransferSettings", "UserSettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PurchaseList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.PurchasesArea", "AreaOwner")
                        .WithMany("PurchasesLists")
                        .HasForeignKey("AreaOwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.TodoList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PurchasesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.MonthPage", "Page")
                        .WithOne("PurchasesArea")
                        .HasForeignKey("DiaryApp.Core.Models.PurchasesArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.UserSettings", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.AppUser", "User")
                        .WithOne("Settings")
                        .HasForeignKey("DiaryApp.Core.Models.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.AppUser", b =>
                {
                    b.Navigation("Settings");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.CommonList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.DesiresArea", b =>
                {
                    b.Navigation("DesiresLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.GoalsArea", b =>
                {
                    b.Navigation("GoalLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.HabitTracker", b =>
                {
                    b.Navigation("SelectedDays");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.IdeasArea", b =>
                {
                    b.Navigation("IdeasList");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MainPage", b =>
                {
                    b.Navigation("ImportantEventsArea")
                        .IsRequired();

                    b.Navigation("ImportantThingsArea")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Models.MonthPage", b =>
                {
                    b.Navigation("DesiresArea")
                        .IsRequired();

                    b.Navigation("GoalsArea")
                        .IsRequired();

                    b.Navigation("IdeasArea")
                        .IsRequired();

                    b.Navigation("PurchasesArea")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PurchasesArea", b =>
                {
                    b.Navigation("PurchasesLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.UserSettings", b =>
                {
                    b.Navigation("PageAreaTransferSettings");
                });
#pragma warning restore 612, 618
        }
    }
}

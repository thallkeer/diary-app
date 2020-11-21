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
    [Migration("20200920143457_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("DiaryApp.Core.AppUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiaryApp.Core.CommonList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("CommonLists");
                });

            modelBuilder.Entity("DiaryApp.Core.EventItem", b =>
                {
                    b.Property<int>("ID")
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

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DiaryApp.Core.HabitsTracker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("GoalsAreaID")
                        .HasColumnType("int");

                    b.Property<string>("SelectedDays")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("GoalsAreaID");

                    b.ToTable("HabitsTrackers");
                });

            modelBuilder.Entity("DiaryApp.Core.ListItem", b =>
                {
                    b.Property<int>("ID")
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

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("ListItems");
                });

            modelBuilder.Entity("DiaryApp.Core.MainPage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("MainPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("EventLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.DesiresList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DesiresAreaID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DesiresAreaID");

                    b.HasIndex("ListID");

                    b.ToTable("DesiresLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.IdeasList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("IdeasAreaID")
                        .HasColumnType("int");

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdeasAreaID")
                        .IsUnique();

                    b.HasIndex("ListID");

                    b.ToTable("IdeasLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.PurchasesList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.Property<int>("PurchasesAreaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ListID");

                    b.HasIndex("PurchasesAreaID");

                    b.ToTable("PurchasesLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.WeekPlansList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ListID")
                        .HasColumnType("int");

                    b.Property<int>("WeekPlansAreaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ListID");

                    b.HasIndex("WeekPlansAreaID")
                        .IsUnique();

                    b.ToTable("WeekPlansLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.DesiresArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("DesiresAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.GoalsArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("GoalsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.IdeasArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("IdeasAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.ImportantEventsArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ImportantEventsID")
                        .HasColumnType("int");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ImportantEventsID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("ImportantEventsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.ImportantThingsArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ImportantThingsID")
                        .HasColumnType("int");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ImportantThingsID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("ImportantThingsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.NotesArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("NotesAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.PurchasesArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("PurchasesAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.WeekPlansArea", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PageID")
                        .HasColumnType("int");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PageID")
                        .IsUnique();

                    b.ToTable("WeekPlansAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Pages.WeekPage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("WeekPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoList", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.WeekDay", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Day")
                        .HasColumnType("date");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeekPlansAreaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("WeekPlansAreaID");

                    b.ToTable("WeekDays");
                });

            modelBuilder.Entity("DiaryApp.Core.MonthPage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("MonthPages");
                });

            modelBuilder.Entity("DiaryApp.Core.TodoItem", b =>
                {
                    b.Property<int>("ID")
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

                    b.HasKey("ID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("DiaryApp.Core.EventItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.EventList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.HabitsTracker", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.PageAreas.GoalsArea", "GoalsArea")
                        .WithMany("GoalsLists")
                        .HasForeignKey("GoalsAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoalsArea");
                });

            modelBuilder.Entity("DiaryApp.Core.ListItem", b =>
                {
                    b.HasOne("DiaryApp.Core.CommonList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.MainPage", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.DesiresList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.PageAreas.DesiresArea", "DesiresArea")
                        .WithMany("DesiresLists")
                        .HasForeignKey("DesiresAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DesiresArea");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.IdeasList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.PageAreas.IdeasArea", "IdeasArea")
                        .WithOne("IdeasList")
                        .HasForeignKey("DiaryApp.Core.Models.Lists.IdeasList", "IdeasAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdeasArea");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.PurchasesList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.PageAreas.PurchasesArea", "PurchasesArea")
                        .WithMany("PurchasesLists")
                        .HasForeignKey("PurchasesAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");

                    b.Navigation("PurchasesArea");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Lists.WeekPlansList", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Models.PageAreas.WeekPlansArea", "WeekPlansArea")
                        .WithOne("WeekPlansList")
                        .HasForeignKey("DiaryApp.Core.Models.Lists.WeekPlansList", "WeekPlansAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("List");

                    b.Navigation("WeekPlansArea");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.DesiresArea", b =>
                {
                    b.HasOne("DiaryApp.Core.MonthPage", "Page")
                        .WithOne("DesiresArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.DesiresArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.GoalsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.MonthPage", "Page")
                        .WithOne("GoalsArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.GoalsArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.IdeasArea", b =>
                {
                    b.HasOne("DiaryApp.Core.MonthPage", "Page")
                        .WithOne("IdeasArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.IdeasArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.ImportantEventsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.EventList", "ImportantEvents")
                        .WithMany()
                        .HasForeignKey("ImportantEventsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.MainPage", "Page")
                        .WithOne("ImportantEvents")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.ImportantEventsArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantEvents");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.ImportantThingsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "ImportantThings")
                        .WithMany()
                        .HasForeignKey("ImportantThingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.MainPage", "Page")
                        .WithOne("ImportantThings")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.ImportantThingsArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantThings");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.NotesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.Pages.WeekPage", "Page")
                        .WithOne("NotesArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.NotesArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.PurchasesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.MonthPage", "Page")
                        .WithOne("PurchasesArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.PurchasesArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.WeekPlansArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.Pages.WeekPage", "Page")
                        .WithOne("WeekPlansArea")
                        .HasForeignKey("DiaryApp.Core.Models.PageAreas.WeekPlansArea", "PageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Pages.WeekPage", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.WeekDay", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.PageAreas.WeekPlansArea", "WeekPlansArea")
                        .WithMany("WeekDays")
                        .HasForeignKey("WeekPlansAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeekPlansArea");
                });

            modelBuilder.Entity("DiaryApp.Core.MonthPage", b =>
                {
                    b.HasOne("DiaryApp.Core.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.TodoItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Models.TodoList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.CommonList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.MainPage", b =>
                {
                    b.Navigation("ImportantEvents")
                        .IsRequired();

                    b.Navigation("ImportantThings")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Models.EventList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.DesiresArea", b =>
                {
                    b.Navigation("DesiresLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.GoalsArea", b =>
                {
                    b.Navigation("GoalsLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.IdeasArea", b =>
                {
                    b.Navigation("IdeasList");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.PurchasesArea", b =>
                {
                    b.Navigation("PurchasesLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Models.PageAreas.WeekPlansArea", b =>
                {
                    b.Navigation("WeekDays");

                    b.Navigation("WeekPlansList")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Models.Pages.WeekPage", b =>
                {
                    b.Navigation("NotesArea")
                        .IsRequired();

                    b.Navigation("WeekPlansArea")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Models.TodoList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.MonthPage", b =>
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
#pragma warning restore 612, 618
        }
    }
}

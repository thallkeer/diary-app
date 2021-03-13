﻿// <auto-generated />
using System;
using DiaryApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DiaryApp.Core.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210313101607_Event location")]
    partial class Eventlocation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DiaryApp.Core.Entities.CommonList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("CommonLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DiaryLists.HabitTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("GoalsAreaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoalsAreaId");

                    b.ToTable("HabitTrackers");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DiaryLists.TodoList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TodoLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<int>("OwnerID")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("EventLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("HabitTrackerId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HabitTrackerId");

                    b.ToTable("HabitDays");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("OwnerID")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("ListItems");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.DesiresList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerId");

                    b.HasIndex("ListID");

                    b.ToTable("DesireLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.IdeasList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerId")
                        .IsUnique();

                    b.HasIndex("ListID");

                    b.ToTable("IdeaLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.PurchaseList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerId")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerId");

                    b.HasIndex("ListID");

                    b.ToTable("PurchaseLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MainPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Year", "Month")
                        .IsUnique();

                    b.ToTable("MainPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Notifications.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("date");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.DesiresArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("DesiresAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.GoalsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("GoalsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.IdeasArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("IdeasAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.ImportantEventsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ImportantEventsId")
                        .HasColumnType("integer");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ImportantEventsId");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("ImportantEventsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.ImportantThingsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ImportantThingsID")
                        .HasColumnType("integer");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ImportantThingsID");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("ImportantThingsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.PurchasesArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("PurchasesAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Pages.MonthPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Year", "Month")
                        .IsUnique();

                    b.ToTable("MonthPages");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Done")
                        .HasColumnType("boolean");

                    b.Property<int>("OwnerID")
                        .HasColumnType("integer");

                    b.Property<string>("Subject")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<long?>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.NotificationSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.Property<TimeSpan>("NotifyAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("interval")
                        .HasDefaultValue(new TimeSpan(0, 10, 0, 0, 0));

                    b.Property<bool>("NotifyDayBefore")
                        .HasColumnType("boolean");

                    b.Property<int>("UserSettingsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId")
                        .IsUnique();

                    b.ToTable("NotificationSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.PageAreaTransferSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("TransferDesiresArea")
                        .HasColumnType("boolean");

                    b.Property<bool>("TransferGoalsArea")
                        .HasColumnType("boolean");

                    b.Property<bool>("TransferIdeasArea")
                        .HasColumnType("boolean");

                    b.Property<bool>("TransferPurchasesArea")
                        .HasColumnType("boolean");

                    b.Property<int>("UserSettingsId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserSettingsId")
                        .IsUnique();

                    b.ToTable("PageAreaTransferSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DiaryLists.HabitTracker", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.PageAreas.GoalsArea", "GoalsArea")
                        .WithMany("GoalLists")
                        .HasForeignKey("GoalsAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoalsArea");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.EventList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitDay", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.DiaryLists.HabitTracker", "HabitTracker")
                        .WithMany("SelectedDays")
                        .HasForeignKey("HabitTrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HabitTracker");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.CommonList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.DesiresList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.PageAreas.DesiresArea", "AreaOwner")
                        .WithMany("DesiresLists")
                        .HasForeignKey("AreaOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.IdeasList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.PageAreas.IdeasArea", "AreaOwner")
                        .WithOne("IdeasList")
                        .HasForeignKey("DiaryApp.Core.Entities.ListWrappers.IdeasList", "AreaOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.CommonList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ListWrappers.PurchaseList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.PageAreas.PurchasesArea", "AreaOwner")
                        .WithMany("PurchasesLists")
                        .HasForeignKey("AreaOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.DiaryLists.TodoList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MainPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Users.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Notifications.Notification", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.EventItem", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.Users.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.DesiresArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Pages.MonthPage", "Page")
                        .WithOne("DesiresArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.DesiresArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.GoalsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Pages.MonthPage", "Page")
                        .WithOne("GoalsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.GoalsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.IdeasArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Pages.MonthPage", "Page")
                        .WithOne("IdeasArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.IdeasArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.ImportantEventsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.EventList", "ImportantEvents")
                        .WithMany()
                        .HasForeignKey("ImportantEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.MainPage", "Page")
                        .WithOne("ImportantEventsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.ImportantEventsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantEvents");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.ImportantThingsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.DiaryLists.TodoList", "ImportantThings")
                        .WithMany()
                        .HasForeignKey("ImportantThingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.MainPage", "Page")
                        .WithOne("ImportantThingsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.ImportantThingsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantThings");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.PurchasesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Pages.MonthPage", "Page")
                        .WithOne("PurchasesArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PageAreas.PurchasesArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Pages.MonthPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Users.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.TodoItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.DiaryLists.TodoList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.NotificationSettings", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Users.Settings.UserSettings", "UserSettings")
                        .WithOne("NotificationSettings")
                        .HasForeignKey("DiaryApp.Core.Entities.Users.Settings.NotificationSettings", "UserSettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.PageAreaTransferSettings", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Users.Settings.UserSettings", "UserSettings")
                        .WithOne("PageAreaTransferSettings")
                        .HasForeignKey("DiaryApp.Core.Entities.Users.Settings.PageAreaTransferSettings", "UserSettingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSettings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.UserSettings", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.Users.AppUser", "User")
                        .WithOne("Settings")
                        .HasForeignKey("DiaryApp.Core.Entities.Users.Settings.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.CommonList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DiaryLists.HabitTracker", b =>
                {
                    b.Navigation("SelectedDays");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DiaryLists.TodoList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MainPage", b =>
                {
                    b.Navigation("ImportantEventsArea")
                        .IsRequired();

                    b.Navigation("ImportantThingsArea")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.DesiresArea", b =>
                {
                    b.Navigation("DesiresLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.GoalsArea", b =>
                {
                    b.Navigation("GoalLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.IdeasArea", b =>
                {
                    b.Navigation("IdeasList");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PageAreas.PurchasesArea", b =>
                {
                    b.Navigation("PurchasesLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Pages.MonthPage", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.AppUser", b =>
                {
                    b.Navigation("Settings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.UserSettings", b =>
                {
                    b.Navigation("NotificationSettings");

                    b.Navigation("PageAreaTransferSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
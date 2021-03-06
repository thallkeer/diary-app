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
    [Migration("20210223075317_Link notifications with events")]
    partial class Linknotificationswithevents
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("DiaryApp.Core.Entities.AppUser", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.DesiresArea", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.DesiresList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID");

                    b.HasIndex("ListID");

                    b.ToTable("DesireLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

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

            modelBuilder.Entity("DiaryApp.Core.Entities.GoalsArea", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitTracker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GoalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("GoalsAreaID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GoalsAreaID");

                    b.ToTable("HabitTrackers");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.IdeasArea", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.IdeasList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID")
                        .IsUnique();

                    b.HasIndex("ListID");

                    b.ToTable("IdeaLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ImportantEventsArea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ImportantEventsID")
                        .HasColumnType("integer");

                    b.Property<int>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ImportantEventsID");

                    b.HasIndex("PageId")
                        .IsUnique();

                    b.ToTable("ImportantEventsAreas");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ImportantThingsArea", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.MonthPage", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.Notifications.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("timestamp without time zone");

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

            modelBuilder.Entity("DiaryApp.Core.Entities.PurchaseList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AreaOwnerID")
                        .HasColumnType("integer");

                    b.Property<int>("ListID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AreaOwnerID");

                    b.HasIndex("ListID");

                    b.ToTable("PurchaseLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PurchasesArea", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.TodoList", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.Users.Settings.NotificationSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsActivated")
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

            modelBuilder.Entity("DiaryApp.Core.Entities.DesiresArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.MonthPage", "Page")
                        .WithOne("DesiresArea")
                        .HasForeignKey("DiaryApp.Core.Entities.DesiresArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DesiresList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.DesiresArea", "AreaOwner")
                        .WithMany("DesiresLists")
                        .HasForeignKey("AreaOwnerID")
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

            modelBuilder.Entity("DiaryApp.Core.Entities.EventItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.EventList", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.GoalsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.MonthPage", "Page")
                        .WithOne("GoalsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.GoalsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitDay", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.HabitTracker", "HabitTracker")
                        .WithMany("SelectedDays")
                        .HasForeignKey("HabitTrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HabitTracker");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitTracker", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.GoalsArea", "GoalsArea")
                        .WithMany("GoalLists")
                        .HasForeignKey("GoalsAreaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GoalsArea");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.IdeasArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.MonthPage", "Page")
                        .WithOne("IdeasArea")
                        .HasForeignKey("DiaryApp.Core.Entities.IdeasArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.IdeasList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.IdeasArea", "AreaOwner")
                        .WithOne("IdeasList")
                        .HasForeignKey("DiaryApp.Core.Entities.IdeasList", "AreaOwnerID")
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

            modelBuilder.Entity("DiaryApp.Core.Entities.ImportantEventsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.EventList", "ImportantEvents")
                        .WithMany()
                        .HasForeignKey("ImportantEventsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.MainPage", "Page")
                        .WithOne("ImportantEventsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.ImportantEventsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantEvents");

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.ImportantThingsArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.TodoList", "ImportantThings")
                        .WithMany()
                        .HasForeignKey("ImportantThingsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.MainPage", "Page")
                        .WithOne("ImportantThingsArea")
                        .HasForeignKey("DiaryApp.Core.Entities.ImportantThingsArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImportantThings");

                    b.Navigation("Page");
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

            modelBuilder.Entity("DiaryApp.Core.Entities.MainPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MonthPage", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.AppUser", "User")
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

                    b.HasOne("DiaryApp.Core.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PurchaseList", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.PurchasesArea", "AreaOwner")
                        .WithMany("PurchasesLists")
                        .HasForeignKey("AreaOwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiaryApp.Core.Entities.TodoList", "List")
                        .WithMany()
                        .HasForeignKey("ListID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaOwner");

                    b.Navigation("List");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.PurchasesArea", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.MonthPage", "Page")
                        .WithOne("PurchasesArea")
                        .HasForeignKey("DiaryApp.Core.Entities.PurchasesArea", "PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.TodoItem", b =>
                {
                    b.HasOne("DiaryApp.Core.Entities.TodoList", "Owner")
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
                    b.HasOne("DiaryApp.Core.Entities.AppUser", "User")
                        .WithOne("Settings")
                        .HasForeignKey("DiaryApp.Core.Entities.Users.Settings.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.AppUser", b =>
                {
                    b.Navigation("Settings");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.CommonList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.DesiresArea", b =>
                {
                    b.Navigation("DesiresLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.EventList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.GoalsArea", b =>
                {
                    b.Navigation("GoalLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.HabitTracker", b =>
                {
                    b.Navigation("SelectedDays");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.IdeasArea", b =>
                {
                    b.Navigation("IdeasList");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MainPage", b =>
                {
                    b.Navigation("ImportantEventsArea")
                        .IsRequired();

                    b.Navigation("ImportantThingsArea")
                        .IsRequired();
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.MonthPage", b =>
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

            modelBuilder.Entity("DiaryApp.Core.Entities.PurchasesArea", b =>
                {
                    b.Navigation("PurchasesLists");
                });

            modelBuilder.Entity("DiaryApp.Core.Entities.TodoList", b =>
                {
                    b.Navigation("Items");
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

﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace STS.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210708101114_UpdateEntityAttribute")]
    partial class UpdateEntityAttribute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Hotline")
                        .HasColumnType("text");

                    b.Property<string>("LogoImg")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Data.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Entities.ShiftAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("MealEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("MealStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("integer");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("StoreId");

                    b.HasIndex("Username");

                    b.ToTable("ShiftAssignments");
                });

            modelBuilder.Entity("Data.Entities.ShiftAttendance", b =>
                {
                    b.Property<int>("ShiftAssignmentId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeCheckIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeCheckOut")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ShiftAssignmentId");

                    b.ToTable("ShiftAttendances");
                });

            modelBuilder.Entity("Data.Entities.ShiftLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<int>("ShiftAssignmentId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ShiftAssignmentId");

                    b.ToTable("ShiftLogs");
                });

            modelBuilder.Entity("Data.Entities.ShiftRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("PreferSkill")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("ShiftRegisters");
                });

            modelBuilder.Entity("Data.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Data.Entities.StaffScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<float>("MaxHoursPerDay")
                        .HasColumnType("real");

                    b.Property<float>("MaxHoursPerWeek")
                        .HasColumnType("real");

                    b.Property<float>("MaxShiftDuration")
                        .HasColumnType("real");

                    b.Property<float>("MinHoursPerDay")
                        .HasColumnType("real");

                    b.Property<float>("MinHoursPerWeek")
                        .HasColumnType("real");

                    b.Property<float>("MinShiftDuration")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Username1")
                        .HasColumnType("text");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Username1");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("StaffScheduleDetails");
                });

            modelBuilder.Entity("Data.Entities.StaffSkill", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Username", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("StaffSkills");
                });

            modelBuilder.Entity("Data.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Data.Entities.StoreScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("MaxDayOff")
                        .HasColumnType("integer");

                    b.Property<float>("MaxHoursPerDay")
                        .HasColumnType("real");

                    b.Property<float>("MaxHoursPerWeek")
                        .HasColumnType("real");

                    b.Property<float>("MaxShiftDuration")
                        .HasColumnType("real");

                    b.Property<int>("MaxShiftPerDay")
                        .HasColumnType("integer");

                    b.Property<int>("MinDayOff")
                        .HasColumnType("integer");

                    b.Property<float>("MinHoursPerDay")
                        .HasColumnType("real");

                    b.Property<float>("MinHoursPerWeek")
                        .HasColumnType("real");

                    b.Property<float>("MinShiftDuration")
                        .HasColumnType("real");

                    b.Property<int>("StaffType")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("StoreScheduleDetails");
                });

            modelBuilder.Entity("Data.Entities.StoreStaff", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsManager")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPrimaryStore")
                        .HasColumnType("boolean");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Username", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreStaffs");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int?>("BrandId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<byte[]>("Password")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int?>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Username");

                    b.HasIndex("BrandId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Entities.WeekSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("WeekSchedules");
                });

            modelBuilder.Entity("Data.Entities.WeekScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("WorkEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("WorkStart")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("WeekScheduleDetails");
                });

            modelBuilder.Entity("Data.Entities.Post", b =>
                {
                    b.HasOne("Data.Entities.Brand", "Brand")
                        .WithMany("Posts")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Data.Entities.ShiftAssignment", b =>
                {
                    b.HasOne("Data.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("ShiftAssignments")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Skill");

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Entities.ShiftAttendance", b =>
                {
                    b.HasOne("Data.Entities.ShiftAssignment", "ShiftAssignment")
                        .WithOne("ShiftAttendance")
                        .HasForeignKey("Data.Entities.ShiftAttendance", "ShiftAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShiftAssignment");
                });

            modelBuilder.Entity("Data.Entities.ShiftLog", b =>
                {
                    b.HasOne("Data.Entities.ShiftAssignment", "ShiftAssignment")
                        .WithMany("ShiftLogs")
                        .HasForeignKey("ShiftAssignmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ShiftAssignment");
                });

            modelBuilder.Entity("Data.Entities.ShiftRegister", b =>
                {
                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("ShiftRegisters")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Data.Entities.WeekSchedule", "WeekSchedule")
                        .WithMany("ShiftRegisters")
                        .HasForeignKey("WeekScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Data.Entities.Skill", b =>
                {
                    b.HasOne("Data.Entities.Brand", "Brand")
                        .WithMany("Skills")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Data.Entities.StaffScheduleDetail", b =>
                {
                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("StaffScheduleDetails")
                        .HasForeignKey("Username1");

                    b.HasOne("Data.Entities.WeekSchedule", "WeekSchedule")
                        .WithMany("StaffScheduleDetails")
                        .HasForeignKey("WeekScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Data.Entities.StaffSkill", b =>
                {
                    b.HasOne("Data.Entities.Skill", "Skill")
                        .WithMany("StaffSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("StaffSkills")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Entities.Store", b =>
                {
                    b.HasOne("Data.Entities.Brand", "Brand")
                        .WithMany("Stores")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Data.Entities.StoreScheduleDetail", b =>
                {
                    b.HasOne("Data.Entities.WeekSchedule", "WeekSchedule")
                        .WithMany("StoreScheduleDetails")
                        .HasForeignKey("WeekScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Data.Entities.StoreStaff", b =>
                {
                    b.HasOne("Data.Entities.Store", "Store")
                        .WithMany("StoreStaffs")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("StoreStaffs")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.HasOne("Data.Entities.Brand", "Brand")
                        .WithMany("Users")
                        .HasForeignKey("BrandId");

                    b.HasOne("Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Data.Entities.WeekSchedule", b =>
                {
                    b.HasOne("Data.Entities.Store", "Store")
                        .WithMany("WeekSchedules")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("Data.Entities.WeekScheduleDetail", b =>
                {
                    b.HasOne("Data.Entities.Skill", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.WeekSchedule", "WeekSchedule")
                        .WithMany("WeekScheduleDetails")
                        .HasForeignKey("WeekScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Data.Entities.Brand", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("Skills");

                    b.Navigation("Stores");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data.Entities.ShiftAssignment", b =>
                {
                    b.Navigation("ShiftAttendance");

                    b.Navigation("ShiftLogs");
                });

            modelBuilder.Entity("Data.Entities.Skill", b =>
                {
                    b.Navigation("StaffSkills");
                });

            modelBuilder.Entity("Data.Entities.Store", b =>
                {
                    b.Navigation("StoreStaffs");

                    b.Navigation("WeekSchedules");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Navigation("ShiftAssignments");

                    b.Navigation("ShiftRegisters");

                    b.Navigation("StaffScheduleDetails");

                    b.Navigation("StaffSkills");

                    b.Navigation("StoreStaffs");
                });

            modelBuilder.Entity("Data.Entities.WeekSchedule", b =>
                {
                    b.Navigation("ShiftRegisters");

                    b.Navigation("StaffScheduleDetails");

                    b.Navigation("StoreScheduleDetails");

                    b.Navigation("WeekScheduleDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

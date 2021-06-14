﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace STS.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hotline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogoImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Data.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Entities.ShiftAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MealEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MealStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReferenceId")
                        .HasColumnType("int");

                    b.Property<int?>("ShiftReferenceId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ShiftReferenceId");

                    b.HasIndex("SkillId");

                    b.HasIndex("StoreId");

                    b.HasIndex("Username");

                    b.ToTable("ShiftAssignments");
                });

            modelBuilder.Entity("Data.Entities.ShiftAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ShiftAssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeCheckOut")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ShiftAssignmentId")
                        .IsUnique();

                    b.ToTable("ShiftAttendances");
                });

            modelBuilder.Entity("Data.Entities.ShiftLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShiftAssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ShiftAssignmentId");

                    b.ToTable("ShiftLogs");
                });

            modelBuilder.Entity("Data.Entities.ShiftRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PreferSkill")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("ShiftRegisters");
                });

            modelBuilder.Entity("Data.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Data.Entities.StaffScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxHours")
                        .HasColumnType("int");

                    b.Property<int>("MinHours")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Username1");

                    b.HasIndex("WeekScheduleId");

                    b.ToTable("StaffScheduleDetails");
                });

            modelBuilder.Entity("Data.Entities.StaffSkill", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Username", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("StaffSkills");
                });

            modelBuilder.Entity("Data.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Data.Entities.StoreStaff", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsManager")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPrimaryStaff")
                        .HasColumnType("bit");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Username", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreStaffs");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.HasIndex("BrandId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.Entities.WeekSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("WeekSchedules");
                });

            modelBuilder.Entity("Data.Entities.WeekScheduleDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("WeekScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WorkEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkStart")
                        .HasColumnType("datetime2");

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
                    b.HasOne("Data.Entities.ShiftAssignment", "ShiftReference")
                        .WithMany()
                        .HasForeignKey("ShiftReferenceId");

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

                    b.Navigation("ShiftReference");

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
                        .OnDelete(DeleteBehavior.NoAction)
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

                    b.Navigation("WeekScheduleDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

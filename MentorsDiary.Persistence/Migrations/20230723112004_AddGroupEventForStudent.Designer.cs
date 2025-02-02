﻿// <auto-generated />
using System;
using MentorsDiary.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MentorsDiary.Persistence.Migrations
{
    [DbContext(typeof(MentorsDiaryDbContext))]
    [Migration("20230723112004_AddGroupEventForStudent")]
    partial class AddGroupEventForStudent
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupEventStudent", b =>
                {
                    b.Property<int>("GroupEventsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("GroupEventsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("GroupEventStudent");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Curators.Domains.Curator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Curators");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Divisions.Domains.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Divisions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Division1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Division2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Division3"
                        });
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Events.Domains.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEvent")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CountParticipants")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupEvents");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Groups.Domains.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CuratorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("DivisionId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Parents.Domains.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("WorkName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Students.Domains.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Users.Domains.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DivisionId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserCreated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserUpdated")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DivisionId = 1,
                            Name = "user1",
                            Password = "user1",
                            Role = 1
                        },
                        new
                        {
                            Id = 2,
                            DivisionId = 2,
                            Name = "user2",
                            Password = "user2",
                            Role = 2
                        },
                        new
                        {
                            Id = 3,
                            DivisionId = 3,
                            Name = "user3",
                            Password = "user3",
                            Role = 2
                        },
                        new
                        {
                            Id = 4,
                            DivisionId = 2,
                            Name = "user4",
                            Password = "user4",
                            Role = 3
                        },
                        new
                        {
                            Id = 5,
                            DivisionId = 2,
                            Name = "user5",
                            Password = "user5",
                            Role = 3
                        },
                        new
                        {
                            Id = 6,
                            DivisionId = 2,
                            Name = "user6",
                            Password = "user6",
                            Role = 3
                        },
                        new
                        {
                            Id = 7,
                            DivisionId = 3,
                            Name = "user7",
                            Password = "user7",
                            Role = 3
                        },
                        new
                        {
                            Id = 8,
                            DivisionId = 3,
                            Name = "user8",
                            Password = "user8",
                            Role = 3
                        });
                });

            modelBuilder.Entity("ParentStudent", b =>
                {
                    b.Property<int>("ChildrensId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("ChildrensId", "ParentId");

                    b.HasIndex("ParentId");

                    b.ToTable("ParentStudent");
                });

            modelBuilder.Entity("GroupEventStudent", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent", null)
                        .WithMany()
                        .HasForeignKey("GroupEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MentorsDiary.Application.Entities.Students.Domains.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Curators.Domains.Curator", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Users.Domains.User", "User")
                        .WithMany("Curators")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.GroupEvents.Domains.GroupEvent", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Events.Domains.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("MentorsDiary.Application.Entities.Groups.Domains.Group", "Group")
                        .WithMany("GroupEvents")
                        .HasForeignKey("GroupId");

                    b.Navigation("Event");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Groups.Domains.Group", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Curators.Domains.Curator", "Curator")
                        .WithMany("Group")
                        .HasForeignKey("CuratorId");

                    b.HasOne("MentorsDiary.Application.Entities.Divisions.Domains.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");

                    b.Navigation("Curator");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Students.Domains.Student", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Groups.Domains.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Users.Domains.User", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Divisions.Domains.Division", "Division")
                        .WithMany("Users")
                        .HasForeignKey("DivisionId");

                    b.Navigation("Division");
                });

            modelBuilder.Entity("ParentStudent", b =>
                {
                    b.HasOne("MentorsDiary.Application.Entities.Students.Domains.Student", null)
                        .WithMany()
                        .HasForeignKey("ChildrensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MentorsDiary.Application.Entities.Parents.Domains.Parent", null)
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Curators.Domains.Curator", b =>
                {
                    b.Navigation("Group");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Divisions.Domains.Division", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Groups.Domains.Group", b =>
                {
                    b.Navigation("GroupEvents");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("MentorsDiary.Application.Entities.Users.Domains.User", b =>
                {
                    b.Navigation("Curators");
                });
#pragma warning restore 612, 618
        }
    }
}

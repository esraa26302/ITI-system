﻿// <auto-generated />
using MVC_tasks_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVC_tasks_2.Migrations
{
    [DbContext(typeof(ContextDataBase))]
    [Migration("20240222150101_m10")]
    partial class m10
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.Property<int>("DepartmentsDeptId")
                        .HasColumnType("int");

                    b.Property<int>("coursesId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsDeptId", "coursesId");

                    b.HasIndex("coursesId");

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("DeptName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeptId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Deptid")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageMimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Deptid");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.StudentCourse", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentsCourses");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.HasOne("MVC_tasks_2.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVC_tasks_2.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("coursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Student", b =>
                {
                    b.HasOne("MVC_tasks_2.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("Deptid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.StudentCourse", b =>
                {
                    b.HasOne("MVC_tasks_2.Models.Course", "Course")
                        .WithMany("CoursesStudents")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MVC_tasks_2.Models.Student", "Student")
                        .WithMany("studentcourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Course", b =>
                {
                    b.Navigation("CoursesStudents");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Department", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MVC_tasks_2.Models.Student", b =>
                {
                    b.Navigation("studentcourses");
                });
#pragma warning restore 612, 618
        }
    }
}

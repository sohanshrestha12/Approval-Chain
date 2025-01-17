﻿// <auto-generated />
using ApprovalChain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApprovalChain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240504193155_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApprovalChain.Models.Approval", b =>
                {
                    b.Property<int>("Approval_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Approval_Id"));

                    b.Property<string>("Approval_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Approval_Id");

                    b.ToTable("approvals");
                });

            modelBuilder.Entity("ApprovalChain.Models.DocumentApproval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("DocumentId");

                    b.ToTable("documentApprovals");
                });

            modelBuilder.Entity("ApprovalChain.Models.DocumentRecommender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("documentRecommenders");
                });

            modelBuilder.Entity("ApprovalChain.Models.Documents", b =>
                {
                    b.Property<int>("Document_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Document_Id"));

                    b.Property<string>("Document_Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Employee_Id")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("currentEmpDoc")
                        .HasColumnType("int");

                    b.HasKey("Document_Id");

                    b.HasIndex("Employee_Id");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("ApprovalChain.Models.Employee", b =>
                {
                    b.Property<int>("Employee_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Employee_Id"));

                    b.Property<string>("Employee_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Employee_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Employee_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Employee_Id");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("ApprovalChain.Models.DocumentApproval", b =>
                {
                    b.HasOne("ApprovalChain.Models.Approval", "Approval")
                        .WithMany()
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApprovalChain.Models.Documents", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("ApprovalChain.Models.DocumentRecommender", b =>
                {
                    b.HasOne("ApprovalChain.Models.Documents", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApprovalChain.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("employee");
                });

            modelBuilder.Entity("ApprovalChain.Models.Documents", b =>
                {
                    b.HasOne("ApprovalChain.Models.Employee", "Employee")
                        .WithMany("Employee_Documents")
                        .HasForeignKey("Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ApprovalChain.Models.Employee", b =>
                {
                    b.Navigation("Employee_Documents");
                });
#pragma warning restore 612, 618
        }
    }
}

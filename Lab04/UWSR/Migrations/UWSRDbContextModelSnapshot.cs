﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UWSR.Models;

#nullable disable

namespace UWSR.Migrations
{
    [DbContext(typeof(UWSRDbContext))]
    partial class UWSRDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UWSR.Models.WSREF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Minus")
                        .HasColumnType("int");

                    b.Property<int>("Plus")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WSREFs");
                });

            modelBuilder.Entity("UWSR.Models.WSREFCOMMENT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ComText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Stamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("WSREFId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WSREFId");

                    b.ToTable("WSREFComments");
                });

            modelBuilder.Entity("UWSR.Models.WSREFCOMMENT", b =>
                {
                    b.HasOne("UWSR.Models.WSREF", "WSREF")
                        .WithMany("Comments")
                        .HasForeignKey("WSREFId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WSREF");
                });

            modelBuilder.Entity("UWSR.Models.WSREF", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}

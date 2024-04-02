﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UFAR.DM.API.Data.DAO;

#nullable disable

namespace UFAR.DM.API.Data.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20240327101129_QuestionEntityAdded")]
    partial class QuestionEntityAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.ExpressionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Expressions");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.QuestionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Random1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Random2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Random3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionEntityId")
                        .HasColumnType("int");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Synonym")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionEntityId");

                    b.ToTable("QuestionEntity");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.SectionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.WordEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.ExpressionEntity", b =>
                {
                    b.HasOne("UFAR.DM.API.Data.Entities.SectionEntity", "Section")
                        .WithMany("Expressions")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.QuestionEntity", b =>
                {
                    b.HasOne("UFAR.DM.API.Data.Entities.SectionEntity", "SectionEntity")
                        .WithMany("Questions")
                        .HasForeignKey("SectionEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SectionEntity");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.WordEntity", b =>
                {
                    b.HasOne("UFAR.DM.API.Data.Entities.SectionEntity", "Section")
                        .WithMany("Words")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("UFAR.DM.API.Data.Entities.SectionEntity", b =>
                {
                    b.Navigation("Expressions");

                    b.Navigation("Questions");

                    b.Navigation("Words");
                });
#pragma warning restore 612, 618
        }
    }
}

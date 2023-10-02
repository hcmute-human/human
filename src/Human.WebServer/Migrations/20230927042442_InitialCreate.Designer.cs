﻿// <auto-generated />
using System;
using Human.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Human.WebServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230927042442_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Human.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Instant>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("current_timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(261)
                        .HasColumnType("character varying(261)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(61)
                        .HasColumnType("character varying(61)");

                    b.Property<Instant>("UpdatingTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("current_timestamp");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a06116ce-c51e-46fa-9bf6-b051b24d5923"),
                            CreationTime = NodaTime.Instant.FromUnixTimeTicks(0L),
                            Email = "admin@gmail.com",
                            PasswordHash = "$2a$11$DEMCG1S/FXoo95W./kiU3OafZp91zbbNQEt3y4D2O/WUSJlwKFbCe",
                            UpdatingTime = NodaTime.Instant.FromUnixTimeTicks(0L)
                        });
                });

            modelBuilder.Entity("Human.Domain.Models.UserPasswordResetToken", b =>
                {
                    b.Property<string>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<Instant>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Token");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserPasswordResetTokens");
                });

            modelBuilder.Entity("Human.Domain.Models.UserPasswordResetToken", b =>
                {
                    b.HasOne("Human.Domain.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Human.Domain.Models.UserPasswordResetToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

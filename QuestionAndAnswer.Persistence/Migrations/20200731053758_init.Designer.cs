﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuestionAndAnswer.Persistence;

namespace QuestionAndAnswer.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200731053758_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("QuestionAndAnswer.Domain.Entities.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Content = "To catch problems earlier speeding up your developments",
                            Created = new DateTime(2019, 5, 18, 14, 50, 0, 0, DateTimeKind.Unspecified),
                            QuestionId = -1,
                            UserId = 2,
                            UserName = "jane.test@test.com"
                        },
                        new
                        {
                            Id = -2,
                            Content = "So, that you can use the JavaScript features of tomorrow, today",
                            Created = new DateTime(2019, 5, 18, 16, 48, 0, 0, DateTimeKind.Unspecified),
                            QuestionId = -1,
                            UserId = 3,
                            UserName = "fred.test@test.com"
                        });
                });

            modelBuilder.Entity("QuestionAndAnswer.Domain.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Content = "TypeScript seems to be getting popular so I wondered whether it is worth my time learning it? What benefits does it give over JavaScript?",
                            Created = new DateTime(2019, 5, 18, 14, 32, 0, 0, DateTimeKind.Unspecified),
                            Title = "Why should I learn TypeScript?",
                            UserId = 1,
                            UserName = "bob.test@test.com"
                        },
                        new
                        {
                            Id = -2,
                            Content = "There seem to be a fair few state management tools around for React - React, Unstated, ... Which one should I use?",
                            Created = new DateTime(2019, 5, 18, 14, 48, 0, 0, DateTimeKind.Unspecified),
                            Title = "Which state management tool should I use?",
                            UserId = 2,
                            UserName = "jane.test@test.com"
                        });
                });

            modelBuilder.Entity("QuestionAndAnswer.Domain.Entities.Answer", b =>
                {
                    b.HasOne("QuestionAndAnswer.Domain.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

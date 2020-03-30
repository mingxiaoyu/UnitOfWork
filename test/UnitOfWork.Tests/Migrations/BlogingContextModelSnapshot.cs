﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitOfWork.Tests;

namespace UnitOfWork.Tests.Migrations
{
    [DbContext(typeof(BloggingContext))]
    partial class BlogingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("UnitOfWork.Tests.Blog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("CreatedUser");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset?>("Modified");

                    b.Property<string>("ModifiedUser");

                    b.Property<string>("Url")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Bolgs");
                });

            modelBuilder.Entity("UnitOfWork.Tests.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ordering.Features.Buyer;

#nullable disable

namespace Ordering.Migrations.Buyer
{
    [DbContext(typeof(BuyerContext))]
    partial class BuyerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommonFeatures.Storage.PersistendEvent<Ordering.Features.Buyer.ValueObjects.BuyerId>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AggregateName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AggregateId");

                    b.HasIndex("AggregateId", "Version")
                        .IsUnique();

                    b.ToTable("Buyer_Events", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

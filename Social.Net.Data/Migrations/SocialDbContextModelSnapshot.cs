﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Social.Net.Data;

#nullable disable

namespace Social.Net.Data.Migrations
{
    [DbContext(typeof(SocialDbContext))]
    partial class SocialDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Social.Net.Core.Domains.Directory.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("StateProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StateProvinceId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Directory.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("NumericIsoCode")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ThreeLetterIsoCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("char(3)")
                        .IsFixedLength();

                    b.Property<string>("TwoLetterIsoCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("char(2)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Directory.StateProvince", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<bool>("Published")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("StateProvinces");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.FriendManagement.FriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AcceptedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("FriendRequestStatusId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverProfileId")
                        .HasColumnType("int");

                    b.Property<int>("SenderProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverProfileId");

                    b.HasIndex("SenderProfileId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.FriendManagement.Friendship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FriendListedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProfileId1")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId1");

                    b.HasIndex("ProfileId2");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("char(60)")
                        .IsFixedLength();

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PermanentAddressId")
                        .HasColumnType("int");

                    b.Property<int>("PresentAddressId")
                        .HasColumnType("int");

                    b.Property<bool>("RequireAuthentication")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("RequireEmailVerification")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("PermanentAddressId");

                    b.HasIndex("PresentAddressId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Directory.Address", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Directory.StateProvince", null)
                        .WithMany()
                        .HasForeignKey("StateProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Directory.StateProvince", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Directory.Country", null)
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.FriendManagement.FriendRequest", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithMany()
                        .HasForeignKey("ReceiverProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithMany()
                        .HasForeignKey("SenderProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.FriendManagement.Friendship", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.Password", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithOne()
                        .HasForeignKey("Social.Net.Core.Domains.Users.Password", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.Profile", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Directory.Address", null)
                        .WithMany()
                        .HasForeignKey("PermanentAddressId");

                    b.HasOne("Social.Net.Core.Domains.Directory.Address", null)
                        .WithMany()
                        .HasForeignKey("PresentAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Social.Net.Core.Domains.Users.RefreshToken", b =>
                {
                    b.HasOne("Social.Net.Core.Domains.Users.Profile", null)
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

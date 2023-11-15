﻿// <auto-generated />
using System;
using DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(BookingHotelDbContext))]
    [Migration("20231115012847_EditRomSize")]
    partial class EditRomSize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entities.Booking", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("CusCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("Deposit")
                        .HasColumnType("bit");

                    b.Property<string>("HotelCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Paid")
                        .HasColumnType("bit");

                    b.Property<string>("RoomCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CusCodeByUserId");

                    b.HasIndex("HotelCodeByUserId");

                    b.HasIndex("RoomCodeByUserId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("DataAccess.Entities.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CusAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CusDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CusDob")
                        .HasColumnType("datetime2");

                    b.Property<string>("CusEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CusFullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CusPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("DataAccess.Entities.Evaluate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CusCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CusCodeByUserId");

                    b.HasIndex("HotelCodeByUserId");

                    b.ToTable("Evaluate");
                });

            modelBuilder.Entity("DataAccess.Entities.Hotel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HotelLevel")
                        .HasColumnType("int");

                    b.Property<string>("HotelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ImgCodeByUserId");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("DataAccess.Entities.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PathServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DataAccess.Entities.Menu", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("MenuLevel")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("DataAccess.Entities.New", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int?>("CountView")
                        .HasColumnType("int");

                    b.Property<string>("ImgCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NewsContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SumContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImgCodeByUserId");

                    b.ToTable("New");
                });

            modelBuilder.Entity("DataAccess.Entities.Room", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImgCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RoomHuman")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelCodeByUserId");

                    b.HasIndex("ImgCodeByUserId");

                    b.HasIndex("ServiceCodeByUserId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("DataAccess.Entities.SaleOff", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BeginDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ExpiryDatetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("HotelCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Numbers")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelCodeByUserId");

                    b.ToTable("SaleOff");
                });

            modelBuilder.Entity("DataAccess.Entities.Service", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HotelCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImgCodeByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SysDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HotelCodeByUserId");

                    b.HasIndex("ImgCodeByUserId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataAccess.Entities.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastPasswordChanged")
                        .HasColumnType("datetime2");

                    b.Property<string>("NationId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PasswordChanged")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PasswordExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.HasIndex("ImageId");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("DataAccess.Entities.Booking", b =>
                {
                    b.HasOne("DataAccess.Entities.Customer", "CusCodeByUser")
                        .WithMany("Booking")
                        .HasForeignKey("CusCodeByUserId");

                    b.HasOne("DataAccess.Entities.Hotel", "HotelCodeByUser")
                        .WithMany("Booking")
                        .HasForeignKey("HotelCodeByUserId");

                    b.HasOne("DataAccess.Entities.Room", "RoomCodeByUser")
                        .WithMany("Booking")
                        .HasForeignKey("RoomCodeByUserId");

                    b.Navigation("CusCodeByUser");

                    b.Navigation("HotelCodeByUser");

                    b.Navigation("RoomCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.Evaluate", b =>
                {
                    b.HasOne("DataAccess.Entities.Customer", "CusCodeByUser")
                        .WithMany("Evaluate")
                        .HasForeignKey("CusCodeByUserId");

                    b.HasOne("DataAccess.Entities.Hotel", "HotelCodeByUser")
                        .WithMany("Evaluate")
                        .HasForeignKey("HotelCodeByUserId");

                    b.Navigation("CusCodeByUser");

                    b.Navigation("HotelCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.Hotel", b =>
                {
                    b.HasOne("DataAccess.Entities.Image", "ImgCodeByUser")
                        .WithMany("Hotel")
                        .HasForeignKey("ImgCodeByUserId");

                    b.Navigation("ImgCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.New", b =>
                {
                    b.HasOne("DataAccess.Entities.Image", "ImgCodeByUser")
                        .WithMany("New")
                        .HasForeignKey("ImgCodeByUserId");

                    b.Navigation("ImgCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.Room", b =>
                {
                    b.HasOne("DataAccess.Entities.Hotel", "HotelCodeByUser")
                        .WithMany("Room")
                        .HasForeignKey("HotelCodeByUserId");

                    b.HasOne("DataAccess.Entities.Image", "ImgCodeByUser")
                        .WithMany("Room")
                        .HasForeignKey("ImgCodeByUserId");

                    b.HasOne("DataAccess.Entities.Service", "ServiceCodeByUser")
                        .WithMany()
                        .HasForeignKey("ServiceCodeByUserId");

                    b.Navigation("HotelCodeByUser");

                    b.Navigation("ImgCodeByUser");

                    b.Navigation("ServiceCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.SaleOff", b =>
                {
                    b.HasOne("DataAccess.Entities.Hotel", "HotelCodeByUser")
                        .WithMany("SaleOff")
                        .HasForeignKey("HotelCodeByUserId");

                    b.Navigation("HotelCodeByUser");
                });

            modelBuilder.Entity("DataAccess.Entities.Service", b =>
                {
                    b.HasOne("DataAccess.Entities.Hotel", "HotelCodeByUser")
                        .WithMany("Service")
                        .HasForeignKey("HotelCodeByUserId");

                    b.HasOne("DataAccess.Entities.Image", "ImgCodeByUser")
                        .WithMany("Service")
                        .HasForeignKey("ImgCodeByUserId");

                    b.Navigation("HotelCodeByUser");

                    b.Navigation("ImgCodeByUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Entities.User", b =>
                {
                    b.HasOne("DataAccess.Entities.Image", null)
                        .WithMany("User")
                        .HasForeignKey("ImageId");
                });

            modelBuilder.Entity("DataAccess.Entities.Customer", b =>
                {
                    b.Navigation("Booking");

                    b.Navigation("Evaluate");
                });

            modelBuilder.Entity("DataAccess.Entities.Hotel", b =>
                {
                    b.Navigation("Booking");

                    b.Navigation("Evaluate");

                    b.Navigation("Room");

                    b.Navigation("SaleOff");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("DataAccess.Entities.Image", b =>
                {
                    b.Navigation("Hotel");

                    b.Navigation("New");

                    b.Navigation("Room");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Entities.Room", b =>
                {
                    b.Navigation("Booking");
                });
#pragma warning restore 612, 618
        }
    }
}

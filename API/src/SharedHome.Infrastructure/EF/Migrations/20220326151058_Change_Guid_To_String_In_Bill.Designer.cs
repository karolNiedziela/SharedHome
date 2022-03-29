﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedHome.Infrastructure.EF.Contexts;

#nullable disable

namespace SharedHome.Infrastructure.EF.Migrations
{
    [DbContext(typeof(SharedHomeDbContext))]
    [Migration("20220326151058_Change_Guid_To_String_In_Bill")]
    partial class Change_Guid_To_String_In_Bill
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.Bills.Entities.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BillType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly>("DateOfPayment")
                        .HasColumnType("date");

                    b.Property<bool>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("SharedHome.Domain.HouseGroups.Aggregates.HouseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("HouseGroups");
                });

            modelBuilder.Entity("SharedHome.Domain.Invitations.Aggregates.Invitation", b =>
                {
                    b.Property<int>("HouseGroupId")
                        .HasColumnType("int");

                    b.Property<string>("PersonId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("HouseGroupId", "PersonId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("SharedHome.Domain.ShoppingLists.Aggregates.ShoppingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PersonId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("SharedHome.Infrastructure.Identity.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
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
                    b.HasOne("SharedHome.Infrastructure.Identity.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SharedHome.Infrastructure.Identity.Entities.ApplicationUser", null)
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

                    b.HasOne("SharedHome.Infrastructure.Identity.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SharedHome.Infrastructure.Identity.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedHome.Domain.Bills.Entities.Bill", b =>
                {
                    b.OwnsOne("SharedHome.Domain.Bills.ValueObjects.BillCost", "Cost", b1 =>
                        {
                            b1.Property<int>("BillId")
                                .HasColumnType("int");

                            b1.Property<decimal?>("Value")
                                .IsRequired()
                                .HasPrecision(14, 2)
                                .HasColumnType("decimal(14,2)")
                                .HasColumnName("Cost");

                            b1.HasKey("BillId");

                            b1.ToTable("Bills");

                            b1.WithOwner()
                                .HasForeignKey("BillId");
                        });

                    b.OwnsOne("SharedHome.Domain.Bills.ValueObjects.ServiceProviderName", "ServiceProvider", b1 =>
                        {
                            b1.Property<int>("BillId")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("ServiceProviderName");

                            b1.HasKey("BillId");

                            b1.ToTable("Bills");

                            b1.WithOwner()
                                .HasForeignKey("BillId");
                        });

                    b.Navigation("Cost");

                    b.Navigation("ServiceProvider")
                        .IsRequired();
                });

            modelBuilder.Entity("SharedHome.Domain.HouseGroups.Aggregates.HouseGroup", b =>
                {
                    b.OwnsMany("SharedHome.Domain.HouseGroups.ValueObjects.HouseGroupMember", "Members", b1 =>
                        {
                            b1.Property<int>("HouseGroupId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<bool>("IsOwner")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("tinyint(1)")
                                .HasDefaultValue(false);

                            b1.Property<string>("PersonId")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("HouseGroupId", "Id");

                            b1.ToTable("HouseGroupMembers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("HouseGroupId");

                            b1.OwnsOne("SharedHome.Domain.HouseGroups.ValueObjects.Email", "Email", b2 =>
                                {
                                    b2.Property<int>("HouseGroupMemberHouseGroupId")
                                        .HasColumnType("int");

                                    b2.Property<int>("HouseGroupMemberId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("Email");

                                    b2.HasKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");

                                    b2.ToTable("HouseGroupMembers");

                                    b2.WithOwner()
                                        .HasForeignKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");
                                });

                            b1.OwnsOne("SharedHome.Domain.HouseGroups.ValueObjects.FirstName", "FirstName", b2 =>
                                {
                                    b2.Property<int>("HouseGroupMemberHouseGroupId")
                                        .HasColumnType("int");

                                    b2.Property<int>("HouseGroupMemberId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("FirstName");

                                    b2.HasKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");

                                    b2.ToTable("HouseGroupMembers");

                                    b2.WithOwner()
                                        .HasForeignKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");
                                });

                            b1.OwnsOne("SharedHome.Domain.HouseGroups.ValueObjects.LastName", "LastName", b2 =>
                                {
                                    b2.Property<int>("HouseGroupMemberHouseGroupId")
                                        .HasColumnType("int");

                                    b2.Property<int>("HouseGroupMemberId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("LastName");

                                    b2.HasKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");

                                    b2.ToTable("HouseGroupMembers");

                                    b2.WithOwner()
                                        .HasForeignKey("HouseGroupMemberHouseGroupId", "HouseGroupMemberId");
                                });

                            b1.Navigation("Email")
                                .IsRequired();

                            b1.Navigation("FirstName")
                                .IsRequired();

                            b1.Navigation("LastName")
                                .IsRequired();
                        });

                    b.Navigation("Members");
                });

            modelBuilder.Entity("SharedHome.Domain.ShoppingLists.Aggregates.ShoppingList", b =>
                {
                    b.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.ShoppingListName", "Name", b1 =>
                        {
                            b1.Property<int>("ShoppingListId")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.HasKey("ShoppingListId");

                            b1.ToTable("ShoppingLists");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");
                        });

                    b.OwnsMany("SharedHome.Domain.ShoppingLists.ValueObjects.ShoppingListProduct", "Products", b1 =>
                        {
                            b1.Property<int>("ShoppingListId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<bool>("IsBought")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("tinyint(1)")
                                .HasDefaultValue(false);

                            b1.HasKey("ShoppingListId", "Id");

                            b1.ToTable("ShoppingListProducts", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.ProductPrice", "Price", b2 =>
                                {
                                    b2.Property<int>("ShoppingListProductShoppingListId")
                                        .HasColumnType("int");

                                    b2.Property<int>("ShoppingListProductId")
                                        .HasColumnType("int");

                                    b2.Property<decimal?>("Value")
                                        .IsRequired()
                                        .HasPrecision(12, 4)
                                        .HasColumnType("decimal(12,4)")
                                        .HasColumnName("Price");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");
                                });

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.Quantity", "Quantity", b2 =>
                                {
                                    b2.Property<int>("ShoppingListProductShoppingListId")
                                        .HasColumnType("int");

                                    b2.Property<int>("ShoppingListProductId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Quantity");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");
                                });

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.ShoppingListProductName", "Name", b2 =>
                                {
                                    b2.Property<int>("ShoppingListProductShoppingListId")
                                        .HasColumnType("int");

                                    b2.Property<int>("ShoppingListProductId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("Name");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");
                                });

                            b1.Navigation("Name")
                                .IsRequired();

                            b1.Navigation("Price");

                            b1.Navigation("Quantity")
                                .IsRequired();
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

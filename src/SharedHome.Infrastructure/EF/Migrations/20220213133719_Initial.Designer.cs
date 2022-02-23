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
    [Migration("20220213133719_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

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

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

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

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("SharedHome.Domain.Bills.Entities.Bill", b =>
                {
                    b.OwnsOne("SharedHome.Domain.Bills.ValueObjects.BillCost", "Cost", b1 =>
                        {
                            b1.Property<int>("BillId")
                                .HasColumnType("int");

                            b1.Property<decimal?>("Value")
                                .IsRequired()
                                .HasColumnType("decimal(65,30)")
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

                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

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
                                        .HasColumnType("decimal(65,30)")
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

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedHome.Infrastructure.EF.Contexts;

#nullable disable

namespace SharedHome.Infrastructure.EF.Migrations.Write
{
    [DbContext(typeof(WriteSharedHomeDbContext))]
    partial class WriteSharedHomeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SharedHome.Domain.Bills.Entities.Bill", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("BillType")
                        .HasColumnType("int")
                        .HasColumnName("BillType");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateOnly>("DateOfPayment")
                        .HasColumnType("date");

                    b.Property<bool>("IsPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Bills", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.HouseGroups.Aggregates.HouseGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("HouseGroups", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.Invitations.Aggregates.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("HouseGroupId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("RequestedByPersonId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RequestedToPersonId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("InvitationStatus");

                    b.HasKey("Id");

                    b.HasIndex("HouseGroupId");

                    b.HasIndex("RequestedByPersonId");

                    b.HasIndex("RequestedToPersonId");

                    b.ToTable("Invitations", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.Persons.Aggregates.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.ShoppingLists.Aggregates.ShoppingList", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDone")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("ShoppingLists", (string)null);
                });

            modelBuilder.Entity("SharedHome.Notifications.Entities.AppNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Operation")
                        .HasColumnType("int")
                        .HasColumnName("OperationType");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Target")
                        .HasColumnType("int")
                        .HasColumnName("TargetType");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Type")
                        .HasColumnType("int")
                        .HasColumnName("NotificationType");

                    b.HasKey("Id");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("SharedHome.Domain.Bills.Entities.Bill", b =>
                {
                    b.HasOne("SharedHome.Domain.Persons.Aggregates.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SharedHome.Domain.Shared.ValueObjects.Money", "Cost", b1 =>
                        {
                            b1.Property<Guid>("BillId")
                                .HasColumnType("char(36)");

                            b1.Property<decimal>("Amount")
                                .HasPrecision(14, 2)
                                .HasColumnType("decimal(14,2)")
                                .HasColumnName("Cost");

                            b1.HasKey("BillId");

                            b1.ToTable("Bills");

                            b1.WithOwner()
                                .HasForeignKey("BillId");

                            b1.OwnsOne("SharedHome.Domain.Shared.ValueObjects.Currency", "Currency", b2 =>
                                {
                                    b2.Property<Guid>("MoneyBillId")
                                        .HasColumnType("char(36)");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("Currency");

                                    b2.HasKey("MoneyBillId");

                                    b2.ToTable("Bills");

                                    b2.WithOwner()
                                        .HasForeignKey("MoneyBillId");
                                });

                            b1.Navigation("Currency")
                                .IsRequired();
                        });

                    b.OwnsOne("SharedHome.Domain.Bills.ValueObjects.ServiceProviderName", "ServiceProvider", b1 =>
                        {
                            b1.Property<Guid>("BillId")
                                .HasColumnType("char(36)");

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
                    b.OwnsMany("SharedHome.Domain.HouseGroups.Entities.HouseGroupMember", "Members", b1 =>
                        {
                            b1.Property<Guid>("HouseGroupId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("CreatedBy")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<bool>("IsOwner")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("tinyint(1)")
                                .HasDefaultValue(false);

                            b1.Property<DateTime>("ModifiedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("ModifiedBy")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("HouseGroupId", "PersonId");

                            b1.HasIndex("PersonId")
                                .IsUnique();

                            b1.ToTable("HouseGroupMembers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("HouseGroupId");

                            b1.HasOne("SharedHome.Domain.Persons.Aggregates.Person", null)
                                .WithOne()
                                .HasForeignKey("SharedHome.Domain.HouseGroups.Entities.HouseGroupMember", "PersonId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();
                        });

                    b.OwnsOne("SharedHome.Domain.HouseGroups.ValueObjects.HouseGroupName", "Name", b1 =>
                        {
                            b1.Property<Guid>("HouseGroupId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.HasKey("HouseGroupId");

                            b1.ToTable("HouseGroups");

                            b1.WithOwner()
                                .HasForeignKey("HouseGroupId");
                        });

                    b.Navigation("Members");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("SharedHome.Domain.Invitations.Aggregates.Invitation", b =>
                {
                    b.HasOne("SharedHome.Domain.HouseGroups.Aggregates.HouseGroup", null)
                        .WithMany()
                        .HasForeignKey("HouseGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedHome.Domain.Persons.Aggregates.Person", null)
                        .WithMany()
                        .HasForeignKey("RequestedByPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharedHome.Domain.Persons.Aggregates.Person", null)
                        .WithMany()
                        .HasForeignKey("RequestedToPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SharedHome.Domain.Persons.Aggregates.Person", b =>
                {
                    b.OwnsOne("SharedHome.Domain.Persons.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Email");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SharedHome.Domain.Persons.ValueObjects.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("FirstName");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("SharedHome.Domain.Persons.ValueObjects.LastName", "LastName", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("LastName");

                            b1.HasKey("PersonId");

                            b1.ToTable("Persons");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();
                });

            modelBuilder.Entity("SharedHome.Domain.ShoppingLists.Aggregates.ShoppingList", b =>
                {
                    b.HasOne("SharedHome.Domain.Persons.Aggregates.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.ShoppingListName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ShoppingListId")
                                .HasColumnType("char(36)");

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
                            b1.Property<Guid>("ShoppingListId")
                                .HasColumnType("char(36)");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("char(36)");

                            b1.Property<bool>("IsBought")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("tinyint(1)")
                                .HasDefaultValue(false);

                            b1.HasKey("ShoppingListId", "Id");

                            b1.ToTable("ShoppingListProducts", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.NetContent", "NetContent", b2 =>
                                {
                                    b2.Property<Guid>("ShoppingListProductShoppingListId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid>("ShoppingListProductId")
                                        .HasColumnType("char(36)");

                                    b2.Property<int?>("Type")
                                        .HasColumnType("int")
                                        .HasColumnName("NetContentType");

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnType("longtext")
                                        .HasColumnName("NetContent");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");
                                });

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.Quantity", "Quantity", b2 =>
                                {
                                    b2.Property<Guid>("ShoppingListProductShoppingListId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid>("ShoppingListProductId")
                                        .HasColumnType("char(36)");

                                    b2.Property<int>("Value")
                                        .HasColumnType("int")
                                        .HasColumnName("Quantity");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");
                                });

                            b1.OwnsOne("SharedHome.Domain.Shared.ValueObjects.Money", "Price", b2 =>
                                {
                                    b2.Property<Guid>("ShoppingListProductShoppingListId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid>("ShoppingListProductId")
                                        .HasColumnType("char(36)");

                                    b2.Property<decimal>("Amount")
                                        .HasPrecision(12, 4)
                                        .HasColumnType("decimal(12,4)")
                                        .HasColumnName("Price");

                                    b2.HasKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.ToTable("ShoppingListProducts");

                                    b2.WithOwner()
                                        .HasForeignKey("ShoppingListProductShoppingListId", "ShoppingListProductId");

                                    b2.OwnsOne("SharedHome.Domain.Shared.ValueObjects.Currency", "Currency", b3 =>
                                        {
                                            b3.Property<Guid>("MoneyShoppingListProductShoppingListId")
                                                .HasColumnType("char(36)");

                                            b3.Property<Guid>("MoneyShoppingListProductId")
                                                .HasColumnType("char(36)");

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasColumnType("longtext")
                                                .HasColumnName("Currency");

                                            b3.HasKey("MoneyShoppingListProductShoppingListId", "MoneyShoppingListProductId");

                                            b3.ToTable("ShoppingListProducts");

                                            b3.WithOwner()
                                                .HasForeignKey("MoneyShoppingListProductShoppingListId", "MoneyShoppingListProductId");
                                        });

                                    b2.Navigation("Currency")
                                        .IsRequired();
                                });

                            b1.OwnsOne("SharedHome.Domain.ShoppingLists.ValueObjects.ShoppingListProductName", "Name", b2 =>
                                {
                                    b2.Property<Guid>("ShoppingListProductShoppingListId")
                                        .HasColumnType("char(36)");

                                    b2.Property<Guid>("ShoppingListProductId")
                                        .HasColumnType("char(36)");

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

                            b1.Navigation("NetContent");

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

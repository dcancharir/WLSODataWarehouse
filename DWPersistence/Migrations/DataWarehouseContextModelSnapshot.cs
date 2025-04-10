﻿// <auto-generated />
using System;
using DWPersistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DWPersistence.Migrations
{
    [DbContext(typeof(DataWarehouseContext))]
    partial class DataWarehouseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DWDomain.DWAssociate", b =>
                {
                    b.Property<string>("Ruc")
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .HasColumnName("ruc")
                        .IsFixedLength()
                        .HasComment("Ruc");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("name")
                        .HasComment("Nombre del asociado");

                    b.Property<byte?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status")
                        .HasComment("Estado del asociado");

                    b.HasKey("Ruc")
                        .HasName("PK_Associate");

                    b.ToTable("Associates", null, t =>
                        {
                            t.HasComment("Asociados");
                        });
                });

            modelBuilder.Entity("DWDomain.DWBrand", b =>
                {
                    b.Property<string>("BrandId")
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .HasColumnName("brandId")
                        .IsFixedLength()
                        .HasComment("ID de la marca");

                    b.Property<short?>("Active")
                        .HasColumnType("smallint")
                        .HasColumnName("active")
                        .HasComment("1 -> Activo | 0 -> Inactivo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name")
                        .HasComment("Nombre Brand");

                    b.Property<long>("ProviderId")
                        .HasColumnType("bigint")
                        .HasColumnName("providerId")
                        .HasComment("ID del provider");

                    b.HasKey("BrandId")
                        .HasName("PK_Brand");

                    b.ToTable("Brands", (string)null);
                });

            modelBuilder.Entity("DWDomain.DWCustomer", b =>
                {
                    b.Property<short?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("active");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("AddressDept")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("addressDept");

                    b.Property<string>("AddressDist")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("addressDist");

                    b.Property<string>("AddressProv")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("addressProv");

                    b.Property<string>("AssociateId")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("associateId")
                        .IsFixedLength();

                    b.Property<DateOnly?>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nchar(250)")
                        .HasColumnName("email")
                        .IsFixedLength();

                    b.Property<short?>("Excluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0)
                        .HasColumnName("excluded");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("firstName");

                    b.Property<string>("IdentId")
                        .HasMaxLength(36)
                        .HasColumnType("nchar(36)")
                        .HasColumnName("identId")
                        .IsFixedLength();

                    b.Property<string>("Identification")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("identification")
                        .IsFixedLength();

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("lastName");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("phone")
                        .IsFixedLength();

                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint")
                        .HasColumnName("playerId");

                    b.Property<DateTime?>("RegDatetime")
                        .HasColumnType("datetime")
                        .HasColumnName("regDatetime");

                    b.Property<string>("StoreId")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("storeId")
                        .IsFixedLength();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nchar(250)")
                        .HasColumnName("username")
                        .IsFixedLength();

                    b.Property<short?>("Verified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)0)
                        .HasColumnName("verified");

                    b.ToTable("Customers", null, t =>
                        {
                            t.HasComment("Informacion de Customer");
                        });
                });

            modelBuilder.Entity("DWDomain.DWCustomersGroup", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint")
                        .HasColumnName("playerId");

                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("groupId");

                    b.Property<int?>("GroupType")
                        .HasColumnType("int")
                        .HasColumnName("groupType");

                    b.HasKey("PlayerId", "GroupId")
                        .HasName("PK_CustomersGroup");

                    b.ToTable("CustomersGroups", (string)null);
                });

            modelBuilder.Entity("DWDomain.DWGame", b =>
                {
                    b.Property<string>("GameId")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("gameId")
                        .IsFixedLength()
                        .HasComment("ID del servicio a traves del que se enviaran mensajes");

                    b.Property<long>("ProviderId")
                        .HasColumnType("bigint")
                        .HasColumnName("providerId")
                        .HasComment("ID del provider");

                    b.Property<short?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("active")
                        .HasComment("1 -> Activo | 0 -> Inactivo");

                    b.Property<string>("BrandId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .HasColumnName("brandId")
                        .IsFixedLength()
                        .HasComment("ID de brand");

                    b.Property<string>("NameBack")
                        .IsRequired()
                        .HasMaxLength(101)
                        .HasColumnType("nchar(101)")
                        .HasColumnName("nameBack")
                        .IsFixedLength()
                        .HasComment("Descripcion del game");

                    b.HasKey("GameId", "ProviderId")
                        .HasName("PK_Game");

                    b.ToTable("Games", null, t =>
                        {
                            t.HasComment("Informacion de los Games");
                        });
                });

            modelBuilder.Entity("DWDomain.DWGroupsx", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("groupId")
                        .HasComment("Id del Tag");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name");

                    b.Property<short?>("Status")
                        .HasColumnType("smallint")
                        .HasColumnName("status")
                        .HasComment("1: Active, 2: Delete");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("type")
                        .HasComment("1 -> Tag, 2 -> Segment");

                    b.HasKey("GroupId")
                        .HasName("PK_Groupsx");

                    b.ToTable("Groupsxs", (string)null);
                });

            modelBuilder.Entity("DWDomain.DWPaymentMethod", b =>
                {
                    b.Property<string>("MethodId")
                        .HasMaxLength(25)
                        .HasColumnType("nchar(25)")
                        .HasColumnName("methodId")
                        .IsFixedLength()
                        .HasComment("ID del metodo del pago. Ej: NIUBIZ_CARDS");

                    b.Property<string>("Type")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasDefaultValue("PAYIN")
                        .HasColumnName("type")
                        .IsFixedLength()
                        .HasComment("Tipo: PAYIN, PAYOUT");

                    b.Property<string>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("1")
                        .HasColumnName("active")
                        .HasComment("1 -> True | 0 -> False");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name")
                        .HasComment("Nombre del metodo de pago");

                    b.HasKey("MethodId", "Type")
                        .HasName("PK_PaymentMethod");

                    b.ToTable("PaymentMethods", (string)null);
                });

            modelBuilder.Entity("DWDomain.DWPlayer", b =>
                {
                    b.Property<long>("PlayerId")
                        .HasColumnType("bigint")
                        .HasColumnName("playerId")
                        .HasComment("ID autoincremental para el player");

                    b.Property<decimal?>("Coins")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasDefaultValue(0m)
                        .HasColumnName("coins")
                        .HasComment("Coins reales");

                    b.Property<decimal?>("CoinsPayOut")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)")
                        .HasDefaultValue(0m)
                        .HasColumnName("coinsPayOut")
                        .HasComment("Balance retirable del usuario");

                    b.HasKey("PlayerId")
                        .HasName("PK_Player");

                    b.ToTable("Players", null, t =>
                        {
                            t.HasComment("Cuentas de Players de dinero real, estas se generan cuando el cliente realiza un registro");
                        });
                });

            modelBuilder.Entity("DWDomain.DWProcessor", b =>
                {
                    b.Property<string>("ProcessorId")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("processorId")
                        .HasComment("Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ...");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("name")
                        .HasComment("Nombre del proveedor");

                    b.Property<short?>("State")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("state")
                        .HasComment("Estado actual del procesador 0 <- DESACTIVADO | 1 <- ACTIVADO");

                    b.HasKey("ProcessorId")
                        .HasName("PK_Processor");

                    b.ToTable("Processors", null, t =>
                        {
                            t.HasComment("Tabla de registros del balance los procesadores de pago");
                        });
                });

            modelBuilder.Entity("DWDomain.DWProvider", b =>
                {
                    b.Property<long>("ProviderId")
                        .HasColumnType("bigint")
                        .HasColumnName("providerId")
                        .HasComment("ID autoincremental del provider");

                    b.Property<short?>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1)
                        .HasColumnName("active")
                        .HasComment("1 -> Activo | 0 -> Inactivo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name")
                        .HasComment("El nombre del provider");

                    b.HasKey("ProviderId")
                        .HasName("PK_Provider");

                    b.ToTable("Providers", null, t =>
                        {
                            t.HasComment("Informacion de Proveedores");
                        });
                });

            modelBuilder.Entity("DWDomain.DWRealGameEvent", b =>
                {
                    b.Property<decimal>("EventId")
                        .HasColumnType("decimal(20,0)")
                        .HasColumnName("eventId")
                        .HasComment("ID de la transaccion");

                    b.Property<long>("Amount")
                        .HasColumnType("bigint")
                        .HasColumnName("amount")
                        .HasComment("Monto de la apuesta");

                    b.Property<string>("AssociateId")
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .HasColumnName("associateId")
                        .IsFixedLength()
                        .HasComment("ID del asociado");

                    b.Property<string>("CoinsType")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16)
                        .HasColumnType("nchar(16)")
                        .HasDefaultValue("REAL")
                        .HasColumnName("coinsType")
                        .IsFixedLength()
                        .HasComment("\"REAL\": dinero real | \"BONUS\": bono");

                    b.Property<string>("GameId")
                        .HasMaxLength(60)
                        .HasColumnType("nchar(60)")
                        .HasColumnName("gameId")
                        .IsFixedLength()
                        .HasComment("ID del juego");

                    b.Property<DateTime?>("InsDatetime")
                        .HasColumnType("datetime")
                        .HasColumnName("insDatetime")
                        .HasComment("Fecha Hora registro");

                    b.Property<long?>("InsTimestamp")
                        .HasColumnType("bigint")
                        .HasColumnName("insTimestamp")
                        .HasComment("Epoch registro");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("playerId")
                        .HasComment("Id del player");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int")
                        .HasColumnName("providerId")
                        .HasComment("ID del proveedor");

                    b.Property<byte?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)0)
                        .HasColumnName("status")
                        .HasComment("Estado del evento: 0 -> Iniciado...");

                    b.Property<string>("StoreId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("storeId")
                        .IsFixedLength()
                        .HasComment("ID del store del player");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nchar(16)")
                        .HasColumnName("type")
                        .IsFixedLength()
                        .HasComment("DEBIT | CREDIT");

                    b.HasKey("EventId")
                        .HasName("PK_RealGameEvent");

                    b.ToTable("RealGameEvents", (string)null);
                });

            modelBuilder.Entity("DWDomain.DWStore", b =>
                {
                    b.Property<string>("StoreId")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("storeId")
                        .IsFixedLength();

                    b.Property<string>("AssociateId")
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .HasColumnName("associateId")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("name");

                    b.Property<byte?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)1)
                        .HasColumnName("status");

                    b.HasKey("StoreId")
                        .HasName("PK_Store");

                    b.ToTable("Stores", null, t =>
                        {
                            t.HasComment("Tiendas");
                        });
                });

            modelBuilder.Entity("DWDomain.DWStoreTx", b =>
                {
                    b.Property<long>("TxId")
                        .HasColumnType("bigint")
                        .HasColumnName("txId")
                        .HasComment("ID de la transaccion");

                    b.Property<int?>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount")
                        .HasComment("monto");

                    b.Property<string>("AssociateId")
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .HasColumnName("associateId")
                        .IsFixedLength();

                    b.Property<long?>("Coins")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(-1L)
                        .HasColumnName("coins")
                        .HasComment("Coins reales después");

                    b.Property<long?>("CoinsBefore")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(-1L)
                        .HasColumnName("coinsBefore")
                        .HasComment("Coins reales antes");

                    b.Property<DateTime?>("EndDatetime")
                        .HasColumnType("datetime")
                        .HasColumnName("endDatetime")
                        .HasComment("Fecha y hora de finalización");

                    b.Property<long?>("EndTimestamp")
                        .HasColumnType("bigint")
                        .HasColumnName("endTimestamp")
                        .HasComment("Epoch final");

                    b.Property<int?>("InsUserId")
                        .HasColumnType("int")
                        .HasColumnName("insUserId")
                        .HasComment("Usuario que hizo la operación");

                    b.Property<string>("PaymentMethodId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25)
                        .HasColumnType("nchar(25)")
                        .HasDefaultValue("MANUAL")
                        .HasColumnName("paymentMethodId")
                        .IsFixedLength()
                        .HasComment("Método de pago");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnName("playerId")
                        .HasComment("Id del player");

                    b.Property<string>("PlayerStoreId")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("playerStoreId")
                        .IsFixedLength();

                    b.Property<string>("ProcessorId")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("processorId")
                        .IsFixedLength()
                        .HasComment("Procesador");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasComment("0: init | 1: success | ...");

                    b.Property<string>("SubType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .HasDefaultValue("NORMAL")
                        .HasColumnName("subType")
                        .IsFixedLength()
                        .HasComment("BONO_FINALIZE");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .HasColumnName("type")
                        .IsFixedLength()
                        .HasComment("DEBIT | CREDIT | ...");

                    b.Property<string>("UserStoreId")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("userStoreId")
                        .IsFixedLength()
                        .HasComment("Id de la tienda");

                    b.HasKey("TxId")
                        .HasName("PK_StoreTx");

                    b.ToTable("StoreTxs", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}

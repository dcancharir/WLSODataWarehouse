using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWPersistence.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Associates",
                columns: table => new
                {
                    ruc = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: false, comment: "Ruc"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "Nombre del asociado"),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1, comment: "Estado del asociado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associate", x => x.ruc);
                },
                comment: "Asociados");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    brandId = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: false, comment: "ID de la marca"),
                    providerId = table.Column<long>(type: "bigint", nullable: false, comment: "ID del provider"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "Nombre Brand"),
                    active = table.Column<short>(type: "smallint", nullable: true, comment: "1 -> Activo | 0 -> Inactivo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.brandId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    playerId = table.Column<long>(type: "bigint", nullable: false),
                    associateId = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    storeId = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    username = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: false),
                    email = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    active = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)1),
                    verified = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    excluded = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    regDatetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    addressDept = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    addressProv = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    addressDist = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    identId = table.Column<string>(type: "nchar(250)", fixedLength: true, maxLength: 250, nullable: true),
                    identification = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.playerId);
                },
                comment: "Informacion de Customer");

            migrationBuilder.CreateTable(
                name: "CustomersGroups",
                columns: table => new
                {
                    playerId = table.Column<long>(type: "bigint", nullable: false),
                    groupId = table.Column<int>(type: "int", nullable: false),
                    groupType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersGroup", x => new { x.playerId, x.groupId });
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    gameId = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false, comment: "ID del servicio a traves del que se enviaran mensajes"),
                    providerId = table.Column<long>(type: "bigint", nullable: false, comment: "ID del provider"),
                    brandId = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: false, comment: "ID de brand"),
                    nameBack = table.Column<string>(type: "nchar(101)", fixedLength: true, maxLength: 101, nullable: false, comment: "Descripcion del game"),
                    active = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)1, comment: "1 -> Activo | 0 -> Inactivo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => new { x.gameId, x.providerId });
                },
                comment: "Informacion de los Games");

            migrationBuilder.CreateTable(
                name: "Groupsx",
                columns: table => new
                {
                    groupId = table.Column<int>(type: "int", nullable: false, comment: "Id del Tag"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false, comment: "1 -> Tag, 2 -> Segment"),
                    status = table.Column<short>(type: "smallint", nullable: true, comment: "1: Active, 2: Delete")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.groupId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    methodId = table.Column<string>(type: "nchar(25)", fixedLength: true, maxLength: 25, nullable: false, comment: "ID del metodo del pago. Ej: NIUBIZ_CARDS"),
                    type = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false, defaultValue: "PAYIN", comment: "Tipo: PAYIN, PAYOUT"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Nombre del metodo de pago"),
                    active = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "1", comment: "1 -> True | 0 -> False")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => new { x.methodId, x.type });
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    playerId = table.Column<long>(type: "bigint", nullable: false, comment: "ID autoincremental para el player"),
                    coins = table.Column<decimal>(type: "decimal(20,0)", nullable: true, defaultValue: 0m, comment: "Coins reales"),
                    coinsPayOut = table.Column<decimal>(type: "decimal(20,0)", nullable: true, defaultValue: 0m, comment: "Balance retirable del usuario")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.playerId);
                },
                comment: "Cuentas de Players de dinero real, estas se generan cuando el cliente realiza un registro");

            migrationBuilder.CreateTable(
                name: "Processors",
                columns: table => new
                {
                    processorId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, comment: "Identificador del procesador. Ej: NIUBIZ, CULQUI, IZIPAY, ..."),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Nombre del proveedor"),
                    state = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)1, comment: "Estado actual del procesador 0 <- DESACTIVADO | 1 <- ACTIVADO")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processor", x => x.processorId);
                },
                comment: "Tabla de registros del balance los procesadores de pago");

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    providerId = table.Column<long>(type: "bigint", nullable: false, comment: "ID autoincremental del provider"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "El nombre del provider"),
                    active = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)1, comment: "1 -> Activo | 0 -> Inactivo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.providerId);
                },
                comment: "Informacion de Proveedores");

            migrationBuilder.CreateTable(
                name: "RealGameEvents",
                columns: table => new
                {
                    eventId = table.Column<decimal>(type: "decimal(20,0)", nullable: false, comment: "ID de la transaccion"),
                    storeId = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: false, comment: "ID del store del player"),
                    playerId = table.Column<int>(type: "int", nullable: false, comment: "Id del player"),
                    providerId = table.Column<int>(type: "int", nullable: false, comment: "ID del proveedor"),
                    gameId = table.Column<string>(type: "nchar(60)", fixedLength: true, maxLength: 60, nullable: true, comment: "ID del juego"),
                    type = table.Column<string>(type: "nchar(16)", fixedLength: true, maxLength: 16, nullable: false, comment: "DEBIT | CREDIT"),
                    amount = table.Column<long>(type: "bigint", nullable: false, comment: "Monto de la apuesta"),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0, comment: "Estado del evento: 0 -> Iniciado..."),
                    insDatetime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Fecha Hora registro"),
                    insTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "Epoch registro"),
                    coinsType = table.Column<string>(type: "nchar(16)", fixedLength: true, maxLength: 16, nullable: true, defaultValue: "REAL", comment: "\"REAL\": dinero real | \"BONUS\": bono"),
                    associateId = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true, comment: "ID del asociado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealGameEvent", x => x.eventId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    storeId = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    associateId = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.storeId);
                },
                comment: "Tiendas");

            migrationBuilder.CreateTable(
                name: "StoreTxs",
                columns: table => new
                {
                    txId = table.Column<long>(type: "bigint", nullable: false, comment: "ID de la transaccion"),
                    playerId = table.Column<int>(type: "int", nullable: false, comment: "Id del player"),
                    type = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: false, comment: "DEBIT | CREDIT | ..."),
                    subType = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: false, defaultValue: "NORMAL", comment: "BONO_FINALIZE"),
                    status = table.Column<int>(type: "int", nullable: false, comment: "0: init | 1: success | ..."),
                    paymentMethodId = table.Column<string>(type: "nchar(25)", fixedLength: true, maxLength: 25, nullable: false, defaultValue: "MANUAL", comment: "Método de pago"),
                    processorId = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true, comment: "Procesador"),
                    amount = table.Column<int>(type: "int", nullable: true, comment: "monto"),
                    coinsBefore = table.Column<long>(type: "bigint", nullable: true, defaultValue: -1L, comment: "Coins reales antes"),
                    coins = table.Column<long>(type: "bigint", nullable: true, defaultValue: -1L, comment: "Coins reales después"),
                    userStoreId = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true, comment: "Id de la tienda"),
                    playerStoreId = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    insUserId = table.Column<int>(type: "int", nullable: true, comment: "Usuario que hizo la operación"),
                    endDatetime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Fecha y hora de finalización"),
                    endTimestamp = table.Column<long>(type: "bigint", nullable: true, comment: "Epoch final"),
                    associateId = table.Column<string>(type: "nchar(11)", fixedLength: true, maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreTx", x => x.txId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Associates");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomersGroups");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Groupsx");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Processors");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "RealGameEvents");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "StoreTxs");
        }
    }
}

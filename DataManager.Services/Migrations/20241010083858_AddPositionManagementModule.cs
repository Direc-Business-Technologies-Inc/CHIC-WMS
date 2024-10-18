using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionManagementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "dfd6904295934d79a8e36842c9352d7a");

            migrationBuilder.CreateTable(
                name: "PositionManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionManagement", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6935), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6876), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6872), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6964), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6869), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6854), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6970), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6850), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6928), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6954), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6967), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6960), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6938), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6950), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6931), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6957), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6942), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6857), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6861), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6973), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6947), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6924), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6846), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6841), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(5764), "80cad8f9fea5488187c09686cf6970a7", "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(5997), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6000), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "80cad8f9fea5488187c09686cf6970a7", new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(5756), "80cad8f9fea5488187c09686cf6970a7", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6772), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6758), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6744), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6730), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6716), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6702), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6685), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6664), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6650), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6634), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6591), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6568), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6555), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6541), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6526), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6513), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6500), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6486), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6405), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6391), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6369), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6355), "80cad8f9fea5488187c09686cf6970a7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 8, 38, 57, 937, DateTimeKind.Utc).AddTicks(6335), "80cad8f9fea5488187c09686cf6970a7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionManagement");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "80cad8f9fea5488187c09686cf6970a7");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4503), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4496), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4494), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4554), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4492), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4487), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4558), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4485), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4500), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4513), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4556), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4552), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4505), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4511), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4501), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4550), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4508), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4489), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4490), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4560), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4509), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4498), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4483), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4480), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4153), "dfd6904295934d79a8e36842c9352d7a", "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4181), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4183), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "dfd6904295934d79a8e36842c9352d7a", new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4147), "dfd6904295934d79a8e36842c9352d7a", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4459), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4454), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4449), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4443), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4438), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4433), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4427), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4422), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4414), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4409), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4402), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4397), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4392), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4387), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4354), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4349), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4344), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4340), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4334), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4329), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4324), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4318), "dfd6904295934d79a8e36842c9352d7a" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 9, 2, 10, 20, 44, 275, DateTimeKind.Utc).AddTicks(4309), "dfd6904295934d79a8e36842c9352d7a" });
        }
    }
}

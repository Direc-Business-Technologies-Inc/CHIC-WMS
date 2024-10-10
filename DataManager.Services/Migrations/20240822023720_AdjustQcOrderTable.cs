using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustQcOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "USRL",
            //    keyColumn: "UserId",
            //    keyValue: "59283224c58a4329b8b9dac749d39df8");

            migrationBuilder.AddColumn<string>(
                name: "DosimetryUsed",
                table: "QCOR",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5879), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5865), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5861), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5939), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5858), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5848), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5946), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5844), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5872), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5897), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5943), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5904), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5883), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5894), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5876), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5900), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5888), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5851), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5855), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5949), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5891), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5868), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5840), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5834), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(4850), "e2e08b73721b486f82fa5521f03ab741", "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5003), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5007), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "e2e08b73721b486f82fa5521f03ab741", new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(4835), "e2e08b73721b486f82fa5521f03ab741", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5792), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5783), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5774), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5765), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5757), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5748), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5736), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5726), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5717), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5708), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5699), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5690), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5682), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5625), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5614), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5606), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5598), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5589), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5578), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5570), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5562), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5552), "e2e08b73721b486f82fa5521f03ab741" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 8, 22, 2, 37, 20, 431, DateTimeKind.Utc).AddTicks(5534), "e2e08b73721b486f82fa5521f03ab741" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "USRL",
            //    keyColumn: "UserId",
            //    keyValue: "e2e08b73721b486f82fa5521f03ab741");

            migrationBuilder.DropColumn(
                name: "DosimetryUsed",
                table: "QCOR");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2338), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2325), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2322), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2358), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2319), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2310), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2367), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2308), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2333), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2351), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2361), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2356), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2340), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2348), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2335), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2353), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2343), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2313), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2316), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2369), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2346), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2327), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2304), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2301), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1286), "59283224c58a4329b8b9dac749d39df8", "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1558), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1563), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "59283224c58a4329b8b9dac749d39df8", new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1273), "59283224c58a4329b8b9dac749d39df8", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2249), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2239), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2230), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2220), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2211), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2201), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2190), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2180), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2120), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2111), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2100), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2086), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2070), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2055), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2038), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2023), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2009), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1993), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1977), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1963), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1948), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1933), "59283224c58a4329b8b9dac749d39df8" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(1908), "59283224c58a4329b8b9dac749d39df8" });
        }
    }
}

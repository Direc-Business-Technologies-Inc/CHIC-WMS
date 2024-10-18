using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddPositionManagementinMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "37bfce64b4524690a83c8780485ec21c");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(42), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(30), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(27), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(64), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(23), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(13), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(70), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(10), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(36), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(56), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(67), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(61), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(45), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(53), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(39), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(58), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(47), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(17), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(20), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(72), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(50), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(32), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(7), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(3), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[] { "POSM", true, new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(75), "1233ef03ab8942be98e55858356014c7", "This is where user can view and update Position", "Administration", "bx bx-home-circle", "bx bx-home-circle", "fa-solid fa-users", 24, "Position Management", "Users", null, null, "/PositionManagement" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9292), "1233ef03ab8942be98e55858356014c7", "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9377), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9380), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "1233ef03ab8942be98e55858356014c7", new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9284), "1233ef03ab8942be98e55858356014c7", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9949), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9940), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9931), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9921), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9912), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9902), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9892), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9883), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9873), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9864), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9710), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9697), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9688), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9679), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9662), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9653), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9644), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9635), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9626), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9617), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9609), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9599), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9586), "1233ef03ab8942be98e55858356014c7" });

            migrationBuilder.InsertData(
                table: "USRM",
                columns: new[] { "Id", "CanCreate", "CanUpdate", "CreatedDate", "CreatedUserId", "IsReadOnly", "ModuleId", "UpdatedDate", "UpdatedUserId", "UserGroupId" },
                values: new object[] { -24, true, true, new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9959), "1233ef03ab8942be98e55858356014c7", true, "POSM", null, null, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "1233ef03ab8942be98e55858356014c7");

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -24);

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "POSM");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5604), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5595), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5593), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5622), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5590), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5583), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5626), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5580), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5600), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5615), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5624), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5620), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5606), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5613), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5602), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5618), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5608), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5585), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5588), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5628), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5611), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5597), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5578), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5575), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(4797), "37bfce64b4524690a83c8780485ec21c", "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(4880), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(4882), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "37bfce64b4524690a83c8780485ec21c", new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(4789), "37bfce64b4524690a83c8780485ec21c", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5527), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5515), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5501), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5443), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5432), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5420), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5399), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5387), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5375), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5364), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5328), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5312), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5301), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5289), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5276), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5265), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5252), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5240), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5227), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5216), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5197), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5185), "37bfce64b4524690a83c8780485ec21c" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 10, 9, 53, 11, 330, DateTimeKind.Utc).AddTicks(5169), "37bfce64b4524690a83c8780485ec21c" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultParamForQCMaintenance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "75a7ff1fcb9449288ad037c7eb2876dd");

            migrationBuilder.AddColumn<bool>(
                name: "DefaultParameter",
                table: "CIP1",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6123), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6115), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6113), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6141), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6111), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6105), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6103), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6119), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6136), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6139), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6124), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6134), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6121), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6138), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6126), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6107), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6108), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6132), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6117), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6101), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6098), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5664), "7b240ee0654c42b899ec66281d96cccd", "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5756), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5758), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "7b240ee0654c42b899ec66281d96cccd", new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5651), "7b240ee0654c42b899ec66281d96cccd", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "7b240ee0654c42b899ec66281d96cccd");

            migrationBuilder.DropColumn(
                name: "DefaultParameter",
                table: "CIP1");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3811), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3798), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3795), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3832), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3792), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3778), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3699), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3804), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3824), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3829), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3813), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3821), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3808), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3826), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3816), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3785), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3788), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3818), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3801), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3696), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3686), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(2773), "75a7ff1fcb9449288ad037c7eb2876dd", "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3153), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3157), "75a7ff1fcb9449288ad037c7eb2876dd" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "75a7ff1fcb9449288ad037c7eb2876dd", new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(2751), "75a7ff1fcb9449288ad037c7eb2876dd", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }
    }
}

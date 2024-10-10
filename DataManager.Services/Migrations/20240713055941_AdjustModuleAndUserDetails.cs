using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModuleAndUserDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "29f76e9fb64d4702a35ae9190e83d180");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7496), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7485), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7483), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7521), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7480), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7467), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7526), "1f87e96704cd4e13941302fb8c65adcc", "bx bx-export" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7465), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7491), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7509), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7523), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7518), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7499), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7507), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7493), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7515), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7501), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7470), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7477), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7504), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7488), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7462), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7457), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "CreatedDate", "CreatedUserId", "FirstName", "UserId" },
                values: new object[] { "", new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(6706), "1f87e96704cd4e13941302fb8c65adcc", "Company", "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(6799), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(6802), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "1f87e96704cd4e13941302fb8c65adcc", new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(6695), "1f87e96704cd4e13941302fb8c65adcc", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7358), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7345), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7333), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7321), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7309), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7295), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7283), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7272), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7259), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7247), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7235), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7224), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7212), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7199), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7187), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7176), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7164), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7151), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7139), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7127), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7115), "1f87e96704cd4e13941302fb8c65adcc" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7098), "1f87e96704cd4e13941302fb8c65adcc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "1f87e96704cd4e13941302fb8c65adcc");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7827), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7814), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7771), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7854), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7769), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7760), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7859), "29f76e9fb64d4702a35ae9190e83d180", "bx bxs-radiation" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7752), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7819), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7846), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7857), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7851), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7830), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7843), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7824), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7849), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7837), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7763), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7766), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7840), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7817), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7749), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7744), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Company", "CreatedDate", "CreatedUserId", "FirstName", "UserId" },
                values: new object[] { "Direc Business Technologies Inc", new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(6677), "29f76e9fb64d4702a35ae9190e83d180", "Direc", "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(6937), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(6948), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "29f76e9fb64d4702a35ae9190e83d180", new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(6666), "29f76e9fb64d4702a35ae9190e83d180", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7670), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7657), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7644), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7631), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7618), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7602), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7590), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7577), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7564), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7551), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7539), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7528), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7513), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7500), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7489), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7475), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7463), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7448), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7379), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7365), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7353), "29f76e9fb64d4702a35ae9190e83d180" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7332), "29f76e9fb64d4702a35ae9190e83d180" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDispatchNotificationsModuleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "f7eb122216514fa0b5cf269e7ba327f2");

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
                columns: new[] { "CreatedDate", "CreatedUserId", "WebLink" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(7859), "29f76e9fb64d4702a35ae9190e83d180", "/DispatchNotifications" });

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
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 7, 11, 5, 21, 35, 300, DateTimeKind.Utc).AddTicks(6677), "29f76e9fb64d4702a35ae9190e83d180", "29f76e9fb64d4702a35ae9190e83d180" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1253), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1239), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1236), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1504), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1234), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1228), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId", "WebLink" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1511), "f7eb122216514fa0b5cf269e7ba327f2", "/EBOperations" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1226), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1249), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1263), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1508), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1267), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1255), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1261), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1251), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1265), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1257), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1230), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1232), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1259), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1241), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1223), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1219), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(147), "f7eb122216514fa0b5cf269e7ba327f2", "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(235), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(237), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "f7eb122216514fa0b5cf269e7ba327f2", new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(138), "f7eb122216514fa0b5cf269e7ba327f2", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1178), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1170), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1163), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1155), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1148), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1138), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1130), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1123), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1116), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1109), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1102), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1095), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1083), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(493), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(486), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(479), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(472), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(464), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(456), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(449), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(440), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(429), "f7eb122216514fa0b5cf269e7ba327f2" });
        }
    }
}

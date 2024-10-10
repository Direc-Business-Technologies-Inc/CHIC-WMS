using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModulesActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "7dc1a837fea24af38f1126326ace2896");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1253), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1239), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1236), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1504), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1234), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1228), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1511), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1226), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1249), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1263), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1508), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1267), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1255), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1261), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1251), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1265), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1257), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1230), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1232), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1259), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1241), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1223), "f7eb122216514fa0b5cf269e7ba327f2" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { true, new DateTime(2024, 7, 10, 5, 48, 37, 754, DateTimeKind.Utc).AddTicks(1219), "f7eb122216514fa0b5cf269e7ba327f2" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "f7eb122216514fa0b5cf269e7ba327f2");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4133), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4117), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4114), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4162), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4111), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4100), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4169), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4096), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4125), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4152), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4165), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4159), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4139), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4149), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4128), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4155), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4142), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4104), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4107), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4146), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4121), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4086), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "Active", "CreatedDate", "CreatedUserId" },
                values: new object[] { false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4081), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(2656), "7dc1a837fea24af38f1126326ace2896", "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(2952), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(2957), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "7dc1a837fea24af38f1126326ace2896", new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(2629), "7dc1a837fea24af38f1126326ace2896", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3967), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3950), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3934), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3918), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3901), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3882), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3866), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3849), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3833), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3815), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3799), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3783), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3767), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3748), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3732), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3717), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3701), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3683), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3666), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3650), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3634), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3599), "7dc1a837fea24af38f1126326ace2896" });
        }
    }
}

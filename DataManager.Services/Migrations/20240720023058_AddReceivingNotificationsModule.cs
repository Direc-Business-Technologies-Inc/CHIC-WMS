using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivingNotificationsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[] { "RCNT", true, new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2369), "59283224c58a4329b8b9dac749d39df8", "This is where user can view and confirm sales orders that are to be received", "Dashboard", "bx bx-import", "bx bx-box", "-", 23, "Receiving Notifications", "-", null, null, "/ReceivingNotifications" });

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

            migrationBuilder.InsertData(
                table: "USRM",
                columns: new[] { "Id", "CanCreate", "CanUpdate", "CreatedDate", "CreatedUserId", "IsReadOnly", "ModuleId", "UpdatedDate", "UpdatedUserId", "UserGroupId" },
                values: new object[] { -23, true, true, new DateTime(2024, 7, 20, 2, 30, 58, 42, DateTimeKind.Utc).AddTicks(2249), "59283224c58a4329b8b9dac749d39df8", true, "RCNT", null, null, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "59283224c58a4329b8b9dac749d39df8");

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23);

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT");

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
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(7526), "1f87e96704cd4e13941302fb8c65adcc" });

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
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 7, 13, 5, 59, 40, 849, DateTimeKind.Utc).AddTicks(6706), "1f87e96704cd4e13941302fb8c65adcc", "1f87e96704cd4e13941302fb8c65adcc" });

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
    }
}

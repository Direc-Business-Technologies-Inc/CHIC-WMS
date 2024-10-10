using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustQCOrderModuleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "6b6c5f198f3843159406602d36c023ef");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "QCOR",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "QCOR",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DosimetryReportNo",
                table: "COR4",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7548), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7532), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7537), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7542), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7550), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7556), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7544), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7552), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7535), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7546), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7554), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7540), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7527), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7529), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7524), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(6867), "2da7df7bde034f3a813a17f99a228fde", "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7150), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(7153), "2da7df7bde034f3a813a17f99a228fde" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "2da7df7bde034f3a813a17f99a228fde", new DateTime(2023, 11, 16, 1, 29, 57, 902, DateTimeKind.Utc).AddTicks(6852), "2da7df7bde034f3a813a17f99a228fde", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "2da7df7bde034f3a813a17f99a228fde");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "QCOR");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "QCOR");

            migrationBuilder.DropColumn(
                name: "DosimetryReportNo",
                table: "COR4");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6010), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5992), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5996), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6001), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6012), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6018), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6006), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6014), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5994), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6008), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(6016), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5998), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5988), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5990), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5985), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5652), "6b6c5f198f3843159406602d36c023ef", "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5783), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5785), "6b6c5f198f3843159406602d36c023ef" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "6b6c5f198f3843159406602d36c023ef", new DateTime(2023, 11, 13, 2, 9, 18, 433, DateTimeKind.Utc).AddTicks(5645), "6b6c5f198f3843159406602d36c023ef", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }
    }
}

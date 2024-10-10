using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class COIAdjustmentLinkage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OCOI_QCOrderNo",
                table: "OCOI");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "913c4c48e28f4f89abe431f849ed7699");

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

            migrationBuilder.CreateIndex(
                name: "IX_OCOI_QCOrderNo",
                table: "OCOI",
                column: "QCOrderNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OCOI_QCOrderNo",
                table: "OCOI");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "6b6c5f198f3843159406602d36c023ef");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8908), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8891), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8896), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8901), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8911), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8920), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8903), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8916), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8893), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8906), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8918), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8898), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8885), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8888), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8881), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8543), "913c4c48e28f4f89abe431f849ed7699", "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8662), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8665), "913c4c48e28f4f89abe431f849ed7699" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "913c4c48e28f4f89abe431f849ed7699", new DateTime(2023, 11, 7, 5, 50, 39, 200, DateTimeKind.Utc).AddTicks(8533), "913c4c48e28f4f89abe431f849ed7699", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_OCOI_QCOrderNo",
                table: "OCOI",
                column: "QCOrderNo",
                unique: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class updatePositionManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "80cad8f9fea5488187c09686cf6970a7");

            migrationBuilder.AddColumn<string>(
                name: "PosName",
                table: "PositionManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "37bfce64b4524690a83c8780485ec21c");

            migrationBuilder.DropColumn(
                name: "PosName",
                table: "PositionManagement");

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
    }
}

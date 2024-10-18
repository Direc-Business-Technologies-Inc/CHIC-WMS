using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddClassificationPM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "1233ef03ab8942be98e55858356014c7");

            migrationBuilder.AlterColumn<string>(
                name: "PosName",
                table: "PositionManagement",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "PositionManagement",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7008), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6994), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6991), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7040), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6987), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6975), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7047), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6971), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7001), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7029), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7043), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7036), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7012), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7023), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7005), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7032), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7016), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "POSM",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7054), "175cfee3d49645659babbe777e44c672", "-" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6979), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6983), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7050), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(7019), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6997), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6966), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6961), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(5579), "175cfee3d49645659babbe777e44c672", "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6061), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6065), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "175cfee3d49645659babbe777e44c672", new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(5563), "175cfee3d49645659babbe777e44c672", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -24,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6836), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -23,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6826), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6814), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6803), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6794), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6783), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6772), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6761), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6751), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6738), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6718), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6700), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6684), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6666), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6647), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6628), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6612), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6595), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6577), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6557), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6541), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6524), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6506), "175cfee3d49645659babbe777e44c672" });

            migrationBuilder.UpdateData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 18, 3, 10, 21, 569, DateTimeKind.Utc).AddTicks(6366), "175cfee3d49645659babbe777e44c672" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "175cfee3d49645659babbe777e44c672");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "PositionManagement");

            migrationBuilder.AlterColumn<string>(
                name: "PosName",
                table: "PositionManagement",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 3);

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
                keyValue: "POSM",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 577, DateTimeKind.Utc).AddTicks(75), "1233ef03ab8942be98e55858356014c7", "bx bx-home-circle" });

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
                keyValue: -24,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 10, 11, 9, 4, 42, 576, DateTimeKind.Utc).AddTicks(9959), "1233ef03ab8942be98e55858356014c7" });

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
        }
    }
}

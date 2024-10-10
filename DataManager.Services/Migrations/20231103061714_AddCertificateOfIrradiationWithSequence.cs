using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificateOfIrradiationWithSequence : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
		    migrationBuilder.Sql("CREATE SEQUENCE COISequence AS int START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 9999");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "9f7e920b817349cf870706724d71a739");

            migrationBuilder.CreateTable(
                name: "OCOI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCOrderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Requester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QCRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COINumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproverRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCOI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OCOI_QCOR_QCOrderNo",
                        column: x => x.QCOrderNo,
                        principalTable: "QCOR",
                        principalColumn: "QCOrderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6429), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6414), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6419), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6423), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6432), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6443), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6425), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6438), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6417), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6427), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6441), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6421), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6410), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6412), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6407), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6077), "8d03283b0cfc4d4e91544f163ef2c6bd", "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6216), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6218), "8d03283b0cfc4d4e91544f163ef2c6bd" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "8d03283b0cfc4d4e91544f163ef2c6bd", new DateTime(2023, 11, 3, 6, 17, 14, 701, DateTimeKind.Utc).AddTicks(6067), "8d03283b0cfc4d4e91544f163ef2c6bd", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_OCOI_QCOrderNo",
                table: "OCOI",
                column: "QCOrderNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP SEQUENCE COISequence");

			migrationBuilder.DropTable(
                name: "OCOI");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "8d03283b0cfc4d4e91544f163ef2c6bd");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2792), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2774), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2781), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2785), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2794), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2800), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2787), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2796), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2776), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2789), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2798), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2783), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2770), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2772), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2767), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2453), "9f7e920b817349cf870706724d71a739", "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2570), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2572), "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "9f7e920b817349cf870706724d71a739", new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2444), "9f7e920b817349cf870706724d71a739", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }
    }
}

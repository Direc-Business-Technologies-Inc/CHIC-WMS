using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "eb6061c519c54f66a9807c5812726ed0");

            migrationBuilder.CreateTable(
                name: "Configurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfigurationId = table.Column<int>(type: "int", nullable: false),
                    SubGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationItems_Configurations_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Configurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Configurations",
                columns: new[] { "Id", "Code", "Name", "Sequence" },
                values: new object[] { 1, "QC_MAINTENANCE", "QC Maintenance", 1 });

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

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[,]
                {
                    { "CNFG", false, new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3832), "75a7ff1fcb9449288ad037c7eb2876dd", "This is where user can adjust system configurations", "Administration", "bx bx-cog", "bx bx-home-circle", "-", 20, "Configurations", "-", null, null, "/Configurations" },
                    { "FRMR", false, new DateTime(2024, 4, 3, 1, 16, 28, 928, DateTimeKind.Utc).AddTicks(3829), "75a7ff1fcb9449288ad037c7eb2876dd", "This is where user can print forms and reports.", "Forms and Reports", "bx bx-library", "bx bx-printer", "-", 19, "Forms And Reports", "-", null, null, "/FormsAndReports" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationItems_ConfigurationId",
                table: "ConfigurationItems",
                column: "ConfigurationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationItems");

            migrationBuilder.DropTable(
                name: "Configurations");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "75a7ff1fcb9449288ad037c7eb2876dd");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7324), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7311), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7308), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7305), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7295), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7292), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7318), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7343), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7331), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7340), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7321), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7351), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7334), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7299), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7302), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7337), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7314), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7288), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7283), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(6666), "eb6061c519c54f66a9807c5812726ed0", "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(6992), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(6997), "eb6061c519c54f66a9807c5812726ed0" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "eb6061c519c54f66a9807c5812726ed0", new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(6647), "eb6061c519c54f66a9807c5812726ed0", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }
    }
}

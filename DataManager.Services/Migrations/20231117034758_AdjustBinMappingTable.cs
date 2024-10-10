using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustBinMappingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "2da7df7bde034f3a813a17f99a228fde");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "OBMP",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Shelf",
                table: "OBMP",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseCode",
                table: "OBMP",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9954), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9936), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9940), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9948), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9956), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9963), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9950), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9959), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9938), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9952), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9961), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9945), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9931), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9933), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9928), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9686), "8e6378f0534e48fdbe899aa4ac41811c", "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9777), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9779), "8e6378f0534e48fdbe899aa4ac41811c" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "8e6378f0534e48fdbe899aa4ac41811c", new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9678), "8e6378f0534e48fdbe899aa4ac41811c", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "8e6378f0534e48fdbe899aa4ac41811c");

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseName",
                table: "OBMP",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Shelf",
                table: "OBMP",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "WarehouseCode",
                table: "OBMP",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 0);

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
    }
}

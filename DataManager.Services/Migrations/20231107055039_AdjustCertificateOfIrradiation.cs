using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustCertificateOfIrradiation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "8d03283b0cfc4d4e91544f163ef2c6bd");

            migrationBuilder.RenameColumn(
                name: "Requester",
                table: "OCOI",
                newName: "QCRequester");

            migrationBuilder.RenameColumn(
                name: "COINumber",
                table: "OCOI",
                newName: "MinValue");

            migrationBuilder.AddColumn<string>(
                name: "ActualValue",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproverIssuedDate",
                table: "OCOI",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproverJobTitle",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApproverName",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CertificateOfIrradiationNumber",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPONo",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DocNo",
                table: "OCOI",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FacilityName",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "IrradiationDate",
                table: "OCOI",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingLotNo",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaxValue",
                table: "OCOI",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalNoOfBoxes",
                table: "OCOI",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "913c4c48e28f4f89abe431f849ed7699");

            migrationBuilder.DropColumn(
                name: "ActualValue",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ApproverIssuedDate",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ApproverJobTitle",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ApproverName",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "CertificateOfIrradiationNumber",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "CustomerPONo",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "DocNo",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "FacilityName",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "IrradiationDate",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "ManufacturingLotNo",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "OCOI");

            migrationBuilder.DropColumn(
                name: "TotalNoOfBoxes",
                table: "OCOI");

            migrationBuilder.RenameColumn(
                name: "QCRequester",
                table: "OCOI",
                newName: "Requester");

            migrationBuilder.RenameColumn(
                name: "MinValue",
                table: "OCOI",
                newName: "COINumber");

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
        }
    }
}

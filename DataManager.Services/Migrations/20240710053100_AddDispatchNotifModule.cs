using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddDispatchNotifModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "7b240ee0654c42b899ec66281d96cccd");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4133), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4117), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4114), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4162), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4111), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4100), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4096), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4125), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4152), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4159), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4139), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4149), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4128), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4155), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4142), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4104), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4107), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4146), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4121), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4086), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4081), "7dc1a837fea24af38f1126326ace2896" });

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[,]
                {
                    { "DPNT", false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4169), "7dc1a837fea24af38f1126326ace2896", "This is where user can view and confirm sales orders that are to be dispatched", "Dashboard", "bx bxs-radiation", "bx bx-box", "-", 22, "Dispatch Notifications", "-", null, null, "/EBOperations" },
                    { "EBOP", false, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(4165), "7dc1a837fea24af38f1126326ace2896", "This is where user can view and verify sales orders that are to be irradiated", "Quality Control", "bx bxs-radiation", "bx bx-search-alt", "-", 21, "EB Operations", "-", null, null, "/EBOperations" }
                });

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

            migrationBuilder.InsertData(
                table: "USRM",
                columns: new[] { "Id", "CanCreate", "CanUpdate", "CreatedDate", "CreatedUserId", "IsReadOnly", "ModuleId", "UpdatedDate", "UpdatedUserId", "UserGroupId" },
                values: new object[,]
                {
                    { -20, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3934), "7dc1a837fea24af38f1126326ace2896", true, "CNFG", null, null, 2 },
                    { -19, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3918), "7dc1a837fea24af38f1126326ace2896", true, "USRM", null, null, 2 },
                    { -18, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3901), "7dc1a837fea24af38f1126326ace2896", true, "USRG", null, null, 2 },
                    { -17, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3882), "7dc1a837fea24af38f1126326ace2896", true, "RCVN", null, null, 2 },
                    { -16, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3866), "7dc1a837fea24af38f1126326ace2896", true, "MNTR", null, null, 2 },
                    { -15, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3849), "7dc1a837fea24af38f1126326ace2896", true, "INVT", null, null, 2 },
                    { -14, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3833), "7dc1a837fea24af38f1126326ace2896", true, "DSPT", null, null, 2 },
                    { -13, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3815), "7dc1a837fea24af38f1126326ace2896", true, "RSCD", null, null, 2 },
                    { -12, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3799), "7dc1a837fea24af38f1126326ace2896", true, "ISCD", null, null, 2 },
                    { -11, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3783), "7dc1a837fea24af38f1126326ace2896", true, "DSCD", null, null, 2 },
                    { -10, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3767), "7dc1a837fea24af38f1126326ace2896", true, "DSBD", null, null, 2 },
                    { -9, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3748), "7dc1a837fea24af38f1126326ace2896", true, "DBNT", null, null, 2 },
                    { -8, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3732), "7dc1a837fea24af38f1126326ace2896", true, "QCOR", null, null, 2 },
                    { -7, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3717), "7dc1a837fea24af38f1126326ace2896", true, "QCMT", null, null, 2 },
                    { -6, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3701), "7dc1a837fea24af38f1126326ace2896", true, "COAP", null, null, 2 },
                    { -5, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3683), "7dc1a837fea24af38f1126326ace2896", true, "BNMP", null, null, 2 },
                    { -4, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3666), "7dc1a837fea24af38f1126326ace2896", true, "BNDB", null, null, 2 },
                    { -3, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3650), "7dc1a837fea24af38f1126326ace2896", true, "PLPR", null, null, 2 },
                    { -2, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3634), "7dc1a837fea24af38f1126326ace2896", true, "ILPR", null, null, 2 },
                    { -1, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3599), "7dc1a837fea24af38f1126326ace2896", true, "BLPR", null, null, 2 },
                    { -22, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3967), "7dc1a837fea24af38f1126326ace2896", true, "DPNT", null, null, 2 },
                    { -21, true, true, new DateTime(2024, 7, 10, 5, 30, 59, 949, DateTimeKind.Utc).AddTicks(3950), "7dc1a837fea24af38f1126326ace2896", true, "EBOP", null, null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "7dc1a837fea24af38f1126326ace2896");

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -22);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -21);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -20);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -17);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -11);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "USRM",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DPNT");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "EBOP");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6123), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6115), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6113), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "CNFG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6141), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6111), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6105), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6103), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6119), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6136), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "FRMR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6139), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6124), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6134), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6121), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6138), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6126), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6107), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6108), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6132), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6117), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6101), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM",
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(6098), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRD",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId", "UserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5664), "7b240ee0654c42b899ec66281d96cccd", "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5756), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.UpdateData(
                table: "USRG",
                keyColumn: "UserGroupId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "CreatedUserId" },
                values: new object[] { new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5758), "7b240ee0654c42b899ec66281d96cccd" });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "7b240ee0654c42b899ec66281d96cccd", new DateTime(2024, 4, 5, 1, 3, 34, 306, DateTimeKind.Utc).AddTicks(5651), "7b240ee0654c42b899ec66281d96cccd", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });
        }
    }
}

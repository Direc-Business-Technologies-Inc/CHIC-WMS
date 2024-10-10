using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class AdjustModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRA");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRL");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "8e6378f0534e48fdbe899aa4ac41811c");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7324), "eb6061c519c54f66a9807c5812726ed0", "This is where user can print bin labels.", "bx bx-file", "bx bx-printer", 12 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7311), "eb6061c519c54f66a9807c5812726ed0", "This is where user can see the Bin Map and Bin Status.", "Bins", "bx bx-map-alt", "bx bx-cabinet", 8 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7308), "eb6061c519c54f66a9807c5812726ed0", "This is where user can see map the bins.", "bx bx-map-pin", "bx bx-cabinet", 7 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7318), "eb6061c519c54f66a9807c5812726ed0", "This is where user can view the Dispatch Schedule.", "bx bx-package", "bx bx-calendar", 10 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7331), "eb6061c519c54f66a9807c5812726ed0", "This is where user can print irradiation labels.", "bx bx-file", "bx bx-printer", 13 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7340), "eb6061c519c54f66a9807c5812726ed0", "This is where user can transfer inventory automatically.", "Inventory", "bx bx-transfer", "bx bx-archive", 16 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7321), "eb6061c519c54f66a9807c5812726ed0", "This is where user can view the Irradiation Schedule.", "bx bx-shield", "bx bx-calendar", 11 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7334), "eb6061c519c54f66a9807c5812726ed0", "This is where user can print pallet labels.", "bx bx-file", "bx bx-printer", 14 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7299), "eb6061c519c54f66a9807c5812726ed0", "This is where user can create Inspection Plans for specific items.", "Quality Control", "bx bx-search-alt", "bx bx-search-alt" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7302), "eb6061c519c54f66a9807c5812726ed0", "This is where user can input QC Order details.", "Quality Control", "bx bx-user-check", "bx bx-search-alt", 5 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7337), "eb6061c519c54f66a9807c5812726ed0", "This is where user can receive sales orders.", "Inventory", "bx bx-archive-in", "bx bx-archive", 15 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7314), "eb6061c519c54f66a9807c5812726ed0", "This is where user can view the Receiving Schedule.", "bx bxs-truck", "bx bx-calendar", 9 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon", "IconGroup", "IconSubGroup", "LineNum", "SubGroupName", "WebLink" },
                values: new object[] { new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7288), "eb6061c519c54f66a9807c5812726ed0", "-", "bx bx-home-circle", "fa-solid fa-users", 1, "Users", "/UserGroup" });

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[,]
                {
                    { "COAP", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7305), "eb6061c519c54f66a9807c5812726ed0", "This is where user can approve and print certificate of irradiation.", "Quality Control", "fa fa-person-circle-check", "bx bx-search-alt", "-", 6, "COI Approval", "-", null, null, "/CertificateOfIrradiationApproval" },
                    { "DBNT", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7295), "eb6061c519c54f66a9807c5812726ed0", "This is where you can view sales orders that are to be received or dispatched.", "Dashboard", "bx bx-home-alt", "bx bx-pie-chart-alt-2", "-", 3, "Dashboard Notification", "-", null, null, "/DashboardNotification" },
                    { "DSBD", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7292), "eb6061c519c54f66a9807c5812726ed0", "This is where you can view the statistics of sales orders.", "Dashboard", "bx bx-home-alt", "bx bx-pie-chart-alt-2", "-", 2, "Dashboard", "-", null, null, "/Home" },
                    { "DSPT", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7343), "eb6061c519c54f66a9807c5812726ed0", "This is where user can release items.", "Inventory", "bx bx-archive-out", "bx bx-archive", "-", 17, "Dispatch", "-", null, null, "/Dispatch" },
                    { "MNTR", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7351), "eb6061c519c54f66a9807c5812726ed0", "This is where user can transfer inventory manually.", "Inventory", "bx bx-transfer", "bx bx-archive", "-", 18, "Manual Transfer", "-", null, null, "/InventoryTransfer/ManualTransfer" },
                    { "USRM", false, new DateTime(2024, 2, 1, 6, 23, 34, 728, DateTimeKind.Utc).AddTicks(7283), "eb6061c519c54f66a9807c5812726ed0", "This is where you can create, update and delete user information.", "Administration", "-", "bx bx-home-circle", "fa-solid fa-users", 0, "User Management", "Users", null, null, "/UserManagement" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "COAP");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DBNT");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSBD");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSPT");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "MNTR");

            migrationBuilder.DeleteData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRM");

            migrationBuilder.DeleteData(
                table: "USRL",
                keyColumn: "UserId",
                keyValue: "eb6061c519c54f66a9807c5812726ed0");

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BLPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9954), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can print bin labels", "fa-file-lines", "-", 10 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNDB",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9936), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can see the Bin Map and Bin Status", "Dashboard", "fa-map", "-", 3 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "BNMP",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9940), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can see map the bins", "fa-map", "-", 5 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "DSCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9948), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can view the Dispatch Schedule", "fa-box", "-", 7 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ILPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9956), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can print irradiation labels", "fa-file-lines", "-", 11 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "INVT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9963), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can execute inventory transfer", "Inventory Transfer", "fa-dolly", "-", 14 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "ISCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9950), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can view the Irradiation Schedule", "fa-shield", "-", 8 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "PLPR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9959), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can print pallet labels", "fa-file-lines", "-", 12 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCMT",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9938), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can create Inspection Plans for specific items", "QC Maintenance", "fa-magnifying-glass", "-" });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "QCOR",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9952), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can input QC Order details", "QC Order", "fa-user-check", "-", 9 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RCVN",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9961), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can receive sales orders", "Receiving", "fa-truck-ramp-box", "-", 13 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "RSCD",
                columns: new[] { "CreatedDate", "CreatedUserId", "Description", "Icon", "IconGroup", "LineNum" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9945), "8e6378f0534e48fdbe899aa4ac41811c", "This is where user can view the Receiving Schedule", "fa-truck", "-", 6 });

            migrationBuilder.UpdateData(
                table: "MODL",
                keyColumn: "ModuleId",
                keyValue: "USRG",
                columns: new[] { "CreatedDate", "CreatedUserId", "Icon", "IconGroup", "IconSubGroup", "LineNum", "SubGroupName", "WebLink" },
                values: new object[] { new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9933), "8e6378f0534e48fdbe899aa4ac41811c", "fa-list-ol", "fa-user-shield", "fa-users", 2, "User Management", "/USRG" });

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[,]
                {
                    { "USRA", false, new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9931), "8e6378f0534e48fdbe899aa4ac41811c", "This is where you can create, update and delete user authorization.", "Administration", "fa-unlock", "fa-user-shield", "fa-users", 1, "User Authorization", "User Management", null, null, "/USRA" },
                    { "USRL", false, new DateTime(2023, 11, 17, 3, 47, 57, 811, DateTimeKind.Utc).AddTicks(9928), "8e6378f0534e48fdbe899aa4ac41811c", "This is where you can create, update and delete user information.", "Administration", "fa-id-card", "fa-user-shield", "fa-users", 0, "User Login Info", "User Management", null, null, "/USRL" }
                });

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
    }
}

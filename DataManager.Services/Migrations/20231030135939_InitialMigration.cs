using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataManager.Services.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create a sequence
            migrationBuilder.Sql("CREATE SEQUENCE MySequence AS int START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 9999");

            migrationBuilder.Sql("CREATE SEQUENCE QCOrderSequence AS int START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 9999");

            migrationBuilder.CreateTable(
                name: "MODL",
                columns: table => new
                {
                    ModuleId = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    LineNum = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(254)", nullable: false),
                    Icon = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    WebLink = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IconGroup = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    GroupName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IconSubGroup = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    SubGroupName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODL", x => x.ModuleId);
                });

            migrationBuilder.CreateTable(
                name: "OBAS",
                columns: table => new
                {
                    BinCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SONo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PalletNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBAS", x => x.BinCode);
                });

            migrationBuilder.CreateTable(
                name: "OBMP",
                columns: table => new
                {
                    WarehouseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBMP", x => new { x.WarehouseCode, x.Shelf });
                });

            migrationBuilder.CreateTable(
                name: "QCIP",
                columns: table => new
                {
                    InspectionPlanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectionPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approver = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfSamples = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalNumberOfBoxes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SamplePassTolerancePercentage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallPassTolerancePercentage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDosimeters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosimeterLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalWeight = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCIP", x => new { x.InspectionPlanCode, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "QCOR",
                columns: table => new
                {
                    QCOrderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InspectionPlanCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionPlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionPlanType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocNo = table.Column<int>(type: "int", nullable: false),
                    DocDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UoM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SampleSize = table.Column<int>(type: "int", nullable: false),
                    SamplePassTolerancePercentage = table.Column<float>(type: "real", nullable: false),
                    OverallPassTolerancePercentage = table.Column<float>(type: "real", nullable: false),
                    PONo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManufacturingLotNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageConditions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCOR", x => x.QCOrderNo);
                });

            migrationBuilder.CreateTable(
                name: "USRG",
                columns: table => new
                {
                    UserGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USRG", x => x.UserGroupId);
                });

            migrationBuilder.CreateTable(
                name: "BPM1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarehouseCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Shelf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Left = table.Column<float>(type: "real", nullable: false),
                    Top = table.Column<float>(type: "real", nullable: false),
                    Radius = table.Column<float>(type: "real", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Aisle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPM1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BPM1_OBMP_WarehouseCode_Shelf",
                        columns: x => new { x.WarehouseCode, x.Shelf },
                        principalTable: "OBMP",
                        principalColumns: new[] { "WarehouseCode", "Shelf" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CIP1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionPlanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UoM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    MinValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIP1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CIP1_QCIP_InspectionPlanCode_Version",
                        columns: x => new { x.InspectionPlanCode, x.Version },
                        principalTable: "QCIP",
                        principalColumns: new[] { "InspectionPlanCode", "Version" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COR1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCOrderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Open = table.Column<int>(type: "int", nullable: false),
                    TotalNoOfPassed = table.Column<int>(type: "int", nullable: false),
                    TotalNoOfFailed = table.Column<int>(type: "int", nullable: false),
                    TotalNoSamples = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COR1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COR1_QCOR_QCOrderNo",
                        column: x => x.QCOrderNo,
                        principalTable: "QCOR",
                        principalColumn: "QCOrderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COR4",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCOrderNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EBOperationLog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualEnergy = table.Column<int>(type: "int", nullable: false),
                    ActualPower = table.Column<int>(type: "int", nullable: false),
                    ActualFrequency = table.Column<int>(type: "int", nullable: false),
                    TotalProductsBeforeIrradiation = table.Column<int>(type: "int", nullable: false),
                    TotalProductsAfterIrradiation = table.Column<int>(type: "int", nullable: false),
                    NCReport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COR4", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COR4_QCOR_QCOrderNo",
                        column: x => x.QCOrderNo,
                        principalTable: "QCOR",
                        principalColumn: "QCOrderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USRL",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    LastPassword = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    LastPasswordSet = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    FailedAttemptCount = table.Column<int>(type: "int", nullable: false),
                    FailedAttempt = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    IsLockedoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsTwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ResetAttempt = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USRL", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_USRL_USRG_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "USRG",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USRM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    CanCreate = table.Column<bool>(type: "bit", nullable: false),
                    CanUpdate = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USRM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USRM_MODL_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "MODL",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USRM_USRG_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "USRG",
                        principalColumn: "UserGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COR2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SampleId = table.Column<int>(type: "int", nullable: false),
                    SampleNo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfPassed = table.Column<int>(type: "int", nullable: false),
                    NoOfFailed = table.Column<int>(type: "int", nullable: false),
                    QABy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COR2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COR2_COR1_SampleId",
                        column: x => x.SampleId,
                        principalTable: "COR1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USRD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    MiddleName = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Company = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Department = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    IsPhoneConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Background = table.Column<byte>(type: "tinyint", nullable: true),
                    DisplayPicture = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    UpdatedUserId = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USRD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_USRD_USRL_UserId",
                        column: x => x.UserId,
                        principalTable: "USRL",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COR3",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SampleId = table.Column<int>(type: "int", nullable: false),
                    ParameterNo = table.Column<int>(type: "int", nullable: false),
                    Parameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParameterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UoM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    MinValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COR3", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COR3_COR2_SampleId",
                        column: x => x.SampleId,
                        principalTable: "COR2",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MODL",
                columns: new[] { "ModuleId", "Active", "CreatedDate", "CreatedUserId", "Description", "GroupName", "Icon", "IconGroup", "IconSubGroup", "LineNum", "Name", "SubGroupName", "UpdatedDate", "UpdatedUserId", "WebLink" },
                values: new object[,]
                {
                    { "BLPR", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2792), "9f7e920b817349cf870706724d71a739", "This is where user can print bin labels", "Forms and Reports", "fa-file-lines", "-", "-", 10, "Bin Label Printing", "-", null, null, "/BinLabelPrinting" },
                    { "BNDB", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2774), "9f7e920b817349cf870706724d71a739", "This is where user can see the Bin Map and Bin Status", "Dashboard", "fa-map", "-", "-", 3, "Bin Dashboard", "-", null, null, "/BinDashboard" },
                    { "BNMP", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2781), "9f7e920b817349cf870706724d71a739", "This is where user can see map the bins", "Bins", "fa-map", "-", "-", 5, "Bin Mapping", "-", null, null, "/BinMapping" },
                    { "DSCD", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2785), "9f7e920b817349cf870706724d71a739", "This is where user can view the Dispatch Schedule", "Schedules", "fa-box", "-", "-", 7, "Dispatch Schedule", "-", null, null, "/DispatchSchedule" },
                    { "ILPR", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2794), "9f7e920b817349cf870706724d71a739", "This is where user can print irradiation labels", "Forms and Reports", "fa-file-lines", "-", "-", 11, "Irradiation Label Printing", "-", null, null, "/IrradiationLabelPrinting" },
                    { "INVT", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2800), "9f7e920b817349cf870706724d71a739", "This is where user can execute inventory transfer", "Inventory Transfer", "fa-dolly", "-", "-", 14, "Inventory Transfer", "-", null, null, "/InventoryTransfer" },
                    { "ISCD", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2787), "9f7e920b817349cf870706724d71a739", "This is where user can view the Irradiation Schedule", "Schedules", "fa-shield", "-", "-", 8, "Irradiation Schedule", "-", null, null, "/IrradiationSchedule" },
                    { "PLPR", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2796), "9f7e920b817349cf870706724d71a739", "This is where user can print pallet labels", "Forms and Reports", "fa-file-lines", "-", "-", 12, "Pallet Label Printing", "-", null, null, "/PalletLabelPrinting" },
                    { "QCMT", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2776), "9f7e920b817349cf870706724d71a739", "This is where user can create Inspection Plans for specific items", "QC Maintenance", "fa-magnifying-glass", "-", "-", 4, "QC Maintenance", "-", null, null, "/QCMaintenance" },
                    { "QCOR", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2789), "9f7e920b817349cf870706724d71a739", "This is where user can input QC Order details", "QC Order", "fa-user-check", "-", "-", 9, "QC Order", "-", null, null, "/QCOrder" },
                    { "RCVN", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2798), "9f7e920b817349cf870706724d71a739", "This is where user can receive sales orders", "Receiving", "fa-truck-ramp-box", "-", "-", 13, "Receiving", "-", null, null, "/Receiving" },
                    { "RSCD", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2783), "9f7e920b817349cf870706724d71a739", "This is where user can view the Receiving Schedule", "Schedules", "fa-truck", "-", "-", 6, "Receiving Schedule", "-", null, null, "/ReceivingSchedule" },
                    { "USRA", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2770), "9f7e920b817349cf870706724d71a739", "This is where you can create, update and delete user authorization.", "Administration", "fa-unlock", "fa-user-shield", "fa-users", 1, "User Authorization", "User Management", null, null, "/USRA" },
                    { "USRG", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2772), "9f7e920b817349cf870706724d71a739", "This is where you can create, update and delete user group.", "Administration", "fa-list-ol", "fa-user-shield", "fa-users", 2, "User Group", "User Management", null, null, "/USRG" },
                    { "USRL", false, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2767), "9f7e920b817349cf870706724d71a739", "This is where you can create, update and delete user information.", "Administration", "fa-id-card", "fa-user-shield", "fa-users", 0, "User Login Info", "User Management", null, null, "/USRL" }
                });

            migrationBuilder.InsertData(
                table: "USRG",
                columns: new[] { "UserGroupId", "CreatedDate", "CreatedUserId", "GroupName", "IsActive", "UpdatedDate", "UpdatedUserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2570), "9f7e920b817349cf870706724d71a739", "Unset", true, null, null },
                    { 2, new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2572), "9f7e920b817349cf870706724d71a739", "Administrator", true, null, null }
                });

            migrationBuilder.InsertData(
                table: "USRL",
                columns: new[] { "UserId", "CreatedDate", "CreatedUserId", "FailedAttempt", "FailedAttemptCount", "IsActive", "IsLocked", "IsLockedoutEnabled", "IsSuperUser", "IsTwoFactorEnabled", "LastLogin", "LastPassword", "LastPasswordSet", "Password", "ResetAttempt", "UpdatedDate", "UpdatedUserId", "UserGroupId", "Username" },
                values: new object[] { "9f7e920b817349cf870706724d71a739", new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2444), "9f7e920b817349cf870706724d71a739", null, 0, true, true, true, true, true, null, null, null, "Uxz7RIWltZj6xFSwsSx775FGJK6qcRCCA0z+dt8CcyNxsmS7N7Rmp0bn0S0mxjiS", null, null, null, 2, "admin" });

            migrationBuilder.InsertData(
                table: "USRD",
                columns: new[] { "Id", "Background", "Company", "CreatedDate", "CreatedUserId", "Department", "DisplayPicture", "Email", "FirstName", "IsActive", "IsEmailConfirmed", "IsPhoneConfirmed", "LastName", "MiddleName", "Phone", "UpdatedDate", "UpdatedUserId", "UserId" },
                values: new object[] { 1, null, "Direc Business Technologies Inc", new DateTime(2023, 10, 30, 13, 59, 39, 497, DateTimeKind.Utc).AddTicks(2453), "9f7e920b817349cf870706724d71a739", "Administration", null, "admin@gmail.com", "Direc", true, false, false, "Admin", "", null, null, null, "9f7e920b817349cf870706724d71a739" });

            migrationBuilder.CreateIndex(
                name: "IX_BPM1_WarehouseCode_Shelf",
                table: "BPM1",
                columns: new[] { "WarehouseCode", "Shelf" });

            migrationBuilder.CreateIndex(
                name: "IX_CIP1_InspectionPlanCode_Version",
                table: "CIP1",
                columns: new[] { "InspectionPlanCode", "Version" });

            migrationBuilder.CreateIndex(
                name: "IX_COR1_QCOrderNo",
                table: "COR1",
                column: "QCOrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COR2_SampleId",
                table: "COR2",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_COR3_SampleId",
                table: "COR3",
                column: "SampleId");

            migrationBuilder.CreateIndex(
                name: "IX_COR4_QCOrderNo",
                table: "COR4",
                column: "QCOrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USRD_UserId",
                table: "USRD",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USRL_UserGroupId",
                table: "USRL",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_USRM_ModuleId",
                table: "USRM",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_USRM_UserGroupId",
                table: "USRM",
                column: "UserGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the sequence
            migrationBuilder.Sql("DROP SEQUENCE MySequence");

            migrationBuilder.Sql("DROP SEQUENCE QCOrderSequence");

            migrationBuilder.DropTable(
                name: "BPM1");

            migrationBuilder.DropTable(
                name: "CIP1");

            migrationBuilder.DropTable(
                name: "COR3");

            migrationBuilder.DropTable(
                name: "COR4");

            migrationBuilder.DropTable(
                name: "OBAS");

            migrationBuilder.DropTable(
                name: "USRD");

            migrationBuilder.DropTable(
                name: "USRM");

            migrationBuilder.DropTable(
                name: "OBMP");

            migrationBuilder.DropTable(
                name: "QCIP");

            migrationBuilder.DropTable(
                name: "COR2");

            migrationBuilder.DropTable(
                name: "USRL");

            migrationBuilder.DropTable(
                name: "MODL");

            migrationBuilder.DropTable(
                name: "COR1");

            migrationBuilder.DropTable(
                name: "USRG");

            migrationBuilder.DropTable(
                name: "QCOR");
        }
    }
}

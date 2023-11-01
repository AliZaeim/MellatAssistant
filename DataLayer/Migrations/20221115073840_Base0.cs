using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Base0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminHelpInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Readers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminHelpInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminSliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminSpecialOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminSpecialOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogGroups",
                columns: table => new
                {
                    BlogGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogGroupTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogGroupEnTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogGroupIcon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShowinMenu = table.Column<bool>(type: "bit", nullable: false),
                    TitleinMenu = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogGroups", x => x.BlogGroupId);
                });

            migrationBuilder.CreateTable(
                name: "BuildingUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UsageRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyCarGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseRate = table.Column<int>(type: "int", nullable: true),
                    IncreasePeriod = table.Column<int>(type: "int", nullable: true),
                    IncreaseCoefficient = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyCarGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyCovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyCovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyCovers_CarBodyCovers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CarBodyCovers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarBodyInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuggestionFormImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasInstallmentRequest = table.Column<bool>(type: "bit", nullable: false),
                    PayrollDeductionImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AttributedLetterImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarCardFrontImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarCardBackImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DrivingPermitFrontImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DrivingPermitBackImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuranceHistoryStatus = table.Column<int>(type: "int", nullable: true),
                    PreviousInsImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasNoneDamageDiscount = table.Column<bool>(type: "bit", nullable: false),
                    NoDamageCertificateImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsChangedHealthOfCar = table.Column<bool>(type: "bit", nullable: false),
                    RecievedDamageLastYear = table.Column<bool>(type: "bit", nullable: false),
                    CarFrontImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarBehindImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverSideImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprenticeSideImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Angle1Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Angle2Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Angle3Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Angle4Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HoodImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrunkImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RoofImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DashboardFullViewImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TapeRecorderImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KilometersImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FrontWindShieldImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RearWindowImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverGlassImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprenticeGlassImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverRearGlassImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApprenticeRearGlassImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SunRoofGlassImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EngineFullViewImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EngineLicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChassisEngravingImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RimsandTires1Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RimsandTires2Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RimsandTires3Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RimsandTires4Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsideBandsImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioSystemFromTrunkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Corrison1Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison2Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison3Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison4Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison5Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison6Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison7Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison8Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison9Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Corrison10Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OuterBodyFilm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarInteriorFilm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EngineSpaceFilm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Premium = table.Column<int>(type: "int", nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MobileImagesTraceCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyInsuranceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<float>(type: "real", nullable: false),
                    HasRecords = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyInsuranceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyInsurerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyInsurerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyLegalDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyLegalDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionCommissionBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodMounth = table.Column<int>(type: "int", nullable: false),
                    PeriodYear = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionCommissionBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Addresses = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cellphones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Emailes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Faxes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelegramLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FacebookLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Whatsapp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderNC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenderFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecepiesInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Readers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    GetReply = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Conversations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoWorkers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialDamages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DamageCount = table.Column<int>(type: "int", nullable: false),
                    DamagePercent = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDamages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPremiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialCoverage = table.Column<int>(type: "int", nullable: false),
                    Premium = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPremiums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsEndofProcess = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemic = table.Column<bool>(type: "bit", nullable: false),
                    GetAmount = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireBaseInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StolenRate = table.Column<float>(type: "real", nullable: false),
                    GlassRate = table.Column<float>(type: "real", nullable: false),
                    PressureTankRate = table.Column<float>(type: "real", nullable: false),
                    CashDiscount = table.Column<float>(type: "real", nullable: false),
                    FestivalDiscount = table.Column<float>(type: "real", nullable: false),
                    Vat = table.Column<float>(type: "real", nullable: false),
                    NoDamageDiscount = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireBaseInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireInsCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HasCoverageLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsCoverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuggestionFormPage1Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuggestionFormPage2Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasInstallmentRequest = table.Column<bool>(type: "bit", nullable: false),
                    PayrollDeductionImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AttributedLetterImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsuranceType = table.Column<int>(type: "int", nullable: true),
                    HasTheftCover = table.Column<bool>(type: "bit", nullable: false),
                    PropertiesFile = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExteriorofBuildingImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuranceLocationInputImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MainMeterandElectricalPanelImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuredPlaceFuseandMeterImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuredPlaceMeterandGasBranchesImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GasBurningDevice1Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GasBurningDevice2Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GasBurningDevice3Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GasBurningDevice4Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WholeInteriorFilm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuranceHistoryStatus = table.Column<int>(type: "int", nullable: true),
                    PerviousInsImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasNoDamagedDiscount = table.Column<bool>(type: "bit", nullable: false),
                    NoDamageCertificateImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuredHealthChanged = table.Column<bool>(type: "bit", nullable: false),
                    SufferDamageLastYear = table.Column<bool>(type: "bit", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Premium = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireInsurerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsurerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireLegalDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireLegalDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FireStructureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    OverChargePercent = table.Column<decimal>(type: "decimal(4,1)", nullable: true),
                    HasCoverageLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireStructureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncidentCovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LifeEventCoverage = table.Column<long>(type: "bigint", nullable: false),
                    DriverEventCoverage = table.Column<long>(type: "bigint", nullable: false),
                    DriverEventPremium = table.Column<long>(type: "bigint", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidentCovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsEndofProcess = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsSystemic = table.Column<bool>(type: "bit", nullable: false),
                    GetInsNo = table.Column<bool>(type: "bit", nullable: false),
                    GetPeyment = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instagrams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instagrams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsurerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<float>(type: "real", nullable: false),
                    RemoveTheYearLimit = table.Column<bool>(type: "bit", nullable: false),
                    RemovePickupLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Percent = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiabilityInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsurerStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttributedLetterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<int>(type: "int", nullable: true),
                    BuildingManagerNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SuggestionFormPage1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SuggestionFormPage2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasPreviousYearInsurance = table.Column<bool>(type: "bit", nullable: false),
                    PreviousInsuranceImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasNoDamageHistory = table.Column<bool>(type: "bit", nullable: false),
                    NoDamageHistoryImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoosDriverDamages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DamageCount = table.Column<int>(type: "int", nullable: false),
                    DamagePercent = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoosDriverDamages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoosLifeDamages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DamageCount = table.Column<int>(type: "int", nullable: false),
                    DamagePercent = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoosLifeDamages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberofInstallments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ThirdPartyPercent = table.Column<float>(type: "real", nullable: false),
                    CarBodyPercent = table.Column<float>(type: "real", nullable: false),
                    FirePercent = table.Column<float>(type: "real", nullable: false),
                    LifePercent = table.Column<float>(type: "real", nullable: false),
                    LiabilityPercent = table.Column<float>(type: "real", nullable: false),
                    TravelPercent = table.Column<float>(type: "real", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Freight = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "ThirdParties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SuggestionFormImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PrevInsPolicyImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarCardFrontImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarCardBackImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DrivingPermitFrontImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DrivingPermitBackImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LicensePlateChanged = table.Column<bool>(type: "bit", nullable: false),
                    CarGreenPaperImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ExistPrevInsurancePolicy = table.Column<bool>(type: "bit", nullable: false),
                    PrevInsurancePolicyImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HasInstallmentRequest = table.Column<bool>(type: "bit", nullable: false),
                    PayrollDeductionImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AttributedLetterImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VehicleOperationKilometers = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Premium = table.Column<int>(type: "int", nullable: true),
                    ExtraFinanceDisount = table.Column<float>(type: "real", nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyBaseDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverAccidentPremium = table.Column<int>(type: "int", nullable: false),
                    LegalDiscounts = table.Column<float>(type: "real", nullable: false),
                    LegalDiscountPermit = table.Column<bool>(type: "bit", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(4,1)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyBaseDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelInsClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelInsClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelInsCos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelInsCos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelZooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelZooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    File = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleConstructionYearLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VahicleGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleConstructionYear = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleConstructionYearLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DelayedPenalty = table.Column<int>(type: "int", nullable: true),
                    ImmunityStartDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ImmunityEndDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FinancialPremium = table.Column<int>(type: "int", nullable: true),
                    GroupPremium = table.Column<int>(type: "int", nullable: true),
                    VehicleConstructionYearLimit = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleGroups_VehicleGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "VehicleGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleGroupId = table.Column<int>(type: "int", nullable: true),
                    Usage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleUsages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteUpdates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Readers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteUpdates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkWiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaveMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWiths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BlogDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlogImageInBlog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlogImageInBlogDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlogText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPageTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogPageDescription = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    BlogSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlogTags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    HasVideo = table.Column<bool>(type: "bit", nullable: false),
                    HasVoice = table.Column<bool>(type: "bit", nullable: false),
                    BlogIsActive = table.Column<bool>(type: "bit", nullable: false),
                    BlogGroupId = table.Column<int>(type: "int", nullable: false),
                    BlogUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BlogRefferalLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlogLinkText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlogViewsCount = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogGroups_BlogGroupId",
                        column: x => x.BlogGroupId,
                        principalTable: "BlogGroups",
                        principalColumn: "BlogGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CarBodyCarGroupId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ConsYear = table.Column<int>(type: "int", nullable: false),
                    BasePremium = table.Column<int>(type: "int", nullable: true),
                    Second2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Third2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Fourth2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Fifth2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Sixth2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Seventh2YearsPremium = table.Column<int>(type: "int", nullable: true),
                    Eighth2YearsPremium = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyCars_CarBodyCarGroups_CarBodyCarGroupId",
                        column: x => x.CarBodyCarGroupId,
                        principalTable: "CarBodyCarGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarBodySupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarBodyInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodySupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodySupplements_CarBodyInsurances_CarBodyInsuranceId",
                        column: x => x.CarBodyInsuranceId,
                        principalTable: "CarBodyInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarBodyGroupUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBodyGroupId = table.Column<int>(type: "int", nullable: false),
                    CarBodyUsageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyGroupUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyGroupUsages_CarBodyCarGroups_CarBodyGroupId",
                        column: x => x.CarBodyGroupId,
                        principalTable: "CarBodyCarGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarBodyGroupUsages_CarBodyUsages_CarBodyUsageId",
                        column: x => x.CarBodyUsageId,
                        principalTable: "CarBodyUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionCommissionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuredName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarketerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommitmentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CommitmentValue = table.Column<int>(type: "int", nullable: false),
                    CommissionValue = table.Column<int>(type: "int", nullable: false),
                    CommitmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommitmentDoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CollectionCommissionBaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionCommissionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionCommissionDetails_CollectionCommissionBases_CollectionCommissionBaseId",
                        column: x => x.CollectionCommissionBaseId,
                        principalTable: "CollectionCommissionBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyInsuranceFinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    CarBodyInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyInsuranceFinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyInsuranceFinancialStatuses_CarBodyInsurances_CarBodyInsuranceId",
                        column: x => x.CarBodyInsuranceId,
                        principalTable: "CarBodyInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarBodyInsuranceFinancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingUsageFireCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingUsageId = table.Column<int>(type: "int", nullable: false),
                    FireInsCoverageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingUsageFireCoverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingUsageFireCoverages_BuildingUsages_BuildingUsageId",
                        column: x => x.BuildingUsageId,
                        principalTable: "BuildingUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingUsageFireCoverages_FireInsCoverages_FireInsCoverageId",
                        column: x => x.FireInsCoverageId,
                        principalTable: "FireInsCoverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FireInsuranceFinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    FireInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsuranceFinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireInsuranceFinancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FireInsuranceFinancialStatuses_FireInsurances_FireInsuranceId",
                        column: x => x.FireInsuranceId,
                        principalTable: "FireInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FireInsuranceSupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FireInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsuranceSupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireInsuranceSupplements_FireInsurances_FireInsuranceId",
                        column: x => x.FireInsuranceId,
                        principalTable: "FireInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarBodyInsuranceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    CarBodyInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyInsuranceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyInsuranceStatuses_CarBodyInsurances_CarBodyInsuranceId",
                        column: x => x.CarBodyInsuranceId,
                        principalTable: "CarBodyInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarBodyInsuranceStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FireInsuranceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    FireInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsuranceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireInsuranceStatuses_FireInsurances_FireInsuranceId",
                        column: x => x.FireInsuranceId,
                        principalTable: "FireInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FireInsuranceStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiabilityFinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    LiabilityInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityFinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilityFinancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiabilityFinancialStatuses_LiabilityInsurances_LiabilityInsuranceId",
                        column: x => x.LiabilityInsuranceId,
                        principalTable: "LiabilityInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiabilityStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    LiabilityInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilityStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiabilityStatuses_LiabilityInsurances_LiabilityInsuranceId",
                        column: x => x.LiabilityInsuranceId,
                        principalTable: "LiabilityInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiabilitySupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LiabilityInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilitySupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilitySupplements_LiabilityInsurances_LiabilityInsuranceId",
                        column: x => x.LiabilityInsuranceId,
                        principalTable: "LiabilityInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LifeInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsuredName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsuredFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    InsuredNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuestionnairePage1Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuestionnairePage2Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuestionnairePage3Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuestionnairePage4Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeInsurances_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LifeInsurances_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanPaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPaymentMethods_PaymentMethods_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanPaymentMethods_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RPId = table.Column<int>(name: "RP_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RPId);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Freight = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                    table.ForeignKey(
                        name: "FK_Counties_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyFainancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    ThirdPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyFainancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThirdPartyFainancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThirdPartyFainancialStatuses_ThirdParties_ThirdPartyId",
                        column: x => x.ThirdPartyId,
                        principalTable: "ThirdParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    ThirdPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThirdPartyStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThirdPartyStatuses_ThirdParties_ThirdPartyId",
                        column: x => x.ThirdPartyId,
                        principalTable: "ThirdParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartySupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThirdPartyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartySupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThirdPartySupplements_ThirdParties_ThirdPartyId",
                        column: x => x.ThirdPartyId,
                        principalTable: "ThirdParties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TravelClassZooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ZoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelClassZooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelClassZooms_TravelInsClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "TravelInsClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelClassZooms_TravelZooms_ZoomId",
                        column: x => x.ZoomId,
                        principalTable: "TravelZooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelInsurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SellerRoleId = table.Column<int>(type: "int", nullable: true),
                    CommissionPercent = table.Column<float>(type: "real", nullable: true),
                    InsurerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InsurerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsurerNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsurerStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AttributedLetterImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuredName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuredFamily = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InsuredAge = table.Column<int>(type: "int", nullable: true),
                    InsuredNCImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsuredPassportImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SuggestionFormImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InsCo = table.Column<int>(type: "int", maxLength: 100, nullable: true),
                    InsClass = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    HasCrona = table.Column<bool>(type: "bit", nullable: true),
                    TravelZoom = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    TravelPeriod = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    TraceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IssuedInsNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsFinancailStateId = table.Column<int>(type: "int", nullable: true),
                    LastInsStateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastInsFinancialStateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelInsurances_TravelZooms_TravelZoom",
                        column: x => x.TravelZoom,
                        principalTable: "TravelZooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BasicThirdPartyPremiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VGId = table.Column<int>(type: "int", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Premium = table.Column<int>(type: "int", nullable: false),
                    DelayPenalty = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicThirdPartyPremiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicThirdPartyPremiums_VehicleGroups_VGId",
                        column: x => x.VGId,
                        principalTable: "VehicleGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VehicleGroupUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    UsageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleGroupUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleGroupUsages_VehicleGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "VehicleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleGroupUsages_VehicleUsages_UsageId",
                        column: x => x.UsageId,
                        principalTable: "VehicleUsages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FireInsStateRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    BuildingUsageFireCoverageId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsStateRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireInsStateRates_BuildingUsageFireCoverages_BuildingUsageFireCoverageId",
                        column: x => x.BuildingUsageFireCoverageId,
                        principalTable: "BuildingUsageFireCoverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FireInsStateRates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarBodyStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarBodyStatusId = table.Column<int>(type: "int", nullable: true),
                    CarBodyFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyStatusComments_CarBodyInsuranceFinancialStatuses_CarBodyFinancialStatusId",
                        column: x => x.CarBodyFinancialStatusId,
                        principalTable: "CarBodyInsuranceFinancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarBodyStatusComments_CarBodyInsuranceStatuses_CarBodyStatusId",
                        column: x => x.CarBodyStatusId,
                        principalTable: "CarBodyInsuranceStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FireInsuranceStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireInsuranceStatusId = table.Column<int>(type: "int", nullable: true),
                    FireInsuranceFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FireInsuranceStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FireInsuranceStatusComments_FireInsuranceFinancialStatuses_FireInsuranceFinancialStatusId",
                        column: x => x.FireInsuranceFinancialStatusId,
                        principalTable: "FireInsuranceFinancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FireInsuranceStatusComments_FireInsuranceStatuses_FireInsuranceStatusId",
                        column: x => x.FireInsuranceStatusId,
                        principalTable: "FireInsuranceStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LiabilityStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiabilityStatusId = table.Column<int>(type: "int", nullable: true),
                    LiabilityFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiabilityStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiabilityStatusComments_LiabilityFinancialStatuses_LiabilityFinancialStatusId",
                        column: x => x.LiabilityFinancialStatusId,
                        principalTable: "LiabilityFinancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiabilityStatusComments_LiabilityStatuses_LiabilityStatusId",
                        column: x => x.LiabilityStatusId,
                        principalTable: "LiabilityStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LifeInsuranceFinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    LifeInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeInsuranceFinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceFinancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceFinancialStatuses_LifeInsurances_LifeInsuranceId",
                        column: x => x.LifeInsuranceId,
                        principalTable: "LifeInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeInsuranceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    LifeInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeInsuranceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceStatuses_LifeInsurances_LifeInsuranceId",
                        column: x => x.LifeInsuranceId,
                        principalTable: "LifeInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeInsuranceSupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LifeInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeInsuranceSupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceSupplements_LifeInsurances_LifeInsuranceId",
                        column: x => x.LifeInsuranceId,
                        principalTable: "LifeInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cooperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LevelOfEducation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TodoComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cooperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cooperations_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ConfirmCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConfirmedCellphone = table.Column<bool>(type: "bit", nullable: false),
                    Father = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserCreditCardNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserBankAccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShebaNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferralCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    InsWokHistory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IssuePlace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NC = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FieldofStudy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LevelofStudy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UniversityName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DemandNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DemandValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SalesExCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PortalPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PortalIsActive = table.Column<bool>(type: "bit", nullable: false),
                    NCImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EducationDegreeImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EndofServiceImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersonalImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SHEBAImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Exam96Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NoAddictionImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CriminalRecord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DemandFrontImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DemandBackImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgentRequestImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SignImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId");
                });

            migrationBuilder.CreateTable(
                name: "ThirdPartyStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdPartyStatusId = table.Column<int>(type: "int", nullable: true),
                    ThirdPartyFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdPartyStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThirdPartyStatusComments_ThirdPartyFainancialStatuses_ThirdPartyFinancialStatusId",
                        column: x => x.ThirdPartyFinancialStatusId,
                        principalTable: "ThirdPartyFainancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThirdPartyStatusComments_ThirdPartyStatuses_ThirdPartyStatusId",
                        column: x => x.ThirdPartyStatusId,
                        principalTable: "ThirdPartyStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TravelFinancialStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false),
                    TravelInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelFinancialStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelFinancialStatuses_FinancialStatuses_FinancialStatusId",
                        column: x => x.FinancialStatusId,
                        principalTable: "FinancialStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelFinancialStatuses_TravelInsurances_TravelInsuranceId",
                        column: x => x.TravelInsuranceId,
                        principalTable: "TravelInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsStatusId = table.Column<int>(type: "int", nullable: false),
                    TravelInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelStatuses_InsStatuses_InsStatusId",
                        column: x => x.InsStatusId,
                        principalTable: "InsStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TravelStatuses_TravelInsurances_TravelInsuranceId",
                        column: x => x.TravelInsuranceId,
                        principalTable: "TravelInsurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelSupplements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TravelInsuranceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelSupplements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelSupplements_TravelInsurances_TravelInsuranceId",
                        column: x => x.TravelInsuranceId,
                        principalTable: "TravelInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LifeInsuranceStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LifeInsuranceStatusId = table.Column<int>(type: "int", nullable: true),
                    LifeInsuranceFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeInsuranceStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeInsuranceStatusComments_LifeInsuranceFinancialStatuses_LifeInsuranceFinancialStatusId",
                        column: x => x.LifeInsuranceFinancialStatusId,
                        principalTable: "LifeInsuranceFinancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LifeInsuranceStatusComments_LifeInsuranceStatuses_LifeInsuranceStatusId",
                        column: x => x.LifeInsuranceStatusId,
                        principalTable: "LifeInsuranceStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    URId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ThirdPartyPercent = table.Column<float>(type: "real", nullable: true),
                    CarBodyPercent = table.Column<float>(type: "real", nullable: true),
                    FirePercent = table.Column<float>(type: "real", nullable: true),
                    LifePercent = table.Column<float>(type: "real", nullable: true),
                    LiabilityPercent = table.Column<float>(type: "real", nullable: true),
                    TravelPercent = table.Column<float>(type: "real", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.URId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelStatusComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelStatusId = table.Column<int>(type: "int", nullable: true),
                    TravelFinancialStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelStatusComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelStatusComments_TravelFinancialStatuses_TravelFinancialStatusId",
                        column: x => x.TravelFinancialStatusId,
                        principalTable: "TravelFinancialStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TravelStatusComments_TravelStatuses_TravelStatusId",
                        column: x => x.TravelStatusId,
                        principalTable: "TravelStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BlogGroups",
                columns: new[] { "BlogGroupId", "BlogGroupEnTitle", "BlogGroupIcon", "BlogGroupTitle", "IsActive", "IsDeleted", "ShowinMenu", "TitleinMenu" },
                values: new object[,]
                {
                    { 10, "insurance-knowledge", "knowledge.svg", "دانستنی ها", true, false, false, null },
                    { 11, "insurance-learning", "learning.svg", "آموزش", true, false, false, null },
                    { 12, "insurance-notices", "info.svg", "اطلاعیه ها", true, false, false, null },
                    { 13, "mellat-insurance", "mellat-insurance.svg", "بیمه ملت", true, false, false, null },
                    { 14, "insurance-industry", "insurance.svg", "صنعت بیمه", true, false, false, null },
                    { 15, "insurance-damage", "damage.svg", "خسارت", true, false, false, null }
                });

            migrationBuilder.InsertData(
                table: "FinancialDamages",
                columns: new[] { "Id", "DamageCount", "DamagePercent", "RegDate" },
                values: new object[,]
                {
                    { 1, 1, 20, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1528) },
                    { 2, 2, 30, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1530) },
                    { 3, 3, 40, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1531) }
                });

            migrationBuilder.InsertData(
                table: "FinancialPremiums",
                columns: new[] { "Id", "FinancialCoverage", "Premium", "RegDate" },
                values: new object[,]
                {
                    { 1, 160000000, 350000, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Local).AddTicks(1617) },
                    { 2, 320000000, 700000, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Local).AddTicks(1629) },
                    { 3, 500000000, 1000000, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Local).AddTicks(1631) }
                });

            migrationBuilder.InsertData(
                table: "FinancialStatuses",
                columns: new[] { "Id", "Amount", "Comment", "GetAmount", "IsDefault", "IsDeleted", "IsEndofProcess", "IsSystemic", "Text" },
                values: new object[,]
                {
                    { 1, null, null, false, false, false, true, true, "پرداخت شده" },
                    { 2, null, null, false, true, false, false, true, "پرداخت نشده" },
                    { 3, null, null, true, false, false, false, true, "کسر پرداخت" }
                });

            migrationBuilder.InsertData(
                table: "IncidentCovers",
                columns: new[] { "Id", "DriverEventCoverage", "DriverEventPremium", "LifeEventCoverage", "RegDate" },
                values: new object[] { 1, 4800000000L, 3360000L, 6400000000L, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1577) });

            migrationBuilder.InsertData(
                table: "InsStatuses",
                columns: new[] { "Id", "Comment", "GetInsNo", "GetPeyment", "IsDefault", "IsDeleted", "IsEndofProcess", "IsSystemic", "Text" },
                values: new object[,]
                {
                    { 1, null, false, false, true, false, false, true, "دریافت شده" },
                    { 2, null, false, false, false, false, false, true, "در دست اقدام" },
                    { 3, null, false, false, false, false, false, true, "مدارک ناقص" },
                    { 4, null, false, false, false, false, false, true, "در حال صدور" },
                    { 5, null, true, false, false, false, true, true, "صادر شده" },
                    { 6, null, false, false, false, false, true, true, "رد شده" },
                    { 7, null, false, true, false, false, false, true, "اعلام حق بیمه" }
                });

            migrationBuilder.InsertData(
                table: "InsurerTypes",
                columns: new[] { "Id", "DiscountPercent", "RemovePickupLimit", "RemoveTheYearLimit", "Title" },
                values: new object[,]
                {
                    { 1, 0f, false, false, "معمولی" },
                    { 2, 0f, false, false, "گروهی" },
                    { 3, 25f, true, true, "صندوق بازنشستگی" },
                    { 4, 25f, true, true, "تامین اجتماعی" },
                    { 5, 0f, true, true, "بیمه گذار زندگی" }
                });

            migrationBuilder.InsertData(
                table: "LegalDiscounts",
                columns: new[] { "Id", "Percent", "RegDate", "State", "Title", "Type" },
                values: new object[,]
                {
                    { 1, 2.5m, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1551), true, "تخفیف قانونی", "dis" },
                    { 2, 10m, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1554), true, "تخفیف گروهی", "dis" },
                    { 3, 10m, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1556), true, "اضافه نرخ حادثه ساز", "add" }
                });

            migrationBuilder.InsertData(
                table: "LoosDriverDamages",
                columns: new[] { "Id", "DamageCount", "DamagePercent", "RegDate" },
                values: new object[,]
                {
                    { 1, 1, 30, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1435) },
                    { 2, 2, 70, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1440) },
                    { 3, 3, 100, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1441) }
                });

            migrationBuilder.InsertData(
                table: "LoosLifeDamages",
                columns: new[] { "Id", "DamageCount", "DamagePercent", "RegDate" },
                values: new object[,]
                {
                    { 1, 1, 30, null },
                    { 2, 2, 70, null },
                    { 3, 3, 100, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 1, null, "management", "مدیریت" },
                    { 220, null, "myregisterdreqs", "درخواستهای من" },
                    { 237, null, "myregisterdinss", "بیمه های من" },
                    { 254, null, "myins", "گزارش فروش" },
                    { 271, null, "comrep", "گزارش کارمزد" },
                    { 274, null, "conversations", "پیامهای داخلی" },
                    { 305, null, "payconf", "تایید پرداخت" },
                    { 306, null, "attachfile", "پیوست فایل" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CarBodyPercent", "FirePercent", "IsDeleted", "LiabilityPercent", "LifePercent", "RoleName", "RoleTitle", "ThirdPartyPercent", "TravelPercent" },
                values: new object[,]
                {
                    { 1, 0f, 0f, false, 0f, 0f, "Admin", "مدیر سایت", 0f, 0f },
                    { 2, 0f, 0f, false, 0f, 0f, "Customer", "مشتری", 0f, 0f },
                    { 3, 5f, 10f, false, 10f, 7f, "Marketer", "همکار فروش", 2f, 10f },
                    { 4, 0f, 0f, false, 0f, 0f, "Operator", "اپراتور", 0f, 0f },
                    { 5, 9f, 15f, false, 15f, 10f, "SalesEx", "کارشناس فروش", 3f, 15f },
                    { 6, 10f, 17f, false, 17f, 20f, "Agent", "نماینده", 4f, 0f },
                    { 7, 0f, 0f, false, 0f, 0f, "Applicant", "متقاضی", 0f, 0f }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "Freight", "IsDeleted", "StateName" },
                values: new object[,]
                {
                    { 1, 200000, false, "آذربایجان شرقی" },
                    { 2, 200000, false, "آذربایجان غربی" },
                    { 3, 200000, false, "اردبیل" },
                    { 4, 200000, false, "اصفهان" },
                    { 5, 200000, false, "البرز" },
                    { 6, 200000, false, "ایلام" },
                    { 7, 200000, false, "بوشهر" },
                    { 8, 200000, false, "تهران" },
                    { 9, 200000, false, "چهار محال و بختیاری" },
                    { 10, 200000, false, "خراسان جنوبی" },
                    { 11, 200000, false, "خراسان رضوی" },
                    { 12, 200000, false, "خراسان شمالی" },
                    { 13, 200000, false, "خوزستان" },
                    { 14, 200000, false, "زنجان" },
                    { 15, 200000, false, "سمنان" },
                    { 16, 200000, false, "سیستان و بلوچستان" },
                    { 17, 200000, false, "فارس" },
                    { 18, 200000, false, "قزوین" },
                    { 19, 200000, false, "قم" },
                    { 20, 200000, false, "کردستان" },
                    { 21, 200000, false, "کرمان" },
                    { 22, 200000, false, "کرمانشاه" },
                    { 23, 200000, false, "کهکیلویه و بویراحمد" },
                    { 24, 200000, false, "گلستان" },
                    { 25, 200000, false, "گیلان" },
                    { 26, 200000, false, "لرستان" },
                    { 27, 200000, false, "مازندران" },
                    { 28, 200000, false, "مرکزی" },
                    { 29, 200000, false, "هرمزگان" },
                    { 30, 200000, false, "همدان" },
                    { 31, 200000, false, "یزد" }
                });

            migrationBuilder.InsertData(
                table: "ThirdPartyBaseDatas",
                columns: new[] { "Id", "DriverAccidentPremium", "LegalDiscountPermit", "LegalDiscounts", "RegDate", "VAT" },
                values: new object[] { 1, 3000000, false, 2.5f, new DateTime(2022, 11, 15, 11, 8, 39, 811, DateTimeKind.Utc).AddTicks(1655), 9m });

            migrationBuilder.InsertData(
                table: "VehicleGroups",
                columns: new[] { "Id", "DelayedPenalty", "FinancialPremium", "GroupPremium", "GroupTitle", "ImmunityEndDate", "ImmunityStartDate", "ParentId", "VehicleConstructionYearLimit" },
                values: new object[,]
                {
                    { 1, null, null, null, "سواری", null, null, null, null },
                    { 2, null, null, null, "کامیون", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "VehicleUsages",
                columns: new[] { "Id", "Rate", "Usage", "VehicleGroupId" },
                values: new object[,]
                {
                    { 1, 4, "شخصی", null },
                    { 2, 3, "آژانس", null },
                    { 3, 2, "آمبولانس", null }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "Freight", "IsDeleted", "StateId" },
                values: new object[,]
                {
                    { 1, "کشکسرای", 200000, false, 1 },
                    { 2, "سهند", 200000, false, 1 },
                    { 3, "سیس", 200000, false, 1 },
                    { 4, "دوزدوزان", 200000, false, 1 },
                    { 5, "تیمورلو", 200000, false, 1 },
                    { 6, "صوفیان", 200000, false, 1 },
                    { 7, "سردرود", 200000, false, 1 },
                    { 8, "هادیشهر", 200000, false, 1 },
                    { 9, "هشترود", 200000, false, 1 },
                    { 10, "زرنق", 200000, false, 1 },
                    { 11, "ترکمانچای", 200000, false, 1 },
                    { 12, "ورزقان", 200000, false, 1 },
                    { 13, "تسوج", 200000, false, 1 },
                    { 14, "زنوز", 200000, false, 1 },
                    { 15, "ایخچی", 200000, false, 1 },
                    { 16, "شزفخانه", 200000, false, 1 },
                    { 17, "مهربان", 200000, false, 1 },
                    { 18, "مبارک شهر", 200000, false, 1 },
                    { 19, "تیکمه داش", 200000, false, 1 },
                    { 20, "باسمنج", 200000, false, 1 },
                    { 21, "سیه رود", 200000, false, 1 },
                    { 22, "میانه", 200000, false, 1 },
                    { 23, "خمارلو", 200000, false, 1 },
                    { 24, "خواجه", 200000, false, 1 },
                    { 25, "بناب مرند", 200000, false, 1 },
                    { 26, "قره آغاج", 200000, false, 1 },
                    { 27, "وایقان", 200000, false, 1 },
                    { 28, "مراغه", 200000, false, 1 },
                    { 29, "ممقان", 200000, false, 1 },
                    { 30, "خامنه", 200000, false, 1 },
                    { 31, "خسروشاه", 200000, false, 1 },
                    { 32, "لیلان", 200000, false, 1 },
                    { 33, "نظرکهریزی", 200000, false, 1 },
                    { 34, "اهر", 200000, false, 1 },
                    { 35, "بخشایش", 200000, false, 1 },
                    { 36, "آقکند", 200000, false, 1 },
                    { 37, "جوان قلعه", 200000, false, 1 },
                    { 38, "کلیبر", 200000, false, 1 },
                    { 39, "مرند", 200000, false, 1 },
                    { 40, "اسکو", 200000, false, 1 },
                    { 41, "شندآباد", 200000, false, 1 },
                    { 42, "شربیان", 200000, false, 1 },
                    { 43, "گوگان", 200000, false, 1 },
                    { 44, "بستان آباد", 200000, false, 1 },
                    { 45, "تبریز", 200000, false, 1 },
                    { 46, "جلفا", 200000, false, 1 },
                    { 47, "آچاچی", 200000, false, 1 },
                    { 48, "هریس", 200000, false, 1 },
                    { 49, "یامچی", 200000, false, 1 },
                    { 50, "خاروانا", 200000, false, 1 },
                    { 51, "کوزه کنان", 200000, false, 1 },
                    { 52, "خداجو", 200000, false, 1 },
                    { 53, "آذرشهر", 200000, false, 1 },
                    { 54, "شبستر", 200000, false, 1 },
                    { 55, "سراب", 200000, false, 1 },
                    { 56, "ملکان", 200000, false, 1 },
                    { 57, "بناب", 200000, false, 1 },
                    { 58, "هوراند", 200000, false, 1 },
                    { 59, "کلوانق", 200000, false, 1 },
                    { 60, "ترک", 200000, false, 1 },
                    { 61, "عجب شیر", 200000, false, 1 },
                    { 62, "آبش احمد", 200000, false, 1 },
                    { 63, "تازه شهر", 200000, false, 2 },
                    { 64, "نالوس", 200000, false, 2 },
                    { 65, "ایواوغلی", 200000, false, 2 },
                    { 66, "شاهین دژ", 200000, false, 2 },
                    { 67, "گردکشانه", 200000, false, 2 },
                    { 68, "باروق", 200000, false, 2 },
                    { 69, "سیلوانه", 200000, false, 2 },
                    { 70, "بازرگان", 200000, false, 2 },
                    { 71, "نازک علیا", 200000, false, 2 },
                    { 72, "ربط", 200000, false, 2 },
                    { 73, "تکاب", 200000, false, 2 },
                    { 74, "دیزج دیز", 200000, false, 2 },
                    { 75, "سیمینه", 200000, false, 2 },
                    { 76, "نوشین", 200000, false, 2 },
                    { 77, "میاندوآب", 200000, false, 2 },
                    { 78, "مرگنلر", 200000, false, 2 },
                    { 79, "سلماس", 200000, false, 2 },
                    { 80, "آواجیق", 200000, false, 2 },
                    { 81, "قطور", 200000, false, 2 },
                    { 82, "محمودآباد", 200000, false, 2 },
                    { 83, "خوی", 200000, false, 2 },
                    { 84, "نقده", 200000, false, 2 },
                    { 85, "سرو", 200000, false, 2 },
                    { 86, "خلیفان", 200000, false, 2 },
                    { 87, "پلدشت", 200000, false, 2 },
                    { 88, "میرآباد", 200000, false, 2 },
                    { 89, "اشنویه", 200000, false, 2 },
                    { 90, "زرآباد", 200000, false, 2 },
                    { 91, "بوکان", 200000, false, 2 },
                    { 92, "پیرانشهر", 200000, false, 2 },
                    { 93, "چهاربرج", 200000, false, 2 },
                    { 94, "قوشچی", 200000, false, 2 },
                    { 95, "شوط", 200000, false, 2 },
                    { 96, "ماکو", 200000, false, 2 },
                    { 97, "سیه چشمه", 200000, false, 2 },
                    { 98, "سردشت", 200000, false, 2 },
                    { 99, "کشاورز", 200000, false, 2 },
                    { 100, "فیرورق", 200000, false, 2 },
                    { 101, "محمدیار", 200000, false, 2 },
                    { 102, "ارومیه", 200000, false, 2 },
                    { 103, "مهاباد", 200000, false, 2 },
                    { 104, "قره ضیاءالدین", 200000, false, 2 },
                    { 105, "پارس آباد", 200000, false, 3 },
                    { 106, "فخر آباد", 200000, false, 3 },
                    { 107, "کلور", 200000, false, 3 },
                    { 108, "نیر", 200000, false, 3 },
                    { 109, "اردبیل", 200000, false, 3 },
                    { 110, "اسلام آباد", 200000, false, 3 },
                    { 111, "تازه کندانگوت", 200000, false, 3 },
                    { 112, "مشگین شهر", 200000, false, 3 },
                    { 113, "جعفر آباد", 200000, false, 3 },
                    { 114, "نمین", 200000, false, 3 },
                    { 115, "اصلاندوز", 200000, false, 3 },
                    { 116, "مرادلو", 200000, false, 3 },
                    { 117, "خلخال", 200000, false, 3 },
                    { 118, "کوراییم", 200000, false, 3 },
                    { 119, "هیر", 200000, false, 3 },
                    { 120, "گیوی", 200000, false, 3 },
                    { 121, "گرمی", 200000, false, 3 },
                    { 122, "لاهرود", 200000, false, 3 },
                    { 123, "هشتجین", 200000, false, 3 },
                    { 124, "عنبران", 200000, false, 3 },
                    { 125, "تازه کند", 200000, false, 3 },
                    { 126, "قصابه", 200000, false, 3 },
                    { 127, "رضی", 200000, false, 3 },
                    { 128, "سرعین", 200000, false, 3 },
                    { 129, "بیله سوار", 200000, false, 3 },
                    { 130, "آبی بیگلو", 200000, false, 3 },
                    { 131, "آبی بیگلو", 200000, false, 4 },
                    { 132, "زیار", 200000, false, 4 },
                    { 133, "زرین شهر", 200000, false, 4 },
                    { 134, "گلشن", 200000, false, 4 },
                    { 135, "پیربکران", 200000, false, 4 },
                    { 136, "خالدآباد", 200000, false, 4 },
                    { 137, "سجزی", 200000, false, 4 },
                    { 138, "گوگد", 200000, false, 4 },
                    { 139, "تیران", 200000, false, 4 },
                    { 140, "ونک", 200000, false, 4 },
                    { 141, "دهق", 200000, false, 4 },
                    { 142, "زواره", 200000, false, 4 },
                    { 143, "کاشان", 200000, false, 4 },
                    { 144, "ابوزید آباد", 200000, false, 4 },
                    { 145, "اصغر آباد", 200000, false, 4 },
                    { 146, "بافران", 200000, false, 4 },
                    { 147, "شهرضا", 200000, false, 4 },
                    { 148, "خور", 200000, false, 4 },
                    { 149, "مجلسی", 200000, false, 4 },
                    { 150, "هرند", 200000, false, 4 },
                    { 151, "فولادشهر", 200000, false, 4 },
                    { 152, "کمشچه", 200000, false, 4 },
                    { 153, "کلیشادوسودرجان", 200000, false, 4 },
                    { 154, "لای بید", 200000, false, 4 },
                    { 155, "قهجاورستان", 200000, false, 4 },
                    { 156, "چرمین", 200000, false, 4 },
                    { 157, "رزوه", 200000, false, 4 },
                    { 158, "فریدونشهر", 200000, false, 4 },
                    { 159, "طرق آباد", 200000, false, 4 },
                    { 160, "نصر آباد", 200000, false, 4 },
                    { 161, "برزک", 200000, false, 4 },
                    { 162, "سفید شهر", 200000, false, 4 },
                    { 163, "سمیرم", 200000, false, 4 },
                    { 164, "گلدشت", 200000, false, 4 },
                    { 165, "اردستان", 200000, false, 4 },
                    { 166, "جوشقان قالی", 200000, false, 4 },
                    { 167, "بویین و میاندشت", 200000, false, 4 },
                    { 168, "کرکوند", 200000, false, 4 },
                    { 169, "درچه", 200000, false, 4 },
                    { 170, "انارک", 200000, false, 4 },
                    { 171, "دولت آباد", 200000, false, 4 },
                    { 172, "ایمانشهر", 200000, false, 4 },
                    { 173, "گرگاب", 200000, false, 4 },
                    { 174, "حسن آباد", 200000, false, 4 },
                    { 175, "سده لنجان", 200000, false, 4 },
                    { 176, "حبیب آباد", 200000, false, 4 },
                    { 177, "بهاران", 200000, false, 4 },
                    { 178, "میمه", 200000, false, 4 },
                    { 179, "تودشک", 200000, false, 4 },
                    { 180, "گلشهر", 200000, false, 4 },
                    { 181, "رضوانشهر", 200000, false, 4 },
                    { 182, "داران", 200000, false, 4 },
                    { 183, "علویجه", 200000, false, 4 },
                    { 184, "نیک آباد", 200000, false, 4 },
                    { 185, "مشکات", 200000, false, 4 },
                    { 186, "آران و بیدگل", 200000, false, 4 },
                    { 187, "خوانسار", 200000, false, 4 },
                    { 188, "نجف آباد", 200000, false, 4 },
                    { 189, "منظریه", 200000, false, 4 },
                    { 190, "فرخی", 200000, false, 4 },
                    { 191, "دیزیچه", 200000, false, 4 },
                    { 192, "اژیه", 200000, false, 4 },
                    { 193, "زاینده رود", 200000, false, 4 },
                    { 194, "خورزوق", 200000, false, 4 },
                    { 195, "قهدریجان", 200000, false, 4 },
                    { 196, "شاهین شهر", 200000, false, 4 },
                    { 197, "بهارستان", 200000, false, 4 },
                    { 198, "چمگردان", 200000, false, 4 },
                    { 199, "دهاقان", 200000, false, 4 },
                    { 200, "برف انبار", 200000, false, 4 },
                    { 201, "بادرود", 200000, false, 4 },
                    { 202, "کوهپایه", 200000, false, 4 },
                    { 203, "گلپایگان", 200000, false, 4 },
                    { 204, "عسگران", 200000, false, 4 },
                    { 205, "حنا", 200000, false, 4 },
                    { 206, "کهریزسنگ", 200000, false, 4 },
                    { 207, "مهاباد", 200000, false, 4 },
                    { 208, "کامو و چوگان", 200000, false, 4 },
                    { 209, "افوس", 200000, false, 4 },
                    { 300, "زیباشهر", 200000, false, 4 },
                    { 301, "کوشک", 200000, false, 4 },
                    { 302, "نایین", 200000, false, 4 },
                    { 303, "سین", 200000, false, 4 },
                    { 304, "زازران", 200000, false, 4 },
                    { 305, "مبارکه", 200000, false, 4 },
                    { 306, "ورزنه", 200000, false, 4 },
                    { 307, "ورنامخواست", 200000, false, 4 },
                    { 308, "شاپور آباد", 200000, false, 4 },
                    { 309, "فلاورجان", 200000, false, 4 },
                    { 310, "وزوان", 200000, false, 4 },
                    { 311, "اصفهان", 200000, false, 4 },
                    { 312, "باغ بهادران", 200000, false, 4 },
                    { 313, "چادگان", 200000, false, 4 },
                    { 314, "دامنه", 200000, false, 4 },
                    { 315, "نطنز", 200000, false, 4 },
                    { 316, "محمد آباد", 200000, false, 4 },
                    { 317, "نیاسر", 200000, false, 4 },
                    { 318, "نوش آباد", 200000, false, 4 },
                    { 319, "کمه", 200000, false, 4 },
                    { 320, "جوزدان", 200000, false, 4 },
                    { 321, "قمصر", 200000, false, 4 },
                    { 322, "جندق", 200000, false, 4 },
                    { 323, "طالخونچه", 200000, false, 4 },
                    { 324, "خمینی شهر", 200000, false, 4 },
                    { 325, "باغشاد", 200000, false, 4 },
                    { 326, "دستگرد", 200000, false, 4 },
                    { 327, "ابریشم", 200000, false, 4 },
                    { 328, "چهارباغ", 200000, false, 5 },
                    { 329, "آسارا", 200000, false, 5 },
                    { 330, "کرج", 200000, false, 5 },
                    { 331, "طالقان", 200000, false, 5 },
                    { 332, "شهرجدید هشتگرد", 200000, false, 5 },
                    { 333, "محمدشهر", 200000, false, 5 },
                    { 334, "مشکین دشت", 200000, false, 5 },
                    { 335, "نظرآباد", 200000, false, 5 },
                    { 336, "هشتگرد", 200000, false, 5 },
                    { 337, "ماهدشت", 200000, false, 5 },
                    { 338, "اشتهارد", 200000, false, 5 },
                    { 339, "کوهسار", 200000, false, 5 },
                    { 340, "گرمدره", 200000, false, 5 },
                    { 341, "تنکمان", 200000, false, 5 },
                    { 342, "گلسار", 200000, false, 5 },
                    { 343, "کمال شهر", 200000, false, 5 },
                    { 344, "فردیس", 200000, false, 5 },
                    { 345, "آبدانان", 200000, false, 6 },
                    { 346, "شباب", 200000, false, 6 },
                    { 347, "موسیان", 200000, false, 6 },
                    { 348, "بدره", 200000, false, 6 },
                    { 349, "ایلام", 200000, false, 6 },
                    { 350, "ایوان", 200000, false, 6 },
                    { 351, "مهران", 200000, false, 6 },
                    { 352, "آسمان آباد", 200000, false, 6 },
                    { 353, "پهله", 200000, false, 6 },
                    { 354, "مهر", 200000, false, 6 },
                    { 355, "سراب باغ", 200000, false, 6 },
                    { 356, "بلاوه", 200000, false, 6 },
                    { 357, "میمه", 200000, false, 6 },
                    { 358, "دره شهر", 200000, false, 6 },
                    { 359, "ارکواز", 200000, false, 6 },
                    { 360, "مورموری", 200000, false, 6 },
                    { 361, "توحید", 200000, false, 6 },
                    { 362, "دهلران", 200000, false, 6 },
                    { 363, "لومار", 200000, false, 6 },
                    { 364, "چوار", 200000, false, 6 },
                    { 365, "زرنه", 200000, false, 6 },
                    { 366, "صالح آباد", 200000, false, 6 },
                    { 367, "سرابله", 200000, false, 6 },
                    { 368, "ماژین", 200000, false, 6 },
                    { 369, "دلگشا", 200000, false, 6 },
                    { 370, "ریز", 200000, false, 7 },
                    { 371, "برازجان", 200000, false, 7 },
                    { 372, "بندر ریگ", 200000, false, 7 },
                    { 373, "اهرم", 200000, false, 7 },
                    { 374, "دوراهک", 200000, false, 7 },
                    { 375, "خورموج", 200000, false, 7 },
                    { 376, "نخل تقی", 200000, false, 7 },
                    { 377, "کلمه", 200000, false, 7 },
                    { 378, "بندر دیلم", 200000, false, 7 },
                    { 379, "وحدتیه", 200000, false, 7 },
                    { 380, "بنک", 200000, false, 7 },
                    { 381, "چغادک", 200000, false, 7 },
                    { 382, "بندر دیر", 200000, false, 7 },
                    { 383, "کاکی", 200000, false, 7 },
                    { 384, "جم", 200000, false, 7 },
                    { 385, "دالکی", 200000, false, 7 },
                    { 386, "بندر گناوه", 200000, false, 7 },
                    { 387, "آباد", 200000, false, 7 },
                    { 388, "آبدان", 200000, false, 7 },
                    { 389, "خارک", 200000, false, 7 },
                    { 390, "شنبه", 200000, false, 7 },
                    { 391, "بوشکان", 200000, false, 7 },
                    { 392, "انارستان", 200000, false, 7 },
                    { 393, "شبانکاره", 200000, false, 7 },
                    { 394, "سیراف", 200000, false, 7 },
                    { 395, "دلوار", 200000, false, 7 },
                    { 396, "بردستان", 200000, false, 7 },
                    { 397, "بادوله", 200000, false, 7 },
                    { 398, "عسلویه", 200000, false, 7 },
                    { 399, "تنگ ارم", 200000, false, 7 },
                    { 400, "امام حسن", 200000, false, 7 },
                    { 401, "سعد آباد", 200000, false, 7 },
                    { 402, "بندر کنگان", 200000, false, 7 },
                    { 403, "بوشهر", 200000, false, 7 },
                    { 404, "بردخون", 200000, false, 7 },
                    { 405, "آب پخش", 200000, false, 7 },
                    { 406, "شاهدشهر", 200000, false, 8 },
                    { 407, "پیشوا", 200000, false, 8 },
                    { 408, "جوادآباد", 200000, false, 8 },
                    { 409, "ارجمند", 200000, false, 8 },
                    { 410, "ری", 200000, false, 8 },
                    { 411, "نصیر شهر", 200000, false, 8 },
                    { 412, "رودهن", 200000, false, 8 },
                    { 413, "اندیشه", 200000, false, 8 },
                    { 414, "نسیم شهر", 200000, false, 8 },
                    { 415, "صبا شهر", 200000, false, 8 },
                    { 416, "ملارد", 200000, false, 8 },
                    { 417, "شمشک", 200000, false, 8 },
                    { 418, "پاکدشت", 200000, false, 8 },
                    { 419, "باقرشهر", 200000, false, 8 },
                    { 420, "احمدآباد مستوفی", 200000, false, 8 },
                    { 421, "کیلان", 200000, false, 8 },
                    { 422, "قرچک", 200000, false, 8 },
                    { 423, "فردوسیه", 200000, false, 8 },
                    { 424, "گلستان", 200000, false, 8 },
                    { 425, "ورامین", 200000, false, 8 },
                    { 426, "فیروزکوه", 200000, false, 8 },
                    { 427, "فشم", 200000, false, 8 },
                    { 428, "پرند", 200000, false, 8 },
                    { 429, "آبعلی", 200000, false, 8 },
                    { 430, "چهاردانگه", 200000, false, 8 },
                    { 431, "تهران", 200000, false, 8 },
                    { 432, "بومهن", 200000, false, 8 },
                    { 433, "وحیدیه", 200000, false, 8 },
                    { 434, "صفادشت", 200000, false, 8 },
                    { 435, "لواسان", 200000, false, 8 },
                    { 436, "فرون آباد", 200000, false, 8 },
                    { 437, "کهریزک", 200000, false, 8 },
                    { 438, "رباط کریم", 200000, false, 8 },
                    { 439, "آبسرد", 200000, false, 8 },
                    { 440, "باغستان", 200000, false, 8 },
                    { 441, "صالحیه", 200000, false, 8 },
                    { 442, "شهریار", 200000, false, 8 },
                    { 443, "قدس", 200000, false, 8 },
                    { 444, "تجریش", 200000, false, 8 },
                    { 445, "شریف آباد", 200000, false, 8 },
                    { 446, "حسن آباد", 200000, false, 8 },
                    { 447, "اسلامشهر", 200000, false, 8 },
                    { 448, "دماوند", 200000, false, 8 },
                    { 449, "پردیس", 200000, false, 8 },
                    { 450, "وردنجان", 200000, false, 9 },
                    { 451, "گوجان", 200000, false, 9 },
                    { 452, "گهرو", 200000, false, 9 },
                    { 453, "سورشجان", 200000, false, 9 },
                    { 454, "سرخون", 200000, false, 9 },
                    { 455, "شهرکرد", 200000, false, 9 },
                    { 456, "منج", 200000, false, 9 },
                    { 457, "بروجن", 200000, false, 9 },
                    { 458, "پردنجان", 200000, false, 9 },
                    { 459, "سامان", 200000, false, 9 },
                    { 460, "فرخ شهر", 200000, false, 9 },
                    { 461, "صمصامی", 200000, false, 9 },
                    { 462, "طاقانک", 200000, false, 9 },
                    { 463, "کاج", 200000, false, 9 },
                    { 464, "نقنه", 200000, false, 9 },
                    { 465, "لردگان", 200000, false, 9 },
                    { 466, "باباحیدر", 200000, false, 9 },
                    { 467, "دستنا", 200000, false, 9 },
                    { 468, "سودجان", 200000, false, 9 },
                    { 469, "بازفت", 200000, false, 9 },
                    { 470, "هفشجان", 200000, false, 9 },
                    { 471, "سردشت", 200000, false, 9 },
                    { 472, "فرادنبه", 200000, false, 9 },
                    { 473, "چلیچه", 200000, false, 9 },
                    { 474, "بن", 200000, false, 9 },
                    { 475, "فارسان", 200000, false, 9 },
                    { 476, "شلمزار", 200000, false, 9 },
                    { 477, "نافچ", 200000, false, 9 },
                    { 478, "دشتک", 200000, false, 9 },
                    { 479, "بلداجی", 200000, false, 9 },
                    { 480, "آلونی", 200000, false, 9 },
                    { 481, "گندمان", 200000, false, 9 },
                    { 482, "جونقان", 200000, false, 9 },
                    { 483, "ناغان", 200000, false, 9 },
                    { 484, "هارونی", 200000, false, 9 },
                    { 485, "چلگرد", 200000, false, 9 },
                    { 486, "کیان", 200000, false, 9 },
                    { 487, "اردل", 200000, false, 9 },
                    { 488, "سفیددشت", 200000, false, 9 },
                    { 489, "مال خلیفه", 200000, false, 9 },
                    { 490, "اسلامیه", 200000, false, 10 },
                    { 491, "شوسف", 200000, false, 10 },
                    { 492, "قاین", 200000, false, 10 },
                    { 493, "عشق آباد", 200000, false, 10 },
                    { 494, "طبس مسینا", 200000, false, 10 },
                    { 495, "ارسک", 200000, false, 10 },
                    { 496, "آیسک", 200000, false, 10 },
                    { 497, "نیمبلوک", 200000, false, 10 },
                    { 498, "دیهوک", 200000, false, 10 },
                    { 499, "سر بیشه", 200000, false, 10 },
                    { 500, "محمدشهر", 200000, false, 10 },
                    { 501, "بیرجند", 200000, false, 10 },
                    { 502, "فردوس", 200000, false, 10 },
                    { 503, "نهبندان", 200000, false, 10 },
                    { 504, "اسفدن", 200000, false, 10 },
                    { 505, "گزیک", 200000, false, 10 },
                    { 506, "حاجی آباد", 200000, false, 10 },
                    { 507, "سه قلعه", 200000, false, 10 },
                    { 508, "آرین شهر", 200000, false, 10 },
                    { 509, "مود", 200000, false, 10 },
                    { 510, "خوسف", 200000, false, 10 },
                    { 511, "قهستان", 200000, false, 10 },
                    { 512, "بشرویه", 200000, false, 10 },
                    { 513, "سرایان", 200000, false, 10 },
                    { 514, "خضری دشت بیاض", 200000, false, 10 },
                    { 515, "طبس", 200000, false, 10 },
                    { 516, "اسدیه", 200000, false, 10 },
                    { 517, "زهان", 200000, false, 10 },
                    { 518, "بار", 200000, false, 11 },
                    { 519, "نیل شهر", 200000, false, 11 },
                    { 520, "جنگل", 200000, false, 11 },
                    { 521, "درود", 200000, false, 11 },
                    { 522, "رباط سنگ", 200000, false, 11 },
                    { 523, "سلطان آباد", 200000, false, 11 },
                    { 524, "فریمان", 200000, false, 11 },
                    { 525, "گناباد", 200000, false, 11 },
                    { 526, "کاریز", 200000, false, 11 },
                    { 527, "همت آباد", 200000, false, 11 },
                    { 528, "سلامی", 200000, false, 11 },
                    { 529, "باجگیران", 200000, false, 11 },
                    { 530, "بجستان", 200000, false, 11 },
                    { 531, "چناران", 200000, false, 11 },
                    { 532, "درگز", 200000, false, 11 },
                    { 533, "کلات", 200000, false, 11 },
                    { 534, "چکنه", 200000, false, 11 },
                    { 535, "نصرآباد", 200000, false, 11 },
                    { 536, "بردسکن", 200000, false, 11 },
                    { 537, "مشهد", 200000, false, 11 },
                    { 538, "کدکن", 200000, false, 11 },
                    { 539, "نقاب", 200000, false, 11 },
                    { 540, "قلندرآباد", 200000, false, 11 },
                    { 541, "کاشمر", 200000, false, 11 },
                    { 542, "شاندیز", 200000, false, 11 },
                    { 543, "نشتیفان", 200000, false, 11 },
                    { 544, "ششتمد", 200000, false, 11 },
                    { 545, "شادمهر", 200000, false, 11 },
                    { 546, "عشق آباد", 200000, false, 11 },
                    { 547, "چاپشلو", 200000, false, 11 },
                    { 548, "رشتخوار", 200000, false, 11 },
                    { 549, "قدمگاه", 200000, false, 11 },
                    { 550, "صالح آباد", 200000, false, 11 },
                    { 551, "داورزن", 200000, false, 11 },
                    { 552, "فرهادگاه", 200000, false, 11 },
                    { 553, "کاخک", 200000, false, 11 },
                    { 554, "مشهدریزه", 200000, false, 11 },
                    { 555, "جغتای", 200000, false, 11 },
                    { 556, "مزدآوند", 200000, false, 11 },
                    { 557, "قوچان", 200000, false, 11 },
                    { 558, "یونسی", 200000, false, 11 },
                    { 559, "سنگان", 200000, false, 11 },
                    { 560, "نوخندان", 200000, false, 11 },
                    { 561, "کندر", 200000, false, 11 },
                    { 562, "نیشابور", 200000, false, 11 },
                    { 563, "احمدآباد صولت", 200000, false, 11 },
                    { 564, "شهرآباد", 200000, false, 11 },
                    { 565, "رضویه", 200000, false, 11 },
                    { 566, "تربت حیدریه", 200000, false, 11 },
                    { 567, "باخرز", 200000, false, 11 },
                    { 568, "سفید سنگ", 200000, false, 11 },
                    { 569, "بیدخت", 200000, false, 11 },
                    { 570, "تایباد", 200000, false, 11 },
                    { 571, "فیروزه", 200000, false, 11 },
                    { 572, "قاسم آباد", 200000, false, 11 },
                    { 573, "سبزوار", 200000, false, 11 },
                    { 574, "فیض آباد", 200000, false, 11 },
                    { 575, "گلمکان", 200000, false, 11 },
                    { 576, "لطف آباد", 200000, false, 11 },
                    { 577, "شهرزو", 200000, false, 11 },
                    { 578, "خرو", 200000, false, 11 },
                    { 579, "تربت جام", 200000, false, 11 },
                    { 580, "انابد", 200000, false, 11 },
                    { 581, "ملک آباد", 200000, false, 11 },
                    { 582, "بایک", 200000, false, 11 },
                    { 583, "دولت آباد", 200000, false, 11 },
                    { 584, "سرخس", 200000, false, 11 },
                    { 585, "ریوش", 200000, false, 11 },
                    { 586, "طرقبه", 200000, false, 11 },
                    { 587, "خواف", 200000, false, 11 },
                    { 588, "روداب", 200000, false, 11 },
                    { 589, "خلیل آباد", 200000, false, 11 },
                    { 590, "چناران شهر", 200000, false, 12 },
                    { 591, "راز", 200000, false, 12 },
                    { 592, "پیش قلعه", 200000, false, 12 },
                    { 593, "قوشخانه", 200000, false, 12 },
                    { 594, "شوقان", 200000, false, 12 },
                    { 595, "اسفراین", 200000, false, 12 },
                    { 596, "گرمه", 200000, false, 12 },
                    { 597, "قاضی", 200000, false, 12 },
                    { 598, "شیروان", 200000, false, 12 },
                    { 599, "خصار گرمخان", 200000, false, 12 },
                    { 600, "آشخانه", 200000, false, 12 },
                    { 601, "تیتکانلو", 200000, false, 12 },
                    { 602, "جاجرم", 200000, false, 12 },
                    { 603, "بجنورد", 200000, false, 12 },
                    { 604, "درق", 200000, false, 12 },
                    { 605, "آوا", 200000, false, 12 },
                    { 606, "زیارت", 200000, false, 12 },
                    { 607, "سنخواست", 200000, false, 12 },
                    { 608, "صفی آباد", 200000, false, 12 },
                    { 609, "ایور", 200000, false, 12 },
                    { 610, "فاروج", 200000, false, 12 },
                    { 611, "لوجلی", 200000, false, 12 },
                    { 612, "هفتگل", 200000, false, 13 },
                    { 613, "بیدروبه", 200000, false, 13 },
                    { 614, "شاوور", 200000, false, 13 },
                    { 615, "حمزه", 200000, false, 13 },
                    { 616, "گتوند", 200000, false, 13 },
                    { 617, "شرافت", 200000, false, 13 },
                    { 618, "منصوریه", 200000, false, 13 },
                    { 619, "زهره", 200000, false, 13 },
                    { 620, "رامهرمز", 200000, false, 13 },
                    { 621, "بندر امام خمینی", 200000, false, 13 },
                    { 622, "کوت عبداله", 200000, false, 13 },
                    { 623, "میداود", 200000, false, 13 },
                    { 624, "چغامیش", 200000, false, 13 },
                    { 625, "ملاثانی", 200000, false, 13 },
                    { 626, "چم گلک", 200000, false, 13 },
                    { 627, "حر", 200000, false, 13 },
                    { 628, "شمس آباد", 200000, false, 13 },
                    { 629, "آبژدان", 200000, false, 13 },
                    { 630, "چوبیده", 200000, false, 13 },
                    { 631, "مسجد سلیمان", 200000, false, 13 },
                    { 632, "مقاومت", 200000, false, 13 },
                    { 633, "ترکالکی", 200000, false, 13 },
                    { 634, "دارخوین", 200000, false, 13 },
                    { 635, "سردشت", 200000, false, 13 },
                    { 636, "لالی", 200000, false, 13 },
                    { 637, "کوت سید نعیم", 200000, false, 13 },
                    { 638, "حمیدیه", 200000, false, 13 },
                    { 639, "دهدز", 200000, false, 13 },
                    { 640, "قلعه تل", 200000, false, 13 },
                    { 641, "میانرود", 200000, false, 13 },
                    { 642, "رفیع", 200000, false, 13 },
                    { 643, "اندیمشک", 200000, false, 13 },
                    { 644, "الوان", 200000, false, 13 },
                    { 645, "سالند", 200000, false, 13 },
                    { 646, "صالح شهر", 200000, false, 13 },
                    { 647, "اروندکنار", 200000, false, 13 },
                    { 648, "سرداران", 200000, false, 13 },
                    { 649, "تشان", 200000, false, 13 },
                    { 650, "رامشیر", 200000, false, 13 },
                    { 651, "شادگان", 200000, false, 13 },
                    { 652, "بندر ماهشهر", 200000, false, 13 },
                    { 653, "جایزان", 200000, false, 13 },
                    { 654, "بستان", 200000, false, 13 },
                    { 655, "ویس", 200000, false, 13 },
                    { 656, "اهواز", 200000, false, 13 },
                    { 657, "فتح المبین", 200000, false, 13 },
                    { 658, "شهر امام", 200000, false, 13 },
                    { 659, "قلعه خواجه", 200000, false, 13 },
                    { 660, "حسینیه", 200000, false, 13 },
                    { 661, "گلگیر", 200000, false, 13 },
                    { 662, "مینوشهر", 200000, false, 13 },
                    { 663, "سماله", 200000, false, 13 },
                    { 664, "شوشتر", 200000, false, 13 },
                    { 665, "بهبهان", 200000, false, 13 },
                    { 666, "هندیجان", 200000, false, 13 },
                    { 667, "ابوحمیظه", 200000, false, 13 },
                    { 668, "آغاجاری", 200000, false, 13 },
                    { 669, "ایذه", 200000, false, 13 },
                    { 670, "صیدون", 200000, false, 13 },
                    { 671, "سیاه منصور", 200000, false, 13 },
                    { 672, "هویزه", 200000, false, 13 },
                    { 673, "آزادی", 200000, false, 13 },
                    { 674, "شوش", 200000, false, 13 },
                    { 675, "دزفول", 200000, false, 13 },
                    { 676, "جنت مکان", 200000, false, 13 },
                    { 677, "آبادان", 200000, false, 13 },
                    { 678, "گوریه", 200000, false, 13 },
                    { 679, "خرمشهر", 200000, false, 13 },
                    { 680, "مشراگه", 200000, false, 13 },
                    { 681, "خنافره", 200000, false, 13 },
                    { 682, "چمران", 200000, false, 13 },
                    { 683, "امیدیه", 200000, false, 13 },
                    { 684, "سوسنگرد", 200000, false, 13 },
                    { 685, "شیبان", 200000, false, 13 },
                    { 686, "الهایی", 200000, false, 13 },
                    { 687, "باغ ملک", 200000, false, 13 },
                    { 688, "صفی آباد", 200000, false, 13 },
                    { 689, "سجاس", 200000, false, 14 },
                    { 690, "زرین رود", 200000, false, 14 },
                    { 691, "آب بر", 200000, false, 14 },
                    { 692, "ارمغانخانه", 200000, false, 14 },
                    { 693, "کرسف", 200000, false, 14 },
                    { 694, "هیدج", 200000, false, 14 },
                    { 695, "سلطانیه", 200000, false, 14 },
                    { 696, "خرمدره", 200000, false, 14 },
                    { 697, "نیک پی", 200000, false, 14 },
                    { 698, "قیدار", 200000, false, 14 },
                    { 699, "ابهر", 200000, false, 14 },
                    { 700, "دندی", 200000, false, 14 },
                    { 701, "حلب", 200000, false, 14 },
                    { 702, "نور بهار", 200000, false, 14 },
                    { 703, "گرماب", 200000, false, 14 },
                    { 704, "چورزق", 200000, false, 14 },
                    { 705, "زنجان", 200000, false, 14 },
                    { 706, "سهرود", 200000, false, 14 },
                    { 707, "صایین قلعه", 200000, false, 14 },
                    { 708, "ماه نشان", 200000, false, 14 },
                    { 709, "زرین آباد", 200000, false, 14 },
                    { 710, "ایوانکی", 200000, false, 15 },
                    { 711, "مجن", 200000, false, 15 },
                    { 712, "دامغان", 200000, false, 15 },
                    { 713, "سرخه", 200000, false, 15 },
                    { 714, "مهدی شهر", 200000, false, 15 },
                    { 715, "شاهرود", 200000, false, 15 },
                    { 716, "سمنان", 200000, false, 15 },
                    { 717, "کهن آباد", 200000, false, 15 },
                    { 718, "گرمسار", 200000, false, 15 },
                    { 719, "کلاته خیج", 200000, false, 15 },
                    { 720, "دیباج", 200000, false, 15 },
                    { 721, "درجزین", 200000, false, 15 },
                    { 722, "رودیان", 200000, false, 15 },
                    { 723, "بسطام", 200000, false, 15 },
                    { 724, "امیریه", 200000, false, 15 },
                    { 725, "میامی", 200000, false, 15 },
                    { 726, "شهمیرزاد", 200000, false, 15 },
                    { 727, "بیارجمند", 200000, false, 15 },
                    { 728, "کلاته", 200000, false, 15 },
                    { 729, "آرادان", 200000, false, 15 },
                    { 730, "محمدی", 200000, false, 16 },
                    { 731, "شهرک علی اکبر", 200000, false, 16 },
                    { 732, "بنجار", 200000, false, 16 },
                    { 733, "گلمورتی", 200000, false, 16 },
                    { 734, "نگور", 200000, false, 16 },
                    { 735, "راسک", 200000, false, 16 },
                    { 736, "بنت", 200000, false, 16 },
                    { 737, "قصرقند", 200000, false, 16 },
                    { 738, "جالق", 200000, false, 16 },
                    { 739, "هیدوچ", 200000, false, 16 },
                    { 740, "نوک آباد", 200000, false, 16 },
                    { 741, "زهک", 200000, false, 16 },
                    { 742, "بمبپور", 200000, false, 16 },
                    { 743, "پیشین", 200000, false, 16 },
                    { 744, "گشت", 200000, false, 16 },
                    { 745, "محمدآباد", 200000, false, 16 },
                    { 746, "زاهدان", 200000, false, 16 },
                    { 747, "زابلی", 200000, false, 16 },
                    { 748, "چاه بهار", 200000, false, 16 },
                    { 749, "زرآباد", 200000, false, 16 },
                    { 750, "بزمان", 200000, false, 16 },
                    { 751, "اسپکه", 200000, false, 16 },
                    { 752, "فنوج", 200000, false, 16 },
                    { 753, "سراوان", 200000, false, 16 },
                    { 754, "ادیمی", 200000, false, 16 },
                    { 755, "زابل", 200000, false, 16 },
                    { 756, "دوست محمد", 200000, false, 16 },
                    { 757, "ایرانشهر", 200000, false, 16 },
                    { 758, "سرباز", 200000, false, 16 },
                    { 759, "سیرکان", 200000, false, 16 },
                    { 760, "میرجاوه", 200000, false, 16 },
                    { 761, "نصرت آباد", 200000, false, 16 },
                    { 762, "سوران", 200000, false, 16 },
                    { 763, "خاش", 200000, false, 16 },
                    { 764, "کنارک", 200000, false, 16 },
                    { 765, "محمدان", 200000, false, 16 },
                    { 766, "نیک شهر", 200000, false, 16 },
                    { 767, "کازرون", 200000, false, 17 },
                    { 768, "کارزین", 200000, false, 17 },
                    { 769, "فدامی", 200000, false, 17 },
                    { 770, "خومه زار", 200000, false, 17 },
                    { 771, "سلطان آباد", 200000, false, 17 },
                    { 772, "فیروزآباد", 200000, false, 17 },
                    { 773, "دبیران", 200000, false, 17 },
                    { 774, "باب انار", 200000, false, 17 },
                    { 775, "رامجرد", 200000, false, 17 },
                    { 776, "سروستان", 200000, false, 17 },
                    { 777, "قره بلاغ", 200000, false, 17 },
                    { 778, "ارسنجان", 200000, false, 17 },
                    { 779, "دژکرد", 200000, false, 17 },
                    { 780, "بیرم", 200000, false, 17 },
                    { 781, "دهرم", 200000, false, 17 },
                    { 782, "شیراز", 200000, false, 17 },
                    { 783, "ایزدخواست", 200000, false, 17 },
                    { 784, "علامرودشت", 200000, false, 17 },
                    { 785, "اوز", 200000, false, 17 },
                    { 786, "وراوی", 200000, false, 17 },
                    { 787, "بیضا", 200000, false, 17 },
                    { 788, "نی ریز", 200000, false, 17 },
                    { 789, "کنار تخته", 200000, false, 17 },
                    { 790, "امام شهر", 200000, false, 17 },
                    { 791, "جهرم", 200000, false, 17 },
                    { 792, "بابامنیر", 200000, false, 17 },
                    { 793, "گراش", 200000, false, 17 },
                    { 794, "فسا", 200000, false, 17 },
                    { 795, "شهرپیر", 200000, false, 17 },
                    { 796, "حسن آباد", 200000, false, 17 },
                    { 797, "کامفیروز", 200000, false, 17 },
                    { 798, "خنچ", 200000, false, 17 },
                    { 799, "خانه زنیان", 200000, false, 17 },
                    { 800, "استهبان", 200000, false, 17 },
                    { 801, "بوانات", 200000, false, 17 },
                    { 802, "لطیفی", 200000, false, 17 },
                    { 803, "فراشبند", 200000, false, 17 },
                    { 804, "زرقان", 200000, false, 17 },
                    { 805, "صغاد", 200000, false, 17 },
                    { 806, "اشکنان", 200000, false, 17 },
                    { 807, "قائمیه", 200000, false, 17 },
                    { 808, "گله دار", 200000, false, 17 },
                    { 809, "دوبرجی", 200000, false, 17 },
                    { 810, "آباده طشک", 200000, false, 17 },
                    { 811, "خرامه", 200000, false, 17 },
                    { 812, "میمند", 200000, false, 17 },
                    { 813, "افزر", 200000, false, 17 },
                    { 814, "دوزه", 200000, false, 17 },
                    { 815, "سیدان", 200000, false, 17 },
                    { 816, "کوپن", 200000, false, 17 },
                    { 817, "زاهدشهر", 200000, false, 17 },
                    { 818, "قادرآباد", 200000, false, 17 },
                    { 819, "سده", 200000, false, 17 },
                    { 820, "بنارویه", 200000, false, 17 },
                    { 821, "سعادت شهر", 200000, false, 17 },
                    { 822, "شهر صدرا", 200000, false, 17 },
                    { 823, "سورمق", 200000, false, 17 },
                    { 824, "حسامی", 200000, false, 17 },
                    { 825, "جویم", 200000, false, 17 },
                    { 826, "خوزی", 200000, false, 17 },
                    { 827, "اردکان", 200000, false, 17 },
                    { 828, "فطرویه", 200000, false, 17 },
                    { 829, "نودان", 200000, false, 17 },
                    { 830, "مبارک آباددیز", 200000, false, 17 },
                    { 831, "داراب", 200000, false, 17 },
                    { 832, "نورآباد", 200000, false, 17 },
                    { 833, "کوار", 200000, false, 17 },
                    { 834, "نوبندگان", 200000, false, 17 },
                    { 835, "حاجی آباد", 200000, false, 17 },
                    { 836, "خاوران", 200000, false, 17 },
                    { 837, "مرودشت", 200000, false, 17 },
                    { 838, "کوهنجان", 200000, false, 17 },
                    { 839, "ششده", 200000, false, 17 },
                    { 840, "مزایجان", 200000, false, 17 },
                    { 841, "ایج", 200000, false, 17 },
                    { 842, "خور", 200000, false, 17 },
                    { 843, "نوجین", 200000, false, 17 },
                    { 844, "لپویی", 200000, false, 17 },
                    { 845, "بهمن", 200000, false, 17 },
                    { 846, "اهل", 200000, false, 17 },
                    { 847, "خشت", 200000, false, 17 },
                    { 848, "مهر", 200000, false, 17 },
                    { 849, "جنت شهر", 200000, false, 17 },
                    { 850, "مشکان", 200000, false, 17 },
                    { 851, "بالاده", 200000, false, 17 },
                    { 852, "قیر", 200000, false, 17 },
                    { 853, "قطب آباد", 200000, false, 17 },
                    { 854, "خانمین", 200000, false, 17 },
                    { 855, "مصیری", 200000, false, 17 },
                    { 856, "میانشهر", 200000, false, 17 },
                    { 857, "صفاشهر", 200000, false, 17 },
                    { 858, "اقلید", 200000, false, 17 },
                    { 859, "عمادده", 200000, false, 17 },
                    { 860, "مادر سلیمان", 200000, false, 17 },
                    { 861, "داریان", 200000, false, 17 },
                    { 862, "رونیز", 200000, false, 17 },
                    { 863, "کره ای", 200000, false, 17 },
                    { 864, "لار", 200000, false, 17 },
                    { 865, "اسیر", 200000, false, 17 },
                    { 866, "هماشهر", 200000, false, 17 },
                    { 867, "آباده", 200000, false, 17 },
                    { 868, "لامرد", 200000, false, 17 },
                    { 869, "سگزآباد", 200000, false, 18 },
                    { 870, "بیدستان", 200000, false, 18 },
                    { 871, "کوهین", 200000, false, 18 },
                    { 872, "رازمیان", 200000, false, 18 },
                    { 873, "خرمدشت", 200000, false, 18 },
                    { 874, "آبگرم", 200000, false, 18 },
                    { 875, "شال", 200000, false, 18 },
                    { 876, "شریفیه", 200000, false, 18 },
                    { 877, "اقبالیه", 200000, false, 18 },
                    { 878, "نرجه", 200000, false, 18 },
                    { 879, "ارداق", 200000, false, 18 },
                    { 880, "الوند", 200000, false, 18 },
                    { 881, "خاکعلی", 200000, false, 18 },
                    { 882, "سیردان", 200000, false, 18 },
                    { 883, "ضیاد آباد", 200000, false, 18 },
                    { 884, "بوئین زهرا", 200000, false, 18 },
                    { 885, "محمدیه", 200000, false, 18 },
                    { 886, "محمود آباد نمونه", 200000, false, 18 },
                    { 887, "معلم کلایه", 200000, false, 18 },
                    { 888, "اسفرورین", 200000, false, 18 },
                    { 889, "آوج", 200000, false, 18 },
                    { 890, "دانسفهان", 200000, false, 18 },
                    { 891, "آبیک", 200000, false, 18 },
                    { 892, "قزوین", 200000, false, 18 },
                    { 893, "تاکستان", 200000, false, 18 },
                    { 894, "کهک", 200000, false, 19 },
                    { 895, "قم", 200000, false, 19 },
                    { 896, "سلفچگان", 200000, false, 19 },
                    { 897, "جعفریه", 200000, false, 19 },
                    { 898, "قنوات", 200000, false, 19 },
                    { 899, "دستجرد", 200000, false, 19 },
                    { 900, "قروه", 200000, false, 20 },
                    { 901, "توپ آغاج", 200000, false, 20 },
                    { 902, "سروآباد", 200000, false, 20 },
                    { 903, "بوئین سفلی", 200000, false, 20 },
                    { 904, "زرینه", 200000, false, 20 },
                    { 905, "دلبران", 200000, false, 20 },
                    { 906, "سنندج", 200000, false, 20 },
                    { 907, "یاسوکند", 200000, false, 20 },
                    { 908, "موچش", 200000, false, 20 },
                    { 909, "بانه", 200000, false, 20 },
                    { 910, "مریوان", 200000, false, 20 },
                    { 911, "سریش آباد", 200000, false, 20 },
                    { 912, "صاحب", 200000, false, 20 },
                    { 913, "دهگلان", 200000, false, 20 },
                    { 914, "بابارشانی", 200000, false, 20 },
                    { 915, "دیواندره", 200000, false, 20 },
                    { 916, "برده رشه", 200000, false, 20 },
                    { 917, "شویشه", 200000, false, 20 },
                    { 918, "بیجار", 200000, false, 20 },
                    { 919, "اورامان تخت", 200000, false, 20 },
                    { 920, "کانی سور", 200000, false, 20 },
                    { 921, "کانی دینار", 200000, false, 20 },
                    { 922, "دزج", 200000, false, 20 },
                    { 923, "سقز", 200000, false, 20 },
                    { 924, "بلبان آباد", 200000, false, 20 },
                    { 925, "پیرتاج", 200000, false, 20 },
                    { 926, "کامیاران", 200000, false, 20 },
                    { 927, "آرمرده", 200000, false, 20 },
                    { 928, "چناره", 200000, false, 20 },
                    { 929, "کهنوج", 200000, false, 21 },
                    { 930, "بلوک", 200000, false, 21 },
                    { 931, "پاریز", 200000, false, 21 },
                    { 932, "گنبکی", 200000, false, 21 },
                    { 933, "زنگی آباد", 200000, false, 21 },
                    { 934, "بم", 200000, false, 21 },
                    { 935, "خانوک", 200000, false, 21 },
                    { 936, "کیانشهر", 200000, false, 21 },
                    { 937, "جوپار", 200000, false, 21 },
                    { 938, "عنبر آباد", 200000, false, 21 },
                    { 939, "جوزم", 200000, false, 21 },
                    { 940, "نظام شهر", 200000, false, 21 },
                    { 941, "لاله زار", 200000, false, 21 },
                    { 942, "کشکوئیه", 200000, false, 21 },
                    { 943, "زیدآباد", 200000, false, 21 },
                    { 944, "هنزا", 200000, false, 21 },
                    { 945, "چترود", 200000, false, 21 },
                    { 946, "جبالبارز", 200000, false, 21 },
                    { 947, "سیرجان", 200000, false, 21 },
                    { 948, "رودبار", 200000, false, 21 },
                    { 949, "کرمان", 200000, false, 21 },
                    { 950, "بافت", 200000, false, 21 },
                    { 951, "صفائیه", 200000, false, 21 },
                    { 952, "منوجان", 200000, false, 21 },
                    { 953, "اندوهجرد", 200000, false, 21 },
                    { 954, "هجدک", 200000, false, 21 },
                    { 955, "خورسند", 200000, false, 21 },
                    { 956, "امین شهر", 200000, false, 21 },
                    { 957, "بردسیر", 200000, false, 21 },
                    { 958, "رفسنجان", 200000, false, 21 },
                    { 959, "هماشهر", 200000, false, 21 },
                    { 960, "محمد آباد", 200000, false, 21 },
                    { 961, "اختیار آباد", 200000, false, 21 },
                    { 962, "بروات", 200000, false, 21 },
                    { 963, "ریحان", 200000, false, 21 },
                    { 964, "کوهبنان", 200000, false, 21 },
                    { 965, "ماهان", 200000, false, 21 },
                    { 966, "دوساری", 200000, false, 21 },
                    { 967, "دهج", 200000, false, 21 },
                    { 968, "فاریاب", 200000, false, 21 },
                    { 969, "گلزار", 200000, false, 21 },
                    { 970, "بهرمان", 200000, false, 21 },
                    { 971, "بلورد", 200000, false, 21 },
                    { 972, "فهرج", 200000, false, 21 },
                    { 973, "کاظم آباد", 200000, false, 21 },
                    { 974, "جیرفت", 200000, false, 21 },
                    { 975, "نجف شهر", 200000, false, 21 },
                    { 976, "قلعه گنج", 200000, false, 21 },
                    { 977, "باغین", 200000, false, 21 },
                    { 978, "بزنجان", 200000, false, 21 },
                    { 979, "زرند", 200000, false, 21 },
                    { 980, "نودژ", 200000, false, 21 },
                    { 981, "گلباف", 200000, false, 21 },
                    { 982, "راور", 200000, false, 21 },
                    { 983, "خاتون آباد", 200000, false, 21 },
                    { 984, "نرمالشیر", 200000, false, 21 },
                    { 985, "دشتکار", 200000, false, 21 },
                    { 986, "مس سرچسمه", 200000, false, 21 },
                    { 987, "خواجو شهر", 200000, false, 21 },
                    { 989, "رابر", 200000, false, 21 },
                    { 990, "راین", 200000, false, 21 },
                    { 991, "درب بهشت", 200000, false, 21 },
                    { 992, "یزدان شهر", 200000, false, 21 },
                    { 993, "زهکلوت", 200000, false, 21 },
                    { 994, "محی آباد", 200000, false, 21 },
                    { 995, "مردهک", 200000, false, 21 },
                    { 996, "شهداد", 200000, false, 21 },
                    { 997, "ارزوئیه", 200000, false, 21 },
                    { 998, "نگار", 200000, false, 21 },
                    { 999, "شهربابک", 200000, false, 21 },
                    { 1000, "انار", 200000, false, 21 },
                    { 1001, "سنقر", 200000, false, 22 },
                    { 1002, "شاهو", 200000, false, 22 },
                    { 1003, "بانوره", 200000, false, 22 },
                    { 1004, "تازه آباد", 200000, false, 22 },
                    { 1005, "هلشی", 200000, false, 22 },
                    { 1006, "جوانرود", 200000, false, 22 },
                    { 1007, "قصر شیرین", 200000, false, 22 },
                    { 1008, "نوسود", 200000, false, 22 },
                    { 1009, "کرند", 200000, false, 22 },
                    { 1010, "کوزران", 200000, false, 22 },
                    { 1011, "بیستون", 200000, false, 22 },
                    { 1012, "حمیل", 200000, false, 22 },
                    { 1013, "گیلانغرب", 200000, false, 22 },
                    { 1014, "سطر", 200000, false, 22 },
                    { 1015, "روانسر", 200000, false, 22 },
                    { 1016, "پاوه", 200000, false, 22 },
                    { 1017, "ازگله", 200000, false, 22 },
                    { 1018, "کرمانشاه", 200000, false, 22 },
                    { 1019, "میان راهان", 200000, false, 22 },
                    { 1020, "کنگاور", 200000, false, 22 },
                    { 1021, "سرپل ذهاب", 200000, false, 22 },
                    { 1022, "ریجاب", 200000, false, 22 },
                    { 1023, "باینگان", 200000, false, 22 },
                    { 1024, "هرسین", 200000, false, 22 },
                    { 1025, "اسلام آباد غرب", 200000, false, 22 },
                    { 1026, "سرمست", 200000, false, 22 },
                    { 1027, "سومار", 200000, false, 22 },
                    { 1028, "نودشه", 200000, false, 22 },
                    { 1029, "گهواره", 200000, false, 22 },
                    { 1030, "رباط", 200000, false, 22 },
                    { 1031, "صحنه", 200000, false, 22 },
                    { 1032, "گودین", 200000, false, 22 },
                    { 1033, "گراب سفلی", 200000, false, 23 },
                    { 1034, "لنده", 200000, false, 23 },
                    { 1035, "سی سخت", 200000, false, 23 },
                    { 1036, "دهدشت", 200000, false, 23 },
                    { 1037, "یاسوج", 200000, false, 23 },
                    { 1038, "سرفاریاب", 200000, false, 23 },
                    { 1039, "دوگنبدان", 200000, false, 23 },
                    { 1040, "چیتاب", 200000, false, 23 },
                    { 1041, "لیکک", 200000, false, 23 },
                    { 1042, "دیشموک", 200000, false, 23 },
                    { 1043, "مادوان", 200000, false, 23 },
                    { 1044, "باشت", 200000, false, 23 },
                    { 1045, "پاتاوه", 200000, false, 23 },
                    { 1046, "قلعه رئیسی", 200000, false, 23 },
                    { 1047, "مارگون", 200000, false, 23 },
                    { 1048, "چرام", 200000, false, 23 },
                    { 1049, "سوق", 200000, false, 23 },
                    { 1050, "سیمین شهر", 200000, false, 24 },
                    { 1051, "مزرعه", 200000, false, 24 },
                    { 1052, "رامیان", 200000, false, 24 },
                    { 1053, "فراغی", 200000, false, 24 },
                    { 1054, "گنبد کاووس", 200000, false, 24 },
                    { 1055, "کردکوی", 200000, false, 24 },
                    { 1056, "مراوه", 200000, false, 24 },
                    { 1057, "بندر ترکمن", 200000, false, 24 },
                    { 1058, "نگین شهر", 200000, false, 24 },
                    { 1059, "آق قلا", 200000, false, 24 },
                    { 1060, "سرخنکلاته", 200000, false, 24 },
                    { 1061, "گالیکش", 200000, false, 24 },
                    { 1062, "سنگدوین", 200000, false, 24 },
                    { 1063, "دلند", 200000, false, 24 },
                    { 1064, "بندر گز", 200000, false, 24 },
                    { 1065, "نوده خاندوز", 200000, false, 24 },
                    { 1066, "مینو دشت", 200000, false, 24 },
                    { 1067, "گرگان", 200000, false, 24 },
                    { 1068, "گمیش تپه", 200000, false, 24 },
                    { 1069, "علی آباد", 200000, false, 24 },
                    { 1070, "خان ببین", 200000, false, 24 },
                    { 1071, "کلاله", 200000, false, 24 },
                    { 1072, "اینچه برون", 200000, false, 24 },
                    { 1073, "فاضل آباد", 200000, false, 24 },
                    { 1074, "تاتار علیا", 200000, false, 24 },
                    { 1075, "نوکنده", 200000, false, 24 },
                    { 1076, "آزاد شهر", 200000, false, 24 },
                    { 1077, "انبار آلوم", 200000, false, 24 },
                    { 1078, "جلین", 200000, false, 24 },
                    { 1079, "منجیل", 200000, false, 25 },
                    { 1080, "شلمان", 200000, false, 25 },
                    { 1081, "خشکبیجار", 200000, false, 25 },
                    { 1082, "ماکلوان", 200000, false, 25 },
                    { 1083, "سنگر", 200000, false, 25 },
                    { 1084, "مرجقل", 200000, false, 25 },
                    { 1085, "لیسار", 200000, false, 25 },
                    { 1086, "رضوانشهر", 200000, false, 25 },
                    { 1087, "رحیم آباد", 200000, false, 25 },
                    { 1088, "لوندویل", 200000, false, 25 },
                    { 1089, "احمد سرگوراب", 200000, false, 25 },
                    { 1090, "لوشان", 200000, false, 25 },
                    { 1091, "اطاقوار", 200000, false, 25 },
                    { 1092, "لشت نشاء", 200000, false, 25 },
                    { 1093, "فومن", 200000, false, 25 },
                    { 1094, "چوبر", 200000, false, 25 },
                    { 1095, "بازار جمعه", 200000, false, 25 },
                    { 1096, "کلاچای", 200000, false, 25 },
                    { 1097, "بندر انزلی", 200000, false, 25 },
                    { 1098, "املش", 200000, false, 25 },
                    { 1099, "رستم آباد", 200000, false, 25 },
                    { 1100, "لاهیجان", 200000, false, 25 },
                    { 1101, "توتکابن", 200000, false, 25 },
                    { 1102, "لنگرود", 200000, false, 25 },
                    { 1103, "کوچصفهان", 200000, false, 25 },
                    { 1104, "صومعه سرا", 200000, false, 25 },
                    { 1105, "اسالم", 200000, false, 25 },
                    { 1106, "دیلمان", 200000, false, 25 },
                    { 1107, "رودسر", 200000, false, 25 },
                    { 1108, "کیاشهر", 200000, false, 25 },
                    { 1109, "شفت", 200000, false, 25 },
                    { 1110, "رودبار", 200000, false, 25 },
                    { 1111, "کومله", 200000, false, 25 },
                    { 1112, "رشت", 200000, false, 25 },
                    { 1113, "ماسوله", 200000, false, 25 },
                    { 1114, "خمام", 200000, false, 25 },
                    { 1115, "ماسال", 200000, false, 25 },
                    { 1116, "واجارگاه", 200000, false, 25 },
                    { 1117, "هشتپر (تالش)", 200000, false, 25 },
                    { 1118, "پره سر", 200000, false, 25 },
                    { 1119, "بره سر", 200000, false, 25 },
                    { 1120, "آستارا", 200000, false, 25 },
                    { 1121, "رودبنه", 200000, false, 25 },
                    { 1122, "جیرنده", 200000, false, 25 },
                    { 1123, "چاف و چمخاله", 200000, false, 25 },
                    { 1124, "لولمان", 200000, false, 25 },
                    { 1125, "گوراب زرمیخ", 200000, false, 25 },
                    { 1126, "حویق", 200000, false, 25 },
                    { 1127, "سیاهکل", 200000, false, 25 },
                    { 1128, "چابکسر", 200000, false, 25 },
                    { 1129, "آستانه اشرفیه", 200000, false, 25 },
                    { 1130, "رانکوه", 200000, false, 25 },
                    { 1131, "چالانچولان", 200000, false, 26 },
                    { 1132, "بیران شهر", 200000, false, 26 },
                    { 1133, "ویسیان", 200000, false, 26 },
                    { 1134, "شول آباد", 200000, false, 26 },
                    { 1135, "پلدختر", 200000, false, 26 },
                    { 1136, "کوهدشت", 200000, false, 26 },
                    { 1137, "هفت چشمه", 200000, false, 26 },
                    { 1138, "بروجرد", 200000, false, 26 },
                    { 1139, "الشتر", 200000, false, 26 },
                    { 1140, "مومن آباد", 200000, false, 26 },
                    { 1141, "دورود", 200000, false, 26 },
                    { 1142, "زاغه", 200000, false, 26 },
                    { 1143, "چقابل", 200000, false, 26 },
                    { 1144, "الیگودرز", 200000, false, 26 },
                    { 1145, "معمولان", 200000, false, 26 },
                    { 1146, "کوهنانی", 200000, false, 26 },
                    { 1147, "نورآباد", 200000, false, 26 },
                    { 1148, "سپیددشت", 200000, false, 26 },
                    { 1149, "سراب دوره", 200000, false, 26 },
                    { 1150, "ازنا", 200000, false, 26 },
                    { 1151, "گراب", 200000, false, 26 },
                    { 1152, "خرم آباد", 200000, false, 26 },
                    { 1153, "اشترینان", 200000, false, 26 },
                    { 1154, "فیروز آباد", 200000, false, 26 },
                    { 1155, "درب گنبد", 200000, false, 26 },
                    { 1156, "گلوگاه", 200000, false, 27 },
                    { 1157, "پل سفید", 200000, false, 27 },
                    { 1158, "دابودشت", 200000, false, 27 },
                    { 1159, "چالوس", 200000, false, 27 },
                    { 1160, "کیاسر", 200000, false, 27 },
                    { 1161, "بهمنمیر", 200000, false, 27 },
                    { 1162, "تنکابن", 200000, false, 27 },
                    { 1163, "کلاردشت", 200000, false, 27 },
                    { 1164, "ایزدشهر", 200000, false, 27 },
                    { 1165, "گتاب", 200000, false, 27 },
                    { 1166, "سلمان شهر", 200000, false, 27 },
                    { 1167, "ارطه", 200000, false, 27 },
                    { 1168, "امیرکلا", 200000, false, 27 },
                    { 1169, "کوهی خیل", 200000, false, 27 },
                    { 1170, "پایین هولار", 200000, false, 27 },
                    { 1171, "گزنک", 200000, false, 27 },
                    { 1172, "محمود آباد", 200000, false, 27 },
                    { 1173, "رامسر", 200000, false, 27 },
                    { 1174, "نوشهر", 200000, false, 27 },
                    { 1175, "خلیل آباد", 200000, false, 27 },
                    { 1176, "کیاکلا", 200000, false, 27 },
                    { 1177, "نور", 200000, false, 27 },
                    { 1178, "مرزیکلا", 200000, false, 27 },
                    { 1179, "فریدونکنار", 200000, false, 27 },
                    { 1180, "زیرآب", 200000, false, 27 },
                    { 1181, "امامزاده عبدالله", 200000, false, 27 },
                    { 1182, "هچیرود", 200000, false, 27 },
                    { 1183, "فریم", 200000, false, 27 },
                    { 1184, "هادی شهر", 200000, false, 27 },
                    { 1185, "نشتارود", 200000, false, 27 },
                    { 1186, "پول", 200000, false, 27 },
                    { 1187, "بهشهر", 200000, false, 27 },
                    { 1188, "کلارآباد", 200000, false, 27 },
                    { 1189, "بلده", 200000, false, 27 },
                    { 1190, "بابل", 200000, false, 27 },
                    { 1191, "جویبار", 200000, false, 27 },
                    { 1192, "آلاشت", 200000, false, 27 },
                    { 1193, "آمل", 200000, false, 27 },
                    { 1194, "نکا", 200000, false, 27 },
                    { 1195, "کتالم و سادات شهر", 200000, false, 27 },
                    { 1196, "بابلسر", 200000, false, 27 },
                    { 1197, "شیرود", 200000, false, 27 },
                    { 1198, "شیرگاه", 200000, false, 27 },
                    { 1199, "رویان", 200000, false, 27 },
                    { 1200, "زرگر محله", 200000, false, 27 },
                    { 1201, "عباس آباد", 200000, false, 27 },
                    { 1202, "قائم شهر", 200000, false, 27 },
                    { 1203, "خوش رودپی", 200000, false, 27 },
                    { 1204, "مرزن آباد", 200000, false, 27 },
                    { 1205, "ساری", 200000, false, 27 },
                    { 1206, "رینه", 200000, false, 27 },
                    { 1207, "سرخرود", 200000, false, 27 },
                    { 1208, "خرم آباد", 200000, false, 27 },
                    { 1209, "کجور", 200000, false, 27 },
                    { 1210, "رستمکلا", 200000, false, 27 },
                    { 1211, "سورک", 200000, false, 27 },
                    { 1212, "چمستان", 200000, false, 27 },
                    { 1213, "آستانه", 200000, false, 28 },
                    { 1214, "خنجین", 200000, false, 28 },
                    { 1215, "نراق", 200000, false, 28 },
                    { 1216, "کمیجان", 200000, false, 28 },
                    { 1217, "آشتیان", 200000, false, 28 },
                    { 1218, "رازقان", 200000, false, 28 },
                    { 1219, "مهاجران", 200000, false, 28 },
                    { 1220, "غرق آباد", 200000, false, 28 },
                    { 1221, "خنداب", 200000, false, 28 },
                    { 1222, "قورچی باشی", 200000, false, 28 },
                    { 1223, "خشکرود", 200000, false, 28 },
                    { 1224, "ساروق", 200000, false, 28 },
                    { 1225, "محلات", 200000, false, 28 },
                    { 1226, "شازند", 200000, false, 28 },
                    { 1227, "ساوه", 200000, false, 28 },
                    { 1228, "میلاجرد", 200000, false, 28 },
                    { 1229, "تفرش", 200000, false, 28 },
                    { 1230, "زاویه", 200000, false, 28 },
                    { 1231, "اراک", 200000, false, 28 },
                    { 1232, "توره", 200000, false, 28 },
                    { 1233, "نوبران", 200000, false, 28 },
                    { 1234, "فرمهین", 200000, false, 28 },
                    { 1235, "دلیجان", 200000, false, 28 },
                    { 1236, "پرندک", 200000, false, 28 },
                    { 1237, "کارچان", 200000, false, 28 },
                    { 1238, "نیمور", 200000, false, 28 },
                    { 1239, "هندودر", 200000, false, 28 },
                    { 1240, "آوه", 200000, false, 28 },
                    { 1241, "جاورسیان", 200000, false, 28 },
                    { 1242, "خمین", 200000, false, 28 },
                    { 1243, "مامونیه", 200000, false, 28 },
                    { 1244, "داودآباد", 200000, false, 28 },
                    { 1245, "شهباز", 200000, false, 28 },
                    { 1246, "بیکاء", 200000, false, 29 },
                    { 1247, "تیرور", 200000, false, 29 },
                    { 1248, "گروک", 200000, false, 29 },
                    { 1249, "قشم", 200000, false, 29 },
                    { 1250, "کوشکنار", 200000, false, 29 },
                    { 1251, "کیش", 200000, false, 29 },
                    { 1252, "سرگز", 200000, false, 29 },
                    { 1253, "بندرعباس", 200000, false, 29 },
                    { 1254, "زیارتعلی", 200000, false, 29 },
                    { 1255, "سندرک", 200000, false, 29 },
                    { 1256, "کوهستک", 200000, false, 29 },
                    { 1257, "لمزان", 200000, false, 29 },
                    { 1258, "رویدر", 200000, false, 29 },
                    { 1259, "قلعه قاضی", 200000, false, 29 },
                    { 1260, "فارغان", 200000, false, 29 },
                    { 1261, "ابوموسی", 200000, false, 29 },
                    { 1262, "هشتبندی", 200000, false, 29 },
                    { 1263, "سردشت", 200000, false, 29 },
                    { 1264, "درگهان", 200000, false, 29 },
                    { 1265, "پارسیان", 200000, false, 29 },
                    { 1266, "کنگ", 200000, false, 29 },
                    { 1267, "جناح", 200000, false, 29 },
                    { 1268, "تازیان پایین", 200000, false, 29 },
                    { 1269, "دهبازر", 200000, false, 29 },
                    { 1270, "میناب", 200000, false, 29 },
                    { 1271, "سیریک", 200000, false, 29 },
                    { 1272, "سوزا", 200000, false, 29 },
                    { 1273, "خمیر", 200000, false, 29 },
                    { 1274, "چارک", 200000, false, 29 },
                    { 1275, "حاجی آباد", 200000, false, 29 },
                    { 1276, "فین", 200000, false, 29 },
                    { 1277, "بندر جاسک", 200000, false, 29 },
                    { 1278, "گوهران", 200000, false, 29 },
                    { 1279, "هرمز", 200000, false, 29 },
                    { 1280, "دشتی", 200000, false, 29 },
                    { 1281, "بندر لنگه", 200000, false, 29 },
                    { 1282, "بستک", 200000, false, 29 },
                    { 1283, "تخت", 200000, false, 29 },
                    { 1284, "زنگنه", 200000, false, 30 },
                    { 1285, "دمق", 200000, false, 30 },
                    { 1286, "سرکان", 200000, false, 30 },
                    { 1287, "آجین", 200000, false, 30 },
                    { 1288, "جورقان", 200000, false, 30 },
                    { 1289, "برزول", 200000, false, 30 },
                    { 1290, "فامنین", 200000, false, 30 },
                    { 1291, "سامن", 200000, false, 30 },
                    { 1292, "بهار", 200000, false, 30 },
                    { 1293, "فرسنج", 200000, false, 30 },
                    { 1294, "شیرین سو", 200000, false, 30 },
                    { 1295, "مریانج", 200000, false, 30 },
                    { 1296, "فیروزان", 200000, false, 30 },
                    { 1297, "قروه درجزین", 200000, false, 30 },
                    { 1298, "ازندریان", 200000, false, 30 },
                    { 1299, "لالجین", 200000, false, 30 },
                    { 1300, "گل تپه", 200000, false, 30 },
                    { 1301, "گیان", 200000, false, 30 },
                    { 1302, "ملایر", 200000, false, 30 },
                    { 1303, "صالح آباد", 200000, false, 30 },
                    { 1304, "تویسرکان", 200000, false, 30 },
                    { 1305, "اسدآباد", 200000, false, 30 },
                    { 1306, "همدان", 200000, false, 30 },
                    { 1307, "نهاوند", 200000, false, 30 },
                    { 1308, "رزن", 200000, false, 30 },
                    { 1309, "جوکار", 200000, false, 30 },
                    { 1310, "مهاجران", 200000, false, 30 },
                    { 1311, "کبودرآهنگ", 200000, false, 30 },
                    { 1312, "قهاوند", 200000, false, 30 },
                    { 1313, "مرودست", 200000, false, 31 },
                    { 1314, "مهردشت", 200000, false, 31 },
                    { 1315, "حمیدیا", 200000, false, 31 },
                    { 1316, "تفت", 200000, false, 31 },
                    { 1317, "اشکذر", 200000, false, 31 },
                    { 1318, "ندوشن", 200000, false, 31 },
                    { 1319, "یزد", 200000, false, 31 },
                    { 1320, "عقدا", 200000, false, 31 },
                    { 1321, "بهاباد", 200000, false, 31 },
                    { 1322, "ابرکوه", 200000, false, 31 },
                    { 1323, "زارچ", 200000, false, 31 },
                    { 1324, "نیر", 200000, false, 31 },
                    { 1325, "اردکان", 200000, false, 31 },
                    { 1326, "هرات", 200000, false, 31 },
                    { 1327, "بفروییه", 200000, false, 31 },
                    { 1328, "شاهدیه", 200000, false, 31 },
                    { 1329, "بافق", 200000, false, 31 },
                    { 1330, "خضرآباد", 200000, false, 31 },
                    { 1331, "میبد", 200000, false, 31 },
                    { 1332, "مهریز", 200000, false, 31 },
                    { 1333, "احمدآباد", 200000, false, 31 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 2, 1, "upload", "آپلود" },
                    { 6, 1, "roles", "نقش ها" },
                    { 13, 1, "financial", "مالی" },
                    { 16, 1, "insus", "بیمه ها" },
                    { 150, 1, "weblog", "وبلاگ" },
                    { 159, 1, "addinfos", "اطلاعات تکمیلی" },
                    { 190, 1, "reports", "گزارشات" },
                    { 203, 1, "registerdreqs", "درخواستهای ثبت شده" },
                    { 221, 220, "mydetregreq", "جزئیات" },
                    { 222, 220, "myeditregreq", "ویرایش" },
                    { 223, 220, "myaddissuestreq", "ثبت وضعیت صدور" },
                    { 224, 220, "myaddfinstreq", "ثبت وضعیت مالی" },
                    { 225, 220, "myaddissstcomreq", "ثبت یادداشت وضعیت صدور" },
                    { 226, 220, "myeditissstcomreq", "ویرایش یادداشت وضعیت صدور" },
                    { 227, 220, "mydeleteissstcomreq", "حذف یادداشت وضعیت صدور" },
                    { 228, 220, "myaddfinstcomreq", "ثبت یادداشت وضعیت مالی" },
                    { 229, 220, "myeditfinstcomreq", "ویرایش یادداشت وضعیت مالی" },
                    { 230, 220, "mydeletefinstcomreq", "حذف یادداشت وضعیت مالی" },
                    { 231, 220, "myattfilereq", "پیوست فایل" },
                    { 232, 220, "mydwonloadatfilesreq", "دانلود تمام فایلهای پیوست شده" },
                    { 233, 220, "mydownloadatfilereq", "دانلود فایل پیوست شده" },
                    { 234, 220, "mydeleteatfilereq", "حذف فایل پیوست شده" },
                    { 235, 220, "mydownloaddocsreq", "دانلود مدارک" },
                    { 236, 220, "mypayactionreq", "عملیات پرداخت" },
                    { 238, 237, "mydetregins", "جزئیات" },
                    { 239, 237, "myeditregins", "ویرایش" },
                    { 240, 237, "myaddissuestins", "ثبت وضعیت صدور" },
                    { 241, 237, "myaddfinstins", "ثبت وضعیت مالی" },
                    { 242, 237, "myaddissstcomins", "ثبت یادداشت وضعیت صدور" },
                    { 243, 237, "myeditissstcomins", "ویرایش یادداشت وضعیت صدور" },
                    { 244, 237, "mydeleteissstcomins", "حذف یادداشت وضعیت صدور" },
                    { 245, 237, "myaddfinstcomins", "ثبت یادداشت وضعیت مالی" },
                    { 246, 237, "myeditfinstcomins", "ویرایش یادداشت وضعیت مالی" },
                    { 247, 237, "mydeletefinstcomins", "حذف یادداشت وضعیت مالی" },
                    { 248, 237, "myattfileins", "پیوست فایل" },
                    { 249, 237, "mydwonloadatfilesins", "دانلود تمام فایلهای پیوست شده" },
                    { 250, 237, "mydownloadatfileins", "دانلود فایل پیوست شده" },
                    { 251, 237, "mydeleteatfileins", "حذف فایل پیوست شده" },
                    { 252, 237, "mydownloaddocsins", "دانلود مدارک" },
                    { 253, 237, "mypayactionins", "عملیات پرداخت" },
                    { 255, 254, "detregmyins", "جزئیات" },
                    { 256, 254, "editregmyins", "ویرایش" },
                    { 257, 254, "addissuestmyins", "ثبت وضعیت صدور" },
                    { 258, 254, "addfinstmyins", "ثبت وضعیت مالی" },
                    { 259, 254, "addissstcommyins", "ثبت یادداشت وضعیت صدور" },
                    { 260, 254, "editissstcommyins", "ویرایش یادداشت وضعیت صدور" },
                    { 261, 254, "deleteissstcommyins", "حذف یادداشت وضعیت صدور" },
                    { 262, 254, "addfinstcommyins", "ثبت یادداشت وضعیت مالی" },
                    { 263, 254, "editfinstcommyins", "ویرایش یادداشت وضعیت مالی" },
                    { 264, 254, "deletefinstcommyins", "حذف یادداشت وضعیت مالی" },
                    { 265, 254, "attfilemyins", "پیوست فایل" },
                    { 266, 254, "dwonloadatfilesmyins", "دانلود تمام فایلهای پیوست شده" },
                    { 267, 254, "downloadatfilemyins", "دانلود فایل پیوست شده" },
                    { 268, 254, "deleteatfilemyins", "حذف فایل پیوست شده" },
                    { 269, 254, "downloaddocsmyins", "دانلود مدارک" },
                    { 270, 254, "payactionmyins", "عملیات پرداخت" },
                    { 272, 271, "mycommisisons", "کارمزد فروش" },
                    { 273, 271, "collectionreport", "کارمزد وصول" },
                    { 275, 274, "addconv", "افزودن پیام" },
                    { 276, 274, "detconv", "جزئیات پیام" },
                    { 277, 274, "delconv", "حذف پیام" },
                    { 288, 1, "registerdinss", "بیمه های صادر شده" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 220, 220, 1 },
                    { 237, 237, 1 },
                    { 254, 254, 1 },
                    { 271, 271, 1 },
                    { 274, 274, 1 },
                    { 305, 305, 1 },
                    { 306, 306, 1 }
                });

            migrationBuilder.InsertData(
                table: "VehicleGroups",
                columns: new[] { "Id", "DelayedPenalty", "FinancialPremium", "GroupPremium", "GroupTitle", "ImmunityEndDate", "ImmunityStartDate", "ParentId", "VehicleConstructionYearLimit" },
                values: new object[,]
                {
                    { 3, 24700, 50000, 19375000, "کمتر از 4 سیلندر", null, null, 1, 1388 },
                    { 4, 24700, 50000, 22973000, "پیکان، پراید، سپند", null, null, 1, 1388 },
                    { 5, 24700, 50000, 26971000, "سایر 4 سیلندرها", null, null, 1, 1388 }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 3, 2, "uploadcenter", "آپلود سنتر" },
                    { 4, 2, "ckuploadedfiles", "فایلهای آپلودی CK" },
                    { 5, 2, "uploadinsfile", "آپلود فایل بیمه" },
                    { 7, 6, "roleslist", "لیست نقش ها" },
                    { 8, 6, "addrole", "افزودن نقش" },
                    { 9, 6, "editrole", "ویرایش نقش" },
                    { 10, 6, "deleterole", "حذف نقش" },
                    { 11, 6, "detrole", "جزئیات نقش" },
                    { 12, 6, "roleper", "دسترسی" },
                    { 14, 13, "calcom", "محاسبه کارمزد" },
                    { 15, 13, "dfile", "دانلود فایل" },
                    { 17, 16, "issuestate", "وضعیت صدور" },
                    { 21, 16, "financialstate", "وضعیت مالی" },
                    { 25, 16, "tpins", "بیمه ثالث" },
                    { 70, 16, "lifeins", "بیمه زندگی" },
                    { 80, 16, "fireins", "بیمه آتش سوزی" },
                    { 109, 16, "cbins", "بیمه بدنه" },
                    { 137, 16, "travelins", "بیمه مسافرتی" },
                    { 151, 150, "webloggroups", "گروه های وبلاگ" },
                    { 155, 150, "blogs", "بلاگ ها" },
                    { 160, 159, "abouts", "درباره ما" },
                    { 164, 159, "usermessages", "پیامهای کاربران" },
                    { 166, 159, "sliders", "اسلایدر" },
                    { 170, 159, "specialoffer", "پیشنهاد ویژه" },
                    { 174, 159, "userhelpinfo", "راهنمای کاربران" },
                    { 178, 159, "faqs", "پرسش و پاسخ" },
                    { 182, 159, "websiteupdates", "بروزرسانی ها" },
                    { 186, 159, "workwiths", "همکاری با ما" },
                    { 191, 190, "repusers", "کاربران" },
                    { 197, 190, "reqsworkwith", "درخواست های همکاری" },
                    { 199, 190, "coworkers", "همکاران" },
                    { 204, 203, "detregreq", "جزئیات" },
                    { 205, 203, "editregreq", "ویرایش" },
                    { 206, 203, "addissuestreq", "ثبت وضعیت صدور" },
                    { 207, 203, "addfinstreq", "ثبت وضعیت مالی" },
                    { 208, 203, "addissstcomreq", "ثبت یادداشت وضعیت صدور" },
                    { 209, 203, "editissstcomreq", "ویرایش یادداشت وضعیت صدور" },
                    { 210, 203, "deleteissstcomreq", "حذف یادداشت وضعیت صدور" },
                    { 211, 203, "addfinstcomreq", "ثبت یادداشت وضعیت مالی" },
                    { 212, 203, "editfinstcomreq", "ویرایش یادداشت وضعیت مالی" },
                    { 213, 203, "deletefinstcomreq", "حذف یادداشت وضعیت مالی" },
                    { 214, 203, "attfilereq", "پیوست فایل" },
                    { 215, 203, "dwonloadatfilesreq", "دانلود تمام فایلهای پیوست شده" },
                    { 216, 203, "downloadatfilereq", "دانلود فایل پیوست شده" },
                    { 217, 203, "deleteatfilereq", "حذف فایل پیوست شده" },
                    { 218, 203, "downloaddocsreq", "دانلود مدارک" },
                    { 219, 203, "payactionreq", "عملیات پرداخت" },
                    { 281, 159, "counties", "شهرستانها" },
                    { 289, 288, "detregins", "جزئیات" },
                    { 290, 288, "editregins", "ویرایش" },
                    { 291, 288, "addissuestins", "ثبت وضعیت صدور" },
                    { 292, 288, "addfinstins", "ثبت وضعیت مالی" },
                    { 293, 288, "addissstcomins", "ثبت یادداشت وضعیت صدور" },
                    { 294, 288, "editissstcomins", "ویرایش یادداشت وضعیت صدور" },
                    { 295, 288, "deleteissstcomins", "حذف یادداشت وضعیت صدور" },
                    { 296, 288, "addfinstcomins", "ثبت یادداشت وضعیت مالی" },
                    { 297, 288, "editfinstcomins", "ویرایش یادداشت وضعیت مالی" },
                    { 298, 288, "deletefinstcomins", "حذف یادداشت وضعیت مالی" },
                    { 299, 288, "attfileins", "پیوست فایل" },
                    { 300, 288, "dwonloadatfilesins", "دانلود تمام فایلهای پیوست شده" },
                    { 301, 288, "downloadatfileins", "دانلود فایل پیوست شده" },
                    { 302, 288, "deleteatfileins", "حذف فایل پیوست شده" },
                    { 303, 288, "downloaddocsins", "دانلود مدارک" },
                    { 304, 288, "payactionins", "عملیات پرداخت" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 6, 6, 1 },
                    { 13, 13, 1 },
                    { 16, 16, 1 },
                    { 150, 150, 1 },
                    { 159, 159, 1 },
                    { 190, 190, 1 },
                    { 203, 203, 1 },
                    { 221, 221, 1 },
                    { 222, 222, 1 },
                    { 223, 223, 1 },
                    { 224, 224, 1 },
                    { 225, 225, 1 },
                    { 226, 226, 1 },
                    { 227, 227, 1 },
                    { 228, 228, 1 },
                    { 229, 229, 1 },
                    { 230, 230, 1 },
                    { 231, 231, 1 },
                    { 232, 232, 1 },
                    { 233, 233, 1 },
                    { 234, 234, 1 },
                    { 235, 235, 1 },
                    { 236, 236, 1 },
                    { 238, 238, 1 },
                    { 239, 239, 1 },
                    { 240, 240, 1 },
                    { 241, 241, 1 },
                    { 242, 242, 1 },
                    { 243, 243, 1 },
                    { 244, 244, 1 },
                    { 245, 245, 1 },
                    { 246, 246, 1 },
                    { 247, 247, 1 },
                    { 248, 248, 1 },
                    { 249, 249, 1 },
                    { 250, 250, 1 },
                    { 251, 251, 1 },
                    { 252, 252, 1 },
                    { 253, 253, 1 },
                    { 255, 255, 1 },
                    { 256, 256, 1 },
                    { 257, 257, 1 },
                    { 258, 258, 1 },
                    { 259, 259, 1 },
                    { 260, 260, 1 },
                    { 261, 261, 1 },
                    { 262, 262, 1 },
                    { 263, 263, 1 },
                    { 264, 264, 1 },
                    { 265, 265, 1 },
                    { 266, 266, 1 },
                    { 267, 267, 1 },
                    { 268, 268, 1 },
                    { 269, 269, 1 },
                    { 270, 270, 1 },
                    { 272, 272, 1 },
                    { 273, 273, 1 },
                    { 275, 275, 1 },
                    { 276, 276, 1 },
                    { 277, 277, 1 },
                    { 288, 288, 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "AgentCode", "AgentRequestImage", "BirthDate", "Cellphone", "ConfirmCode", "ConfirmedCellphone", "CountyId", "CriminalRecord", "DemandBackImage", "DemandFrontImage", "DemandNo", "DemandValue", "EducationDegreeImage", "Email", "EndofServiceImage", "Exam96Image", "Family", "Father", "FieldofStudy", "GraduationDate", "IdNumber", "InsWokHistory", "IsActive", "IssuePlace", "LevelofStudy", "NC", "NCImage", "Name", "NoAddictionImage", "Password", "PersonalImage", "Phone", "PortalIsActive", "PortalPassword", "PostalCode", "ReferralCode", "RegisteredDate", "SHEBAImage", "SalesExCode", "Sex", "ShebaNumber", "SignImage", "UniversityName", "UserBankAccountNumber", "UserCreditCardNumber" },
                values: new object[,]
                {
                    { 1, null, "3312", null, null, "09123689294", null, false, 330, null, null, null, null, null, null, null, null, null, "فروش", null, null, null, null, null, true, null, null, "0000000000", null, "سرپرستی", null, "farbod-1356", null, "02128428533", false, null, null, "3312", new DateTime(2022, 11, 15, 11, 8, 39, 810, DateTimeKind.Utc).AddTicks(8370), null, "3312", "مرد", null, null, null, null, null },
                    { 2, null, "919919", null, null, "09199190919", null, false, 330, null, null, null, null, null, null, null, null, null, "agent", null, null, null, null, null, true, null, null, null, null, "system", null, "system-agent", null, "02128428533", false, null, null, "919919", new DateTime(2022, 11, 15, 11, 8, 39, 810, DateTimeKind.Utc).AddTicks(8386), null, "919919", "مرد", null, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 18, 17, "addissuestate", "ثبت وضعیت صدور" },
                    { 19, 17, "deleteissuestate", "حذف وضعیت صدور" },
                    { 20, 17, "editissuestate", "ویرایش وضعیت صدور" },
                    { 22, 21, "addfstate", "ثبت وضعیت مالی" },
                    { 23, 21, "deletefstate", "حذف وضعیت مالی" },
                    { 24, 21, "editfstate", "ویرایش وضعیت مالی" },
                    { 26, 25, "tpbaseinfo", "اطلاعات پایه" },
                    { 30, 25, "financialdamges", "جریمه خسارت مالی" },
                    { 34, 25, "financialpremiums", "پوشش مالی" },
                    { 38, 25, "incidentcovers", "پوششهای حوادث" },
                    { 42, 25, "legaldiscounts", "تخفیفات و اضافات قانونی" },
                    { 46, 25, "looslifedamges", "جرایم خسارت جانی" },
                    { 50, 25, "vehicleusages", "کاربری های خودرو" },
                    { 54, 25, "vehiclegroups", "گروه های وسیله نقلیه" },
                    { 62, 25, "loosdrivedamages", "جرایم خسارت حوادث راننده" },
                    { 66, 25, "tpinsurertypes", "انواع بیمه گذار" },
                    { 71, 70, "paymentmethods", "روشهای پرداخت" },
                    { 75, 70, "lifeplans", "طرح های بیمه" },
                    { 81, 80, "fbaseinfo", "اطلاعات پایه" },
                    { 85, 80, "buildingusage", "کاربری ملک" },
                    { 93, 80, "firecovers", "پوششهای بیمه آتش سوزی" },
                    { 97, 80, "fireinsurertypes", "انواع بیمه گذار" },
                    { 101, 80, "firelegaldiscount", "تخفیفات و اضافات" },
                    { 105, 80, "firestructuretypes", "نوع سازه" },
                    { 110, 109, "cbinsgroup", "گروه خودرو" },
                    { 116, 109, "cbusage", "کاربری خودرو" },
                    { 121, 109, "cblegdis", "تخفیف و اضافه نرخ" },
                    { 125, 109, "cbinsurertypes", "انواع بیمه گذار" },
                    { 129, 109, "cbinsurancetypes", "انواع بیمه نامه" },
                    { 133, 109, "cbcovers", "پوششهای بیمه بدنه" },
                    { 138, 137, "tinsco", "بیمه گر" },
                    { 142, 137, "travelclass", "کلاس بیمه نامه" },
                    { 146, 137, "travelzooms", "مناطق سفر" },
                    { 152, 151, "addwg", "ثبت" },
                    { 153, 151, "editwg", "ویرایش" },
                    { 154, 151, "deletewg", "حذف" },
                    { 156, 155, "addblg", "ثبت" },
                    { 157, 155, "editblg", "ویرایش" },
                    { 158, 155, "deleteblg", "حذف" },
                    { 161, 160, "addabout", "ثبت" },
                    { 162, 160, "editabout", "ویرایش" },
                    { 163, 160, "deleteabout", "حذف" },
                    { 165, 164, "umdet", "جزئیات" },
                    { 167, 166, "addslider", "ثبت" },
                    { 168, 166, "editslider", "ویرایش" },
                    { 169, 166, "deleteslider", "حذف" },
                    { 171, 170, "addspo", "ثبت" },
                    { 172, 170, "editspo", "ویرایش" },
                    { 173, 170, "deletespo", "حذف" },
                    { 175, 174, "adduhp", "ثبت" },
                    { 176, 174, "edituhp", "ویرایش" },
                    { 177, 174, "deleteuhp", "حذف" },
                    { 179, 178, "addfaq", "ثبت" },
                    { 180, 178, "editfaq", "ویرایش" },
                    { 181, 178, "deletefaq", "حذف" },
                    { 183, 182, "addwup", "ثبت" },
                    { 184, 182, "editwup", "ویرایش" },
                    { 185, 182, "deletewup", "حذف" },
                    { 187, 186, "addww", "ثبت" },
                    { 188, 186, "editww", "ویرایش" },
                    { 189, 186, "deleteww", "حذف" },
                    { 192, 191, "adduser", "ثبت" },
                    { 193, 191, "edituser", "ویرایش" },
                    { 194, 191, "deleteuser", "حذف" },
                    { 195, 191, "addroletouser", "افزودن نقش" },
                    { 196, 191, "detuser", "جزئیات" },
                    { 198, 197, "editrww", "ویرایش" },
                    { 200, 199, "addcwo", "ثبت" },
                    { 201, 199, "edicwo", "ویرایش" },
                    { 202, 199, "deletecwo", "حذف" },
                    { 282, 281, "addcou", "ثبت" },
                    { 283, 281, "editcou", "ویرایش" },
                    { 284, 281, "detcou", "جزئیات" },
                    { 285, 3, "addfileuc", "آپلود فایل" },
                    { 286, 3, "editfileuc", "ویرایش فایل" },
                    { 287, 3, "deletefileuc", "حذف فایل" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 },
                    { 12, 12, 1 },
                    { 14, 14, 1 },
                    { 15, 15, 1 },
                    { 17, 17, 1 },
                    { 21, 21, 1 },
                    { 25, 25, 1 },
                    { 70, 70, 1 },
                    { 80, 80, 1 },
                    { 109, 109, 1 },
                    { 137, 137, 1 },
                    { 151, 151, 1 },
                    { 155, 155, 1 },
                    { 160, 160, 1 },
                    { 164, 164, 1 },
                    { 166, 166, 1 },
                    { 170, 170, 1 },
                    { 174, 174, 1 },
                    { 178, 178, 1 },
                    { 182, 182, 1 },
                    { 186, 186, 1 },
                    { 191, 191, 1 },
                    { 197, 197, 1 },
                    { 199, 199, 1 },
                    { 204, 204, 1 },
                    { 205, 205, 1 },
                    { 206, 206, 1 },
                    { 207, 207, 1 },
                    { 208, 208, 1 },
                    { 209, 209, 1 },
                    { 210, 210, 1 },
                    { 211, 211, 1 },
                    { 212, 212, 1 },
                    { 213, 213, 1 },
                    { 214, 214, 1 },
                    { 215, 215, 1 },
                    { 216, 216, 1 },
                    { 217, 217, 1 },
                    { 218, 218, 1 },
                    { 219, 219, 1 },
                    { 281, 281, 1 },
                    { 289, 289, 1 },
                    { 290, 290, 1 },
                    { 291, 291, 1 },
                    { 292, 292, 1 },
                    { 293, 293, 1 },
                    { 294, 294, 1 },
                    { 295, 295, 1 },
                    { 296, 296, 1 },
                    { 297, 297, 1 },
                    { 298, 298, 1 },
                    { 299, 299, 1 },
                    { 300, 300, 1 },
                    { 301, 301, 1 },
                    { 302, 302, 1 },
                    { 303, 303, 1 },
                    { 304, 304, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "URId", "CarBodyPercent", "FirePercent", "IsActive", "IsDeleted", "LiabilityPercent", "LifePercent", "RegisterDate", "RoleId", "ThirdPartyPercent", "TravelPercent", "UserId" },
                values: new object[] { 1, 0f, 0f, true, false, 0f, 0f, new DateTime(2022, 11, 15, 11, 8, 39, 810, DateTimeKind.Local).AddTicks(8409), 1, 0f, 0f, 1 });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "ParentId", "PermissionName", "PermissionTitle" },
                values: new object[,]
                {
                    { 27, 26, "addtbi", "ثبت" },
                    { 28, 26, "edittbi", "ویرایش" },
                    { 29, 26, "deletetbi", "حــذف" },
                    { 31, 30, "addfd", "ثبت" },
                    { 32, 30, "editfd", "ویرایش" },
                    { 33, 30, "deletefd", "حــذف" },
                    { 35, 34, "addfp", "ثبت" },
                    { 36, 34, "editfp", "ویرایش" },
                    { 37, 34, "deletefp", "حــذف" },
                    { 39, 38, "addic", "ثبت" },
                    { 40, 38, "editic", "ویرایش" },
                    { 41, 38, "deleteic", "حــذف" },
                    { 43, 42, "addld", "ثبت" },
                    { 44, 42, "editld", "ویرایش" },
                    { 45, 42, "deleteld", "حــذف" },
                    { 47, 46, "addlld", "ثبت" },
                    { 48, 46, "editlld", "ویرایش" },
                    { 49, 46, "deletelld", "حــذف" },
                    { 51, 50, "addlvu", "ثبت" },
                    { 52, 50, "editlvu", "ویرایش" },
                    { 53, 50, "deletelvu", "حــذف" },
                    { 55, 54, "addvg", "ثبت" },
                    { 56, 54, "editvg", "ویرایش" },
                    { 57, 54, "deletevg", "حــذف" },
                    { 58, 54, "addveh", "ثبت وسیله نقلیه" },
                    { 59, 54, "editveh", "ویرایش وسیله نقلیه" },
                    { 60, 54, "deletelveh", "حذف وسیله نقلیه" },
                    { 61, 54, "manageusage", "مدیریت کاربری" },
                    { 63, 62, "addldrd", "ثبت" },
                    { 64, 62, "editldrd", "ویرایش" },
                    { 65, 62, "deleteldrd", "حــذف" },
                    { 67, 66, "addtpit", "ثبت" },
                    { 68, 66, "edittpit", "ویرایش" },
                    { 69, 66, "deletetpit", "حــذف" },
                    { 72, 71, "addlpm", "ثبت" },
                    { 73, 71, "editlpm", "ویرایش" },
                    { 74, 71, "deletelpm", "حذف" },
                    { 76, 75, "addllifep", "ثبت" },
                    { 77, 75, "editlifep", "ویرایش" },
                    { 78, 75, "deletelifep", "حذف" },
                    { 79, 75, "managepm", "مدیریت روشهای پرداخت" },
                    { 82, 81, "addfbi", "ثبت" },
                    { 83, 81, "editfbi", "ویرایش" },
                    { 84, 81, "deletefbi", "حذف" },
                    { 86, 85, "addbu", "ثبت" },
                    { 87, 85, "editbu", "ویرایش" },
                    { 88, 85, "deletebu", "حذف" },
                    { 89, 85, "managecovers", "مدیریت پوشش ها" },
                    { 90, 85, "staterates", "نرخهای استانی" },
                    { 91, 85, "addstr", "ثبت نرخ" },
                    { 92, 85, "deletestr", "حذف نرخ" },
                    { 94, 93, "addfico", "ثبت" },
                    { 95, 93, "editfico", "ویرایش" },
                    { 96, 93, "deletefico", "حذف" },
                    { 98, 97, "addfity", "ثبت" },
                    { 99, 97, "editfity", "ویرایش" },
                    { 100, 97, "deletefity", "حذف" },
                    { 102, 101, "addfld", "ثبت" },
                    { 103, 101, "editfld", "ویرایش" },
                    { 104, 101, "deletefld", "حذف" },
                    { 106, 105, "addfsty", "ثبت" },
                    { 107, 105, "editfsty", "ویرایش" },
                    { 108, 105, "deletefsty", "حذف" },
                    { 111, 110, "addcbg", "ثبت" },
                    { 112, 110, "editcbg", "ویرایش" },
                    { 113, 110, "deletecbg", "حذف" },
                    { 114, 110, "addcbcar", "ثبت خودرو" },
                    { 115, 110, "editcbcar", "ویرایش خودرو" },
                    { 117, 116, "addcbu", "ثبت" },
                    { 118, 116, "editcbu", "ویرایش" },
                    { 119, 116, "deletecbu", "حذف" },
                    { 120, 116, "selcbug", "انتخاب گروه" },
                    { 122, 121, "addcbld", "ثبت" },
                    { 123, 121, "editcbld", "ویرایش" },
                    { 124, 121, "deletecbld", "حذف" },
                    { 126, 125, "addcbit", "ثبت" },
                    { 127, 125, "editcbit", "ویرایش" },
                    { 128, 125, "deletecbit", "حذف" },
                    { 130, 129, "addcbinty", "ثبت" },
                    { 131, 129, "editcbinty", "ویرایش" },
                    { 132, 129, "deletecbinty", "حذف" },
                    { 134, 133, "addcbc", "ثبت" },
                    { 135, 133, "editcbc", "ویرایش" },
                    { 136, 133, "deletecbc", "حذف" },
                    { 139, 138, "addtico", "ثبت" },
                    { 140, 138, "edittico", "ویرایش" },
                    { 141, 138, "deletetico", "حذف" },
                    { 143, 142, "addtc", "ثبت" },
                    { 144, 142, "edittc", "ویرایش" },
                    { 145, 142, "deletetc", "حذف" },
                    { 147, 146, "addtz", "ثبت" },
                    { 148, 146, "edittz", "ویرایش" },
                    { 149, 146, "deletetz", "حذف" },
                    { 278, 110, "detcbcar", "جزئیات خودرو" },
                    { 279, 110, "deletecbcar", "حذف خودرو" },
                    { 280, 110, "viewcbcars", "مشاهده خودروها" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 18, 18, 1 },
                    { 19, 19, 1 },
                    { 20, 20, 1 },
                    { 22, 22, 1 },
                    { 23, 23, 1 },
                    { 24, 24, 1 },
                    { 26, 26, 1 },
                    { 30, 30, 1 },
                    { 34, 34, 1 },
                    { 38, 38, 1 },
                    { 42, 42, 1 },
                    { 46, 46, 1 },
                    { 50, 50, 1 },
                    { 54, 54, 1 },
                    { 62, 62, 1 },
                    { 66, 66, 1 },
                    { 71, 71, 1 },
                    { 75, 75, 1 },
                    { 81, 81, 1 },
                    { 85, 85, 1 },
                    { 93, 93, 1 },
                    { 97, 97, 1 },
                    { 101, 101, 1 },
                    { 105, 105, 1 },
                    { 110, 110, 1 },
                    { 116, 116, 1 },
                    { 121, 121, 1 },
                    { 125, 125, 1 },
                    { 129, 129, 1 },
                    { 133, 133, 1 },
                    { 138, 138, 1 },
                    { 142, 142, 1 },
                    { 146, 146, 1 },
                    { 152, 152, 1 },
                    { 153, 153, 1 },
                    { 154, 154, 1 },
                    { 156, 156, 1 },
                    { 157, 157, 1 },
                    { 158, 158, 1 },
                    { 161, 161, 1 },
                    { 162, 162, 1 },
                    { 163, 163, 1 },
                    { 165, 165, 1 },
                    { 167, 167, 1 },
                    { 168, 168, 1 },
                    { 169, 169, 1 },
                    { 171, 171, 1 },
                    { 172, 172, 1 },
                    { 173, 173, 1 },
                    { 175, 175, 1 },
                    { 176, 176, 1 },
                    { 177, 177, 1 },
                    { 179, 179, 1 },
                    { 180, 180, 1 },
                    { 181, 181, 1 },
                    { 183, 183, 1 },
                    { 184, 184, 1 },
                    { 185, 185, 1 },
                    { 187, 187, 1 },
                    { 188, 188, 1 },
                    { 189, 189, 1 },
                    { 192, 192, 1 },
                    { 193, 193, 1 },
                    { 194, 194, 1 },
                    { 195, 195, 1 },
                    { 196, 196, 1 },
                    { 198, 198, 1 },
                    { 200, 200, 1 },
                    { 201, 201, 1 },
                    { 202, 202, 1 },
                    { 282, 282, 1 },
                    { 283, 283, 1 },
                    { 284, 284, 1 },
                    { 285, 285, 1 },
                    { 286, 286, 1 },
                    { 287, 287, 1 },
                    { 27, 27, 1 },
                    { 28, 28, 1 },
                    { 29, 29, 1 },
                    { 31, 31, 1 },
                    { 32, 32, 1 },
                    { 33, 33, 1 },
                    { 35, 35, 1 },
                    { 36, 36, 1 },
                    { 37, 37, 1 },
                    { 39, 39, 1 },
                    { 40, 40, 1 },
                    { 41, 41, 1 },
                    { 43, 43, 1 },
                    { 44, 44, 1 },
                    { 45, 45, 1 },
                    { 47, 47, 1 },
                    { 48, 48, 1 },
                    { 49, 49, 1 },
                    { 51, 51, 1 },
                    { 52, 52, 1 },
                    { 53, 53, 1 },
                    { 55, 55, 1 },
                    { 56, 56, 1 },
                    { 57, 57, 1 },
                    { 58, 58, 1 },
                    { 59, 59, 1 },
                    { 60, 60, 1 },
                    { 61, 61, 1 },
                    { 63, 63, 1 },
                    { 64, 64, 1 },
                    { 65, 65, 1 },
                    { 67, 67, 1 },
                    { 68, 68, 1 },
                    { 69, 69, 1 },
                    { 72, 72, 1 },
                    { 73, 73, 1 },
                    { 74, 74, 1 },
                    { 76, 76, 1 },
                    { 77, 77, 1 },
                    { 78, 78, 1 },
                    { 79, 79, 1 },
                    { 82, 82, 1 },
                    { 83, 83, 1 },
                    { 84, 84, 1 },
                    { 86, 86, 1 },
                    { 87, 87, 1 },
                    { 88, 88, 1 },
                    { 89, 89, 1 },
                    { 90, 90, 1 },
                    { 91, 91, 1 },
                    { 92, 92, 1 },
                    { 94, 94, 1 },
                    { 95, 95, 1 },
                    { 96, 96, 1 },
                    { 98, 98, 1 },
                    { 99, 99, 1 },
                    { 100, 100, 1 },
                    { 102, 102, 1 },
                    { 103, 103, 1 },
                    { 104, 104, 1 },
                    { 106, 106, 1 },
                    { 107, 107, 1 },
                    { 108, 108, 1 },
                    { 111, 111, 1 },
                    { 112, 112, 1 },
                    { 113, 113, 1 },
                    { 114, 114, 1 },
                    { 115, 115, 1 },
                    { 117, 117, 1 },
                    { 118, 118, 1 },
                    { 119, 119, 1 },
                    { 120, 120, 1 },
                    { 122, 122, 1 },
                    { 123, 123, 1 },
                    { 124, 124, 1 },
                    { 126, 126, 1 },
                    { 127, 127, 1 },
                    { 128, 128, 1 },
                    { 130, 130, 1 },
                    { 131, 131, 1 },
                    { 132, 132, 1 },
                    { 134, 134, 1 },
                    { 135, 135, 1 },
                    { 136, 136, 1 },
                    { 139, 139, 1 },
                    { 140, 140, 1 },
                    { 141, 141, 1 },
                    { 143, 143, 1 },
                    { 144, 144, 1 },
                    { 145, 145, 1 },
                    { 147, 147, 1 },
                    { 148, 148, 1 },
                    { 149, 149, 1 },
                    { 278, 278, 1 },
                    { 279, 279, 1 },
                    { 280, 280, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicThirdPartyPremiums_VGId",
                table: "BasicThirdPartyPremiums",
                column: "VGId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogGroupId",
                table: "Blogs",
                column: "BlogGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingUsageFireCoverages_BuildingUsageId",
                table: "BuildingUsageFireCoverages",
                column: "BuildingUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingUsageFireCoverages_FireInsCoverageId",
                table: "BuildingUsageFireCoverages",
                column: "FireInsCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyCars_CarBodyCarGroupId",
                table: "CarBodyCars",
                column: "CarBodyCarGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyCovers_ParentId",
                table: "CarBodyCovers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyGroupUsages_CarBodyGroupId",
                table: "CarBodyGroupUsages",
                column: "CarBodyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyGroupUsages_CarBodyUsageId",
                table: "CarBodyGroupUsages",
                column: "CarBodyUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyInsuranceFinancialStatuses_CarBodyInsuranceId",
                table: "CarBodyInsuranceFinancialStatuses",
                column: "CarBodyInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyInsuranceFinancialStatuses_FinancialStatusId",
                table: "CarBodyInsuranceFinancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyInsuranceStatuses_CarBodyInsuranceId",
                table: "CarBodyInsuranceStatuses",
                column: "CarBodyInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyInsuranceStatuses_InsStatusId",
                table: "CarBodyInsuranceStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyStatusComments_CarBodyFinancialStatusId",
                table: "CarBodyStatusComments",
                column: "CarBodyFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyStatusComments_CarBodyStatusId",
                table: "CarBodyStatusComments",
                column: "CarBodyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBodySupplements_CarBodyInsuranceId",
                table: "CarBodySupplements",
                column: "CarBodyInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionCommissionDetails_CollectionCommissionBaseId",
                table: "CollectionCommissionDetails",
                column: "CollectionCommissionBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ParentId",
                table: "Conversations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cooperations_CountyId",
                table: "Cooperations",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_StateId",
                table: "Counties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsStateRates_BuildingUsageFireCoverageId",
                table: "FireInsStateRates",
                column: "BuildingUsageFireCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsStateRates_StateId",
                table: "FireInsStateRates",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceFinancialStatuses_FinancialStatusId",
                table: "FireInsuranceFinancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceFinancialStatuses_FireInsuranceId",
                table: "FireInsuranceFinancialStatuses",
                column: "FireInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceStatusComments_FireInsuranceFinancialStatusId",
                table: "FireInsuranceStatusComments",
                column: "FireInsuranceFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceStatusComments_FireInsuranceStatusId",
                table: "FireInsuranceStatusComments",
                column: "FireInsuranceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceStatuses_FireInsuranceId",
                table: "FireInsuranceStatuses",
                column: "FireInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceStatuses_InsStatusId",
                table: "FireInsuranceStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FireInsuranceSupplements_FireInsuranceId",
                table: "FireInsuranceSupplements",
                column: "FireInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityFinancialStatuses_FinancialStatusId",
                table: "LiabilityFinancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityFinancialStatuses_LiabilityInsuranceId",
                table: "LiabilityFinancialStatuses",
                column: "LiabilityInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityStatusComments_LiabilityFinancialStatusId",
                table: "LiabilityStatusComments",
                column: "LiabilityFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityStatusComments_LiabilityStatusId",
                table: "LiabilityStatusComments",
                column: "LiabilityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityStatuses_InsStatusId",
                table: "LiabilityStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilityStatuses_LiabilityInsuranceId",
                table: "LiabilityStatuses",
                column: "LiabilityInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LiabilitySupplements_LiabilityInsuranceId",
                table: "LiabilitySupplements",
                column: "LiabilityInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceFinancialStatuses_FinancialStatusId",
                table: "LifeInsuranceFinancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceFinancialStatuses_LifeInsuranceId",
                table: "LifeInsuranceFinancialStatuses",
                column: "LifeInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsurances_PaymentMethodId",
                table: "LifeInsurances",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsurances_PlanId",
                table: "LifeInsurances",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceStatusComments_LifeInsuranceFinancialStatusId",
                table: "LifeInsuranceStatusComments",
                column: "LifeInsuranceFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceStatusComments_LifeInsuranceStatusId",
                table: "LifeInsuranceStatusComments",
                column: "LifeInsuranceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceStatuses_InsStatusId",
                table: "LifeInsuranceStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceStatuses_LifeInsuranceId",
                table: "LifeInsuranceStatuses",
                column: "LifeInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeInsuranceSupplements_LifeInsuranceId",
                table: "LifeInsuranceSupplements",
                column: "LifeInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanPaymentMethods_PaymentId",
                table: "PlanPaymentMethods",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanPaymentMethods_PlanId",
                table: "PlanPaymentMethods",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyFainancialStatuses_FinancialStatusId",
                table: "ThirdPartyFainancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyFainancialStatuses_ThirdPartyId",
                table: "ThirdPartyFainancialStatuses",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyStatusComments_ThirdPartyFinancialStatusId",
                table: "ThirdPartyStatusComments",
                column: "ThirdPartyFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyStatusComments_ThirdPartyStatusId",
                table: "ThirdPartyStatusComments",
                column: "ThirdPartyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyStatuses_InsStatusId",
                table: "ThirdPartyStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartyStatuses_ThirdPartyId",
                table: "ThirdPartyStatuses",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ThirdPartySupplements_ThirdPartyId",
                table: "ThirdPartySupplements",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelClassZooms_ClassId",
                table: "TravelClassZooms",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelClassZooms_ZoomId",
                table: "TravelClassZooms",
                column: "ZoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelFinancialStatuses_FinancialStatusId",
                table: "TravelFinancialStatuses",
                column: "FinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelFinancialStatuses_TravelInsuranceId",
                table: "TravelFinancialStatuses",
                column: "TravelInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelInsurances_TravelZoom",
                table: "TravelInsurances",
                column: "TravelZoom");

            migrationBuilder.CreateIndex(
                name: "IX_TravelStatusComments_TravelFinancialStatusId",
                table: "TravelStatusComments",
                column: "TravelFinancialStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelStatusComments_TravelStatusId",
                table: "TravelStatusComments",
                column: "TravelStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelStatuses_InsStatusId",
                table: "TravelStatuses",
                column: "InsStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelStatuses_TravelInsuranceId",
                table: "TravelStatuses",
                column: "TravelInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelSupplements_TravelInsuranceId",
                table: "TravelSupplements",
                column: "TravelInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountyId",
                table: "Users",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleGroups_ParentId",
                table: "VehicleGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleGroupUsages_GroupId",
                table: "VehicleGroupUsages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleGroupUsages_UsageId",
                table: "VehicleGroupUsages",
                column: "UsageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "AdminHelpInfos");

            migrationBuilder.DropTable(
                name: "AdminSliders");

            migrationBuilder.DropTable(
                name: "AdminSpecialOffers");

            migrationBuilder.DropTable(
                name: "BasicThirdPartyPremiums");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "CarBodyCars");

            migrationBuilder.DropTable(
                name: "CarBodyCovers");

            migrationBuilder.DropTable(
                name: "CarBodyGroupUsages");

            migrationBuilder.DropTable(
                name: "CarBodyInsuranceTypes");

            migrationBuilder.DropTable(
                name: "CarBodyInsurerTypes");

            migrationBuilder.DropTable(
                name: "CarBodyLegalDiscounts");

            migrationBuilder.DropTable(
                name: "CarBodyStatusComments");

            migrationBuilder.DropTable(
                name: "CarBodySupplements");

            migrationBuilder.DropTable(
                name: "CollectionCommissionDetails");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Cooperations");

            migrationBuilder.DropTable(
                name: "CoWorkers");

            migrationBuilder.DropTable(
                name: "EmailBanks");

            migrationBuilder.DropTable(
                name: "FinancialDamages");

            migrationBuilder.DropTable(
                name: "FinancialPremiums");

            migrationBuilder.DropTable(
                name: "FireBaseInfos");

            migrationBuilder.DropTable(
                name: "FireInsStateRates");

            migrationBuilder.DropTable(
                name: "FireInsuranceStatusComments");

            migrationBuilder.DropTable(
                name: "FireInsuranceSupplements");

            migrationBuilder.DropTable(
                name: "FireInsurerTypes");

            migrationBuilder.DropTable(
                name: "FireLegalDiscounts");

            migrationBuilder.DropTable(
                name: "FireStructureTypes");

            migrationBuilder.DropTable(
                name: "IncidentCovers");

            migrationBuilder.DropTable(
                name: "Instagrams");

            migrationBuilder.DropTable(
                name: "InsurerTypes");

            migrationBuilder.DropTable(
                name: "LegalDiscounts");

            migrationBuilder.DropTable(
                name: "LiabilityStatusComments");

            migrationBuilder.DropTable(
                name: "LiabilitySupplements");

            migrationBuilder.DropTable(
                name: "LifeInsuranceStatusComments");

            migrationBuilder.DropTable(
                name: "LifeInsuranceSupplements");

            migrationBuilder.DropTable(
                name: "LoosDriverDamages");

            migrationBuilder.DropTable(
                name: "LoosLifeDamages");

            migrationBuilder.DropTable(
                name: "PlanPaymentMethods");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "ThirdPartyBaseDatas");

            migrationBuilder.DropTable(
                name: "ThirdPartyStatusComments");

            migrationBuilder.DropTable(
                name: "ThirdPartySupplements");

            migrationBuilder.DropTable(
                name: "TravelClassZooms");

            migrationBuilder.DropTable(
                name: "TravelInsCos");

            migrationBuilder.DropTable(
                name: "TravelStatusComments");

            migrationBuilder.DropTable(
                name: "TravelSupplements");

            migrationBuilder.DropTable(
                name: "UploadCenters");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VehicleConstructionYearLimits");

            migrationBuilder.DropTable(
                name: "VehicleGroupUsages");

            migrationBuilder.DropTable(
                name: "WebsiteUpdates");

            migrationBuilder.DropTable(
                name: "WorkWiths");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "CarBodyCarGroups");

            migrationBuilder.DropTable(
                name: "CarBodyUsages");

            migrationBuilder.DropTable(
                name: "CarBodyInsuranceFinancialStatuses");

            migrationBuilder.DropTable(
                name: "CarBodyInsuranceStatuses");

            migrationBuilder.DropTable(
                name: "CollectionCommissionBases");

            migrationBuilder.DropTable(
                name: "BuildingUsageFireCoverages");

            migrationBuilder.DropTable(
                name: "FireInsuranceFinancialStatuses");

            migrationBuilder.DropTable(
                name: "FireInsuranceStatuses");

            migrationBuilder.DropTable(
                name: "LiabilityFinancialStatuses");

            migrationBuilder.DropTable(
                name: "LiabilityStatuses");

            migrationBuilder.DropTable(
                name: "LifeInsuranceFinancialStatuses");

            migrationBuilder.DropTable(
                name: "LifeInsuranceStatuses");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ThirdPartyFainancialStatuses");

            migrationBuilder.DropTable(
                name: "ThirdPartyStatuses");

            migrationBuilder.DropTable(
                name: "TravelInsClasses");

            migrationBuilder.DropTable(
                name: "TravelFinancialStatuses");

            migrationBuilder.DropTable(
                name: "TravelStatuses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "VehicleGroups");

            migrationBuilder.DropTable(
                name: "VehicleUsages");

            migrationBuilder.DropTable(
                name: "BlogGroups");

            migrationBuilder.DropTable(
                name: "CarBodyInsurances");

            migrationBuilder.DropTable(
                name: "BuildingUsages");

            migrationBuilder.DropTable(
                name: "FireInsCoverages");

            migrationBuilder.DropTable(
                name: "FireInsurances");

            migrationBuilder.DropTable(
                name: "LiabilityInsurances");

            migrationBuilder.DropTable(
                name: "LifeInsurances");

            migrationBuilder.DropTable(
                name: "ThirdParties");

            migrationBuilder.DropTable(
                name: "FinancialStatuses");

            migrationBuilder.DropTable(
                name: "InsStatuses");

            migrationBuilder.DropTable(
                name: "TravelInsurances");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "TravelZooms");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}

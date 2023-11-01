using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllInsAddChangeInfo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "TravelInsurances",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "ThirdParties",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "LifeInsurances",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "LiabilityInsurances",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "FireInsurances",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastChangeUser",
                table: "CarBodyInsurances",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2562));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2564));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2566));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Local).AddTicks(2625));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Local).AddTicks(2630));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Local).AddTicks(2632));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2609));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2584));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2587));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2589));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2414));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2418));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Local).AddTicks(1970));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(1936));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 10, 14, 51, 559, DateTimeKind.Utc).AddTicks(1949));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "TravelInsurances");

            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "ThirdParties");

            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "LifeInsurances");

            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "LiabilityInsurances");

            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "FireInsurances");

            migrationBuilder.DropColumn(
                name: "LastChangeUser",
                table: "CarBodyInsurances");

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4188));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4190));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4191));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Local).AddTicks(4266));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4243));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4211));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4214));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4103));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4107));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4108));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(4292));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Local).AddTicks(4015));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 9, 59, 12, 282, DateTimeKind.Utc).AddTicks(3991));
        }
    }
}

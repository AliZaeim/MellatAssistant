using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTravelZomm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6197));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Local).AddTicks(6271));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Local).AddTicks(6273));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6243));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6216));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6221));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6223));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6101));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6106));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6113));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(6319));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Local).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(5632));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 11, 46, 23, 854, DateTimeKind.Utc).AddTicks(5646));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9303));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9304));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9305));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Local).AddTicks(9368));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Local).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Local).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9351));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9326));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9193));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9198));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(9408));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Local).AddTicks(6532));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(6500));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 11, 44, 5, 800, DateTimeKind.Utc).AddTicks(6512));
        }
    }
}

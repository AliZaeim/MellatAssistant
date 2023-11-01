using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1746));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Local).AddTicks(1815));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1797));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1769));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1774));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1776));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1653));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1658));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1659));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1857));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Local).AddTicks(1084));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 31, 22, 50, 41, 770, DateTimeKind.Utc).AddTicks(1059));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Plans");

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2683));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2685));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Local).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Local).AddTicks(2776));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Local).AddTicks(2779));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2708));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2711));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2713));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2580));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2587));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2588));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 367, DateTimeKind.Utc).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 366, DateTimeKind.Local).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 366, DateTimeKind.Utc).AddTicks(9646));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 14, 0, 24, 366, DateTimeKind.Utc).AddTicks(9657));
        }
    }
}

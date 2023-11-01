using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Imperfect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Imperfect",
                table: "InsStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(855));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(857));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(858));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Local).AddTicks(921));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Local).AddTicks(924));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Local).AddTicks(925));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(907));

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 6,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 7,
                column: "Imperfect",
                value: false);

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(883));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(886));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(888));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(778));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(783));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Local).AddTicks(391));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 3, 14, 22, 4, 38, 890, DateTimeKind.Utc).AddTicks(372));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imperfect",
                table: "InsStatuses");

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
    }
}

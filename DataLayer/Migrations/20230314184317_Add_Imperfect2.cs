using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Imperfect2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6419));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Local).AddTicks(6492));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Local).AddTicks(6495));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Local).AddTicks(6497));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6475));

            migrationBuilder.InsertData(
                table: "InsStatuses",
                columns: new[] { "Id", "Comment", "GetInsNo", "GetPeyment", "Imperfect", "IsDefault", "IsDeleted", "IsEndofProcess", "IsSystemic", "Text" },
                values: new object[] { 8, null, false, false, true, false, false, false, true, "وضعیت ناقص" });

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6442));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6329));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6330));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(6517));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Local).AddTicks(5920));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(5882));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 3, 14, 22, 13, 16, 293, DateTimeKind.Utc).AddTicks(5893));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InsStatuses",
                keyColumn: "Id",
                keyValue: 8);

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
    }
}

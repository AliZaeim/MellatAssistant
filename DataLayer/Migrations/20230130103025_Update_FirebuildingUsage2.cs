using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFirebuildingUsage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 11, 1 },
                    { 12, 12, 1 },
                    { 13, 13, 1 },
                    { 14, 14, 1 },
                    { 15, 15, 1 },
                    { 16, 16, 1 },
                    { 17, 17, 1 },
                    { 18, 18, 1 },
                    { 19, 19, 1 },
                    { 20, 20, 1 },
                    { 21, 21, 1 },
                    { 22, 22, 1 },
                    { 23, 23, 1 },
                    { 24, 24, 1 },
                    { 25, 25, 1 },
                    { 26, 26, 1 },
                    { 27, 27, 1 },
                    { 28, 28, 1 },
                    { 29, 29, 1 },
                    { 30, 30, 1 },
                    { 31, 31, 1 },
                    { 32, 32, 1 },
                    { 33, 33, 1 },
                    { 34, 34, 1 },
                    { 35, 35, 1 },
                    { 36, 36, 1 },
                    { 37, 37, 1 },
                    { 38, 38, 1 },
                    { 39, 39, 1 },
                    { 40, 40, 1 },
                    { 41, 41, 1 },
                    { 42, 42, 1 },
                    { 43, 43, 1 },
                    { 44, 44, 1 },
                    { 45, 45, 1 },
                    { 46, 46, 1 },
                    { 47, 47, 1 },
                    { 48, 48, 1 },
                    { 49, 49, 1 },
                    { 50, 50, 1 },
                    { 51, 51, 1 },
                    { 52, 52, 1 },
                    { 53, 53, 1 },
                    { 54, 54, 1 },
                    { 55, 55, 1 },
                    { 56, 56, 1 },
                    { 57, 57, 1 },
                    { 58, 58, 1 },
                    { 59, 59, 1 },
                    { 60, 60, 1 },
                    { 61, 61, 1 },
                    { 62, 62, 1 },
                    { 63, 63, 1 },
                    { 64, 64, 1 },
                    { 65, 65, 1 },
                    { 66, 66, 1 },
                    { 67, 67, 1 },
                    { 68, 68, 1 },
                    { 69, 69, 1 },
                    { 70, 70, 1 },
                    { 71, 71, 1 },
                    { 72, 72, 1 },
                    { 73, 73, 1 },
                    { 74, 74, 1 },
                    { 75, 75, 1 },
                    { 76, 76, 1 },
                    { 77, 77, 1 },
                    { 78, 78, 1 },
                    { 79, 79, 1 },
                    { 80, 80, 1 },
                    { 81, 81, 1 },
                    { 82, 82, 1 },
                    { 83, 83, 1 },
                    { 84, 84, 1 },
                    { 85, 85, 1 },
                    { 86, 86, 1 },
                    { 87, 87, 1 },
                    { 88, 88, 1 },
                    { 89, 89, 1 },
                    { 90, 90, 1 },
                    { 91, 91, 1 },
                    { 92, 92, 1 },
                    { 93, 93, 1 },
                    { 94, 94, 1 },
                    { 95, 95, 1 },
                    { 96, 96, 1 },
                    { 97, 97, 1 },
                    { 98, 98, 1 },
                    { 99, 99, 1 },
                    { 100, 100, 1 },
                    { 101, 101, 1 },
                    { 102, 102, 1 },
                    { 103, 103, 1 },
                    { 104, 104, 1 },
                    { 105, 105, 1 },
                    { 106, 106, 1 },
                    { 107, 107, 1 },
                    { 108, 108, 1 },
                    { 109, 109, 1 },
                    { 110, 110, 1 },
                    { 111, 111, 1 },
                    { 112, 112, 1 },
                    { 113, 113, 1 },
                    { 114, 114, 1 },
                    { 115, 115, 1 },
                    { 116, 116, 1 },
                    { 117, 117, 1 },
                    { 118, 118, 1 },
                    { 119, 119, 1 },
                    { 120, 120, 1 },
                    { 121, 121, 1 },
                    { 122, 122, 1 },
                    { 123, 123, 1 },
                    { 124, 124, 1 },
                    { 125, 125, 1 },
                    { 126, 126, 1 },
                    { 127, 127, 1 },
                    { 128, 128, 1 },
                    { 129, 129, 1 },
                    { 130, 130, 1 },
                    { 131, 131, 1 },
                    { 132, 132, 1 },
                    { 133, 133, 1 },
                    { 134, 134, 1 },
                    { 135, 135, 1 },
                    { 136, 136, 1 },
                    { 137, 137, 1 },
                    { 138, 138, 1 },
                    { 139, 139, 1 },
                    { 140, 140, 1 },
                    { 141, 141, 1 },
                    { 142, 142, 1 },
                    { 143, 143, 1 },
                    { 144, 144, 1 },
                    { 145, 145, 1 },
                    { 146, 146, 1 },
                    { 147, 147, 1 },
                    { 148, 148, 1 },
                    { 149, 149, 1 },
                    { 150, 150, 1 },
                    { 151, 151, 1 },
                    { 152, 152, 1 },
                    { 153, 153, 1 },
                    { 154, 154, 1 },
                    { 155, 155, 1 },
                    { 156, 156, 1 },
                    { 157, 157, 1 },
                    { 158, 158, 1 },
                    { 159, 159, 1 },
                    { 160, 160, 1 },
                    { 161, 161, 1 },
                    { 162, 162, 1 },
                    { 163, 163, 1 },
                    { 164, 164, 1 },
                    { 165, 165, 1 },
                    { 166, 166, 1 },
                    { 167, 167, 1 },
                    { 168, 168, 1 },
                    { 169, 169, 1 },
                    { 170, 170, 1 },
                    { 171, 171, 1 },
                    { 172, 172, 1 },
                    { 173, 173, 1 },
                    { 174, 174, 1 },
                    { 175, 175, 1 },
                    { 176, 176, 1 },
                    { 177, 177, 1 },
                    { 178, 178, 1 },
                    { 179, 179, 1 },
                    { 180, 180, 1 },
                    { 181, 181, 1 },
                    { 182, 182, 1 },
                    { 183, 183, 1 },
                    { 184, 184, 1 },
                    { 185, 185, 1 },
                    { 186, 186, 1 },
                    { 187, 187, 1 },
                    { 188, 188, 1 },
                    { 189, 189, 1 },
                    { 190, 190, 1 },
                    { 191, 191, 1 },
                    { 192, 192, 1 },
                    { 193, 193, 1 },
                    { 194, 194, 1 },
                    { 195, 195, 1 },
                    { 196, 196, 1 },
                    { 197, 197, 1 },
                    { 198, 198, 1 },
                    { 199, 199, 1 },
                    { 200, 200, 1 },
                    { 201, 201, 1 },
                    { 202, 202, 1 },
                    { 203, 203, 1 },
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
                    { 220, 220, 1 },
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
                    { 237, 237, 1 },
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
                    { 254, 254, 1 },
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
                    { 271, 271, 1 },
                    { 272, 272, 1 },
                    { 273, 273, 1 },
                    { 274, 274, 1 },
                    { 275, 275, 1 },
                    { 276, 276, 1 },
                    { 277, 277, 1 },
                    { 278, 278, 1 },
                    { 279, 279, 1 },
                    { 280, 280, 1 },
                    { 281, 281, 1 },
                    { 282, 282, 1 },
                    { 283, 283, 1 },
                    { 284, 284, 1 },
                    { 285, 285, 1 },
                    { 286, 286, 1 },
                    { 287, 287, 1 },
                    { 288, 288, 1 },
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
                    { 304, 304, 1 },
                    { 305, 305, 1 },
                    { 306, 306, 1 }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "RP_Id",
                keyValue: 306);

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(493));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(494));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(495));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Local).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Local).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(515));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(518));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(519));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(398));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(404));

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 524, DateTimeKind.Utc).AddTicks(595));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 523, DateTimeKind.Local).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 523, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 30, 13, 55, 40, 523, DateTimeKind.Utc).AddTicks(9932));
        }
    }
}

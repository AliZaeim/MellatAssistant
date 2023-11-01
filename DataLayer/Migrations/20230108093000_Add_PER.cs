using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPER : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5777));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5778));

            migrationBuilder.UpdateData(
                table: "FinancialDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5780));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Local).AddTicks(5850));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Local).AddTicks(5862));

            migrationBuilder.UpdateData(
                table: "FinancialPremiums",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Local).AddTicks(5864));

            migrationBuilder.UpdateData(
                table: "IncidentCovers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5832));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5804));

            migrationBuilder.UpdateData(
                table: "LegalDiscounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5806));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5700));

            migrationBuilder.UpdateData(
                table: "LoosDriverDamages",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5701));

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

            migrationBuilder.UpdateData(
                table: "ThirdPartyBaseDatas",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "URId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Local).AddTicks(3138));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2023, 1, 8, 12, 59, 58, 609, DateTimeKind.Utc).AddTicks(3117));

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

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "PermissionId",
                keyValue: 1);

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
    }
}

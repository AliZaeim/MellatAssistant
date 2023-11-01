using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Prodocers
{
    /// <summary>
    /// این متد یک ورودی گرفته و نتیجه پیغام را بر می گرداند
    /// </summary>
    /// <param name="resultId"></param>
    /// <returns></returns>
    public class NextPayResult
    {
        public static string NPMessage(int resultId)
        {
            string result = string.Empty;
            switch (resultId)
            {
                case 0:
                    {
                        result = "پرداخت تکمیل و با موفقیت انجام شده است";
                        break;
                    }
                case -1:
                    {
                        result = "منتظر ارسال تراکنش و ادامه پرداخت";
                        break;
                    }
                case -2:
                    {
                        result = "پرداخت رد شده توسط کاربر یا بانک";
                        break;
                    }
                case -3:
                    {
                        result = "پرداخت در حال انتظار جواب بانک";
                        break;
                    }
                case -4:
                    {
                        result = "پرداخت لغو شده است";
                        break;
                    }
                case -20:
                    {
                        result = "کد api_key ارسال نشده است";
                        break;
                    }
                case -21:
                    {
                        result = "کد trans_id ارسال نشده است";
                        break;
                    }
                case -22:
                    {
                        result = "مبلغ ارسال نشده";
                        break;
                    }
                case -23:
                    {
                        result = "لینک ارسال نشده";
                        break;
                    }
                case -24:
                    {
                        result = "مبلغ صحیح نیست";
                        break;
                    }
                case -25:
                    {
                        result = "تراکنش قبلا انجام و قابل ارسال نیست";
                        break;
                    }
                case -26:
                    {
                        result = "مقدار توکن ارسال نشده است";
                        break;
                    }
                case -27:
                    {
                        result = "شماره سفارش صحیح نیست";
                        break;
                    }
                case -28:
                    {
                        result = "مقدار فیلد سفارشی [custom_json_fields] از نوع json نیست";
                        break;
                    }
                case -29:
                    {
                        result = "کد بازگشت مبلغ صحیح نیست";
                        break;
                    }
                case -30:
                    {
                        result = "مبلغ کمتر از حداقل پرداختی است";
                        break;
                    }
                case -31:
                    {
                        result = "صندوق کاربری موجود نیست";
                        break;
                    }
                case -32:
                    {
                        result = "مسیر بازگشت صحیح نیست";
                        break;
                    }
                case -33:
                    {
                        result = "کلید مجوز دهی صحیح نیست";
                        break;
                    }
                case -34:
                    {
                        result = "کد تراکنش صحیح نیست";
                        break;
                    }
                case -35:
                    {
                        result = "ساختار کلید مجوز دهی صحیح نیست";
                        break;
                    }
                case -36:
                    {
                        result = "شماره سفارش ارسال نشد است";
                        break;
                    }
                case -37:
                    {
                        result = "شماره تراکنش یافت نشد";
                        break;
                    }
                case -38:
                    {
                        result = "توکن ارسالی موجود نیست";
                        break;
                    }
                case -39:
                    {
                        result = "کلید مجوز دهی موجود نیست";
                        break;
                    }
                case -40:
                    {
                        result = "کلید مجوزدهی مسدود شده است";
                        break;
                    }
                case -41:
                    {
                        result = "خطا در دریافت پارامتر، شماره شناسایی صحت اعتبار که از بانک ارسال شده موجود نیست";
                        break;
                    }
                case -42:
                    {
                        result = "سیستم پرداخت دچار مشکل شده است";
                        break;
                    }
                case -43:
                    {
                        result = "درگاه پرداختی برای انجام درخواست یافت نشد";
                        break;
                    }
                case -44:
                    {
                        result = "پاسخ دریاف شده از بانک نامعتبر است";
                        break;
                    }
                case -45:
                    {
                        result = "سیستم پرداخت غیر فعال است";
                        break;
                    }
                case -46:
                    {
                        result = "درخواست نامعتبر";
                        break;
                    }
                case -47:
                    {
                        result = "کلید مجوز دهی یافت نشد [حذف شده]";
                        break;
                    }
                case -48:
                    {
                        result = "نرخ کمیسیون تعیین نشده است";
                        break;
                    }
                case -49:
                    {
                        result = "تراکنش مورد نظر تکراریست";
                        break;
                    }
                case -50:
                    {
                        result = "حساب کاربری برای صندوق مالی یافت نشد";
                        break;
                    }
                case -51:
                    {
                        result = "شناسه کاربری یافت نشد";
                        break;
                    }
                case -52:
                    {
                        result = "حساب کاربری تایید نشده است";
                        break;
                    }
                case -60:
                    {
                        result = "ایمیل صحیح نیست";
                        break;
                    }
                case -61:
                    {
                        result = "کد ملی صحیح نیست";
                        break;
                    }
                case -62:
                    {
                        result = "کد پستی صحیح نیست";
                        break;
                    }
                case -63:
                    {
                        result = "آدرس پستی صحیح نیست و یا بیش از ۱۵۰ کارکتر است";
                        break;
                    }
                case -64:
                    {
                        result = "توضیحات صحیح نیست و یا بیش از ۱۵۰ کارکتر است";
                        break;
                    }
                case -65:
                    {
                        result = "نام و نام خانوادگی صحیح نیست و یا بیش از ۳۵ کاکتر است";
                        break;
                    }
                case -66:
                    {
                        result = "تلفن صحیح نیست";
                        break;
                    }
                case -67:
                    {
                        result = "نام کاربری صحیح نیست یا بیش از ۳۰ کارکتر است";
                        break;
                    }
                case -68:
                    {
                        result = "نام محصول صحیح نیست و یا بیش از ۳۰ کارکتر است";
                        break;
                    }
                case -69:
                    {
                        result = "آدرس ارسالی برای بازگشت موفق صحیح نیست و یا بیش از ۱۰۰ کارکتر است";
                        break;
                    }
                case -70:
                    {
                        result = "آدرس ارسالی برای بازگشت ناموفق صحیح نیست و یا بیش از ۱۰۰ کارکتر است";
                        break;
                    }
                case -71:
                    {
                        result = "موبایل صحیح نیست";
                        break;
                    }
                case -72:
                    {
                        result = "بانک پاسخگو نبوده است لطفا با نکست پی تماس بگیرید";
                        break;
                    }
                case -73:
                    {
                        result = "مسیر بازگشت دارای خطا میباشد یا بسیار طولانیست";
                        break;
                    }
                case -90:
                    {
                        result = "بازگشت مبلغ بدرستی انجام شد";
                        break;
                    }
                case -91:
                    {
                        result = "عملیات ناموفق در بازگشت مبلغ";
                        break;
                    }
                case -92:
                    {
                        result = "در عملیات بازگشت مبلغ خطا رخ داده است";
                        break;
                    }
                case -93:
                    {
                        result = "موجودی صندوق کاربری برای بازگشت مبلغ کافی نیست";
                        break;
                    }
                case -94:
                    {
                        result = "کلید بازگشت مبلغ یافت نشد";
                        break;
                    }
                
                
                
                default:
                    break;
            }
            return result;
        }
    }
}

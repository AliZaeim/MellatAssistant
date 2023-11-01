using Core.DTOs.Admin;
using Core.Prodocers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Utility
{
    public static class Applications
    {
        public static bool IsValidNC(this string NC)
        {
            if (!string.IsNullOrEmpty(NC))
            {
                char[] chArray = NC.ToCharArray();
                int[] numArray = new int[chArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    numArray[i] = (int)char.GetNumericValue(chArray[i]);
                }
                int num2 = numArray[9];
                string[] strArray = { "0000000000", "1111111111", "22222222222", "33333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
                if (strArray.Contains(NC))
                {
                    return false;
                }
                else
                {
                    int num3 = ((((((numArray[0] * 10) + (numArray[1] * 9)) + (numArray[2] * 8)) + (numArray[3] * 7)) + (numArray[4] * 6)) + (numArray[5] * 5)) + (numArray[6] * 4) + (numArray[7] * 3) + (numArray[8] * 2);
                    int num4 = num3 - (num3 / 11 * 11);
                    if ((((num4 == 0) && (num2 == num4)) || ((num4 == 1) && (num2 == 1))) || ((num4 > 1) && (num2 == Math.Abs((int)(num4 - 11)))))
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public static bool IsValidCellphone(this string Cellphone)
        {
            if (!string.IsNullOrEmpty(Cellphone))
            {
                // Regular expression used to validate a phone number.
                string motif = @"^09[0|1|2|3][0-9]{8}$";
                return !string.IsNullOrEmpty(Cellphone) && Regex.IsMatch(Cellphone, motif);
            }
            return false;

        }
        
        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
        public static void GetNewDImage(int Width, int Height, Stream streamImg, string saveFilePath)
        {
            Bitmap sourceImage = new(streamImg);

            using Bitmap objBitmap = new(Width, Height);

            objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
            using Graphics objGraphics = Graphics.FromImage(objBitmap);
            // Set the graphic format for better result cropping   
            objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

            // Save the file path, note we use png format to support png file   
            objBitmap.Save(saveFilePath);
        }
        public static string SaveImageWithNewDimension(this IFormFile image, int width, int height, string root, string name)
        {
            try
            {
                string ImageName = name + Path.GetExtension(image.FileName);
                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, ImageName);
                Stream zstream = image.OpenReadStream();
                Bitmap Simage = new Bitmap(zstream);
                using Bitmap objBitmap = new Bitmap(width, height);
                objBitmap.SetResolution(Simage.HorizontalResolution, Simage.VerticalResolution);
                using Graphics objGraphics = Graphics.FromImage(objBitmap);
                // Set the graphic format for better result cropping   
                objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                objGraphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

                objGraphics.DrawImage(Simage, 0, 0, width, height);

                // Save the file path, note we use png format to support png file   
                objBitmap.Save(ImagePath);
                return ImageName;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static async Task<FileValidation> UploadedImageValidation(this IFormFile uploadImage, int validWidth, int validHeght, int validLength, string[] validExtensions)
        {
            string Messages = string.Empty;
            decimal vlen = decimal.Divide(validLength, 100);
            decimal Vlenght = vlen * 1024 * 1024;
            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "فایلی انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }
            if (uploadImage.Length > Vlenght)
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }
                else
                {
                    Messages += Environment.NewLine + "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }

            }
            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);
                if (validLength != 0 && validWidth != 0)
                {
                    using (var img = Image.FromStream(memoryStream))
                    {
                        // TODO: ResizeImage(img, 100, 100);
                        if (img.Width != validWidth)
                        {
                            Valid = false;
                            if (string.IsNullOrEmpty(Messages))
                            {
                                Messages = "عرض تصویر باید " + validWidth + " پیکسل باشد !";
                            }
                            else
                            {
                                Messages += Environment.NewLine + "عرض تصویر باید " + validWidth + " پیکسل باشد !";
                            }

                        }
                        if (img.Height != validHeght)
                        {
                            Valid = false;
                            if (string.IsNullOrEmpty(Messages))
                            {
                                Messages = "ارتفاع تصویر باید " + validHeght + " پیکسل باشد !";
                            }
                            else
                            {
                                Messages += Environment.NewLine + "ارتفاع تصویر باید " + validHeght + " پیکسل باشد !";
                            }

                        }
                    }
                }

            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static async Task<FileValidation> UploadedImageValidation(this IFormFile uploadImage, int validLength, string[] validExtensions)
        {
            string Messages = string.Empty;
            decimal vlen = decimal.Divide(validLength, 100);
            decimal Vlenght = vlen * 1024 * 1024;
            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "فایلی انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }
            if (uploadImage.Length > Vlenght)
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }
                else
                {
                    Messages += Environment.NewLine + "حجم تصویر بزرگتر از " + validLength + " " + "کیلو بایت" + " می باشد !";
                }

            }
            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);


            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static async Task<FileValidation> UploadedImageValidation(this IFormFile uploadImage, string[] validExtensions)
        {
            string Messages = string.Empty;

            bool Valid = true;
            if (uploadImage == null)
            {
                Messages = "فایلی انتخاب نشده است !";
                Valid = false;
                return new FileValidation { IsValid = Valid, Message = Messages };
            }
            string Ex = Path.GetExtension(uploadImage.FileName);
            if (string.IsNullOrEmpty(Ex))
            {
                Valid = false;
                return new FileValidation { IsValid = false, Message = "پسوند فایل نامشخص است !" };
            }
            if (!validExtensions.Any(a => a == Ex))
            {
                Valid = false;
                if (string.IsNullOrEmpty(Messages))
                {
                    Messages = "پسوند فایل نامعتبر است !";
                }
                else
                {
                    Messages += Environment.NewLine + "پسوند فایل نامعتبر است !";
                }
            }

            using (var memoryStream = new MemoryStream())
            {
                await uploadImage.CopyToAsync(memoryStream);


            }

            return new FileValidation { IsValid = Valid, Message = Messages };


        }
        public static string SaveUploadedImage(this IFormFile uploadImage, string root, bool SetFileName)
        {
            try
            {
                string ImageName = uploadImage.FileName;
                if (SetFileName)
                {
                    ImageName = uploadImage.FileName;
                }
                else
                {
                    string gncode = Generators.GenerateUniqueCode();
                    string ex = Path.GetExtension(uploadImage.FileName);
                    ImageName = gncode + ex;
                }
                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, ImageName);
                using (FileStream stream = new(ImagePath, FileMode.Create))
                {
                    uploadImage.CopyTo(stream);
                }
                return ImageName;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return null;
            }
        }
        public static string SaveUploadedFile(this IFormFile uploadFile, string FName, string root, bool SetFileName, bool SpotSuggestionFileName)
        {
            try
            {
                string FileName = uploadFile.FileName;
                string ex = Path.GetExtension(uploadFile.FileName);
                if (SetFileName)
                {
                    FileName = uploadFile.FileName;
                }
                else
                {
                    if (SpotSuggestionFileName)
                    {
                        FileName = FName + ex;
                    }
                    else
                    {
                        string gncode = Generators.GenerateUniqueCode();
                        FileName = gncode + ex;
                    }

                }

                string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, FileName);
                using (FileStream stream = new(ImagePath, FileMode.Create))
                {
                    uploadFile.CopyTo(stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return null;
            }
        }
        public static bool IsImage(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                string[] extensions = { ".png", ".jpg", "jpeg", ".gif", ".png", ".bmp", ".tif", ".tiff", ".ico", ".webp", ".avif" };
                string source_ex = Path.GetExtension(source);
                return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
            }
            return false;

        }
        public static bool IsVideo(this string source)
        {
            string[] extensions = { ".mp4", ".webm", ".mpeg", ".mkv", ".flv", ".mov", ".wmv", ".avi", ".mkv" };
            string source_ex = Path.GetExtension(source);
            return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
        }
        public static bool IsPdf(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                string[] extensions = { ".pdf" };
                string source_ex = Path.GetExtension(source);
                return extensions.Any(a => a == source_ex.ToLower(new CultureInfo("en-us", false)));
            }
            return false;
        }
        public static string GetLetterOfText(this string Text, int count)
        {
            int txtL = Text.Length;
            if (count > txtL)
            {
                return Text;
            }
            return Text.Substring(0, count);
        }
        public static string GetYearType(this int Year)
        {
            if (Year >= 1900)
            {
                return "miladi";
            }
            return "shamsi";
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static Guid ToGuid(long value)
        {
            byte[] guidData = new byte[16];
            Array.Copy(BitConverter.GetBytes(value), guidData, 8);
            return new Guid(guidData);
        }
        public static long ToLong(Guid guid)
        {
            if (BitConverter.ToInt64(guid.ToByteArray(), 8) != 0)
                throw new OverflowException("Value was either too large or too small for an Int64.");
            return BitConverter.ToInt64(guid.ToByteArray(), 0);
        }

        private static readonly HashSet<char> DefaultNonWordCharacters
          = new HashSet<char> { ',', '.', ':', ';' };

        /// <summary>
        /// Returns a substring from the start of <paramref name="value"/> no 
        /// longer than <paramref name="length"/>.
        /// Returning only whole words is favored over returning a string that 
        /// is exactly <paramref name="length"/> long. 
        /// </summary>
        /// <param name="value">The original string from which the substring 
        /// will be returned.</param>
        /// <param name="length">The maximum length of the substring.</param>
        /// <param name="nonWordCharacters">Characters that, while not whitespace, 
        /// are not considered part of words and therefor can be removed from a 
        /// word in the end of the returned value. 
        /// Defaults to ",", ".", ":" and ";" if null.</param>
        /// <exception cref="System.ArgumentException">
        /// Thrown when <paramref name="length"/> is negative
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="value"/> is null
        /// </exception>
        public static string CropWholeWords(
          this string value,
          int length,
          HashSet<char> nonWordCharacters = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (length < 0)
            {
                throw new ArgumentException("Negative values not allowed.", "length");
            }

            if (nonWordCharacters == null)
            {
                nonWordCharacters = DefaultNonWordCharacters;
            }

            if (length >= value.Length)
            {
                return value;
            }
            int end = length;

            for (int i = end; i > 0; i--)
            {
                if (value[i].IsWhitespace())
                {
                    break;
                }

                if (nonWordCharacters.Contains(value[i])
                    && (value.Length == i + 1 || value[i + 1] == ' '))
                {
                    //Removing a character that isn't whitespace but not part 
                    //of the word either (ie ".") given that the character is 
                    //followed by whitespace or the end of the string makes it
                    //possible to include the word, so we do that.
                    break;
                }
                end--;
            }

            if (end == 0)
            {
                //If the first word is longer than the length we favor 
                //returning it as cropped over returning nothing at all.
                end = length;
            }

            return value.Substring(0, end);
        }

        private static bool IsWhitespace(this char character)
        {
            return character == ' ' || character == 'n' || character == 't';
        }

        public static class ConvertArabicNumberToEnglish
        {
            public static string toEnglishNumber(string input)
            {
                string EnglishNumbers = "";
                for (int i = 0; i < input.Length; i++)
                {
                    if (char.IsDigit(input[i]))
                    {
                        EnglishNumbers += char.GetNumericValue(input, i);
                    }
                    else
                    {
                        EnglishNumbers += input[i].ToString();
                    }
                }
                return EnglishNumbers;
            }
        }

        public static string ReadExcel(string root)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream(root, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == NPOI.SS.UserModel.CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) & !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString().Replace("ي", "ی"));
                            }
                            else
                            {
                                rowList.Add(string.Empty);
                            }
                        }
                        else
                        {
                            rowList.Add(string.Empty);
                        }
                    }
                    if (rowList.Count > 0)
                        dtTable.Rows.Add(rowList.ToArray());
                    rowList.Clear();
                }
                xssWorkbook.Close();
                stream.Close();
            }
            return JsonConvert.SerializeObject(dtTable);

        }
        public static string ReadUploadedExcel(IFormFile file, string FileType)
        {
            Stream inputstream = file.OpenReadStream();
            XSSFWorkbook workbook = new(inputstream);
            DataTable dtTable = new();
            List<string> rowList = new();
            ISheet sheet = workbook.GetSheetAt(0);
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            Dictionary<string, string> keyValuePairs = SimulateFileHeader(FileType);
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString()))
                {
                    continue;
                }
                {
                    if (keyValuePairs != null)
                    {
                        string data = keyValuePairs[cell.ToString()];
                        if (!string.IsNullOrWhiteSpace(data))
                        {
                            dtTable.Columns.Add(data);
                        }
                        else
                        {
                            dtTable.Columns.Add(cell.ToString());
                        }

                    }
                    else
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }
            }
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                }

                if (row.Cells.All(d => d.CellType == CellType.Blank))
                {
                    continue;
                }

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        string v = row.GetCell(j).ToString();
                        if (!string.IsNullOrEmpty(row.GetCell(j).ToString()))
                        {
                            rowList.Add(row.GetCell(j).ToString().Replace("ي", "ی"));
                        }
                        else
                        {
                            rowList.Add(row.GetCell(j).ToString());
                        }
                    }
                }
                if (rowList.Count > 0)
                {
                    dtTable.Rows.Add(rowList.ToArray());
                }

                rowList.Clear();
            }
            return JsonConvert.SerializeObject(dtTable);
        }
        public static (bool confirm, string[] exp) ConfirmExcelFile(string type, IFormFile Exfile)
        {
            if (string.IsNullOrEmpty(type))
            {
                return (false, null);
            }
            var inputstream = Exfile.OpenReadStream();
            XSSFWorkbook workbook = new XSSFWorkbook(inputstream);
            List<string> headers = new List<string>();

            if (type.ToLower() == "bordro")
            {
                string[] bheaders = { "Type", "Status", "AgentNC", "AgentCode", "LifeCapital", "Deposit", "PremiumByPay", "SupPremium", "LifePremium", "PayMethod", "Duration", "Insured", "InsuredNC", "StartDate", "InitialStartDate", "Insurer", "InsurerNC", "IssueDate", "InsNO" };
                headers.AddRange(bheaders);
            }
            if (type.ToLower() == "commission")
            {
                string[] bheaders = { "DeductionDesc", "NetCommission", "TotalVat", "ManicipalTax", "Vat", "Deductions", "Tax", "SupCommission", "LifeCommission", "SupPremium", "LifePremium", "Percent", "PaidDate", "DueDate", "InsNO" };
                headers.AddRange(bheaders);
            }
            if (type.ToLower() == "addition")
            {
                string[] bheaders = { "Type", "Status", "AgentNC", "AgentCode", "LifeCapital", "Deposit", "PremiumByPay", "SupPremium", "LifePremium", "PayMethod", "Duration", "Insured", "InsuredNC", "StartDate", "InitialStartDate", "Insurer", "InsurerNC", "IssueDate", "InsNO" };
                headers.AddRange(bheaders);
            }
            if (type.ToLower() == "insuredinfo")
            {
                string[] bheaders = { "Phone", "Cellphone", "Address", "City", "State", "PaymentMethod", "Duration", "IssueDate", "InsuredFullName", "InsuredBirthDate", "AdditionType", "Status", "InsNO" };
                headers.AddRange(bheaders);
            }

            ISheet sheet = workbook.GetSheetAt(0);
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            List<string> FileHeaders = new List<string>();
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                string header = string.Empty;
                if (cell != null && !string.IsNullOrEmpty(cell.StringCellValue))
                {
                    header = cell.StringCellValue;
                    FileHeaders.Add(header);
                }



            }
            workbook.Close();
            inputstream.Close();
            List<string> comp = headers.Except(FileHeaders).ToList();
            string cm = comp.ToString();
            if (comp != null)
            {
                if (comp.Count != 0)
                {
                    return (false, comp.ToArray());
                }

            }
            comp = comp.Intersect(headers).ToList();
            bool hasMatch = headers.All(a => FileHeaders.Contains(a));
            return (hasMatch, headers.ToArray());
        }
        private static Dictionary<string, string> SimulateFileHeader(string type)
        {
            Dictionary<string, string> keyValuePairs = new();
            if (type == "colcom")
            {
                keyValuePairs.Add("ردیف", "Radif");
                keyValuePairs.Add("شماره بیمه نامه", "InsNO");
                keyValuePairs.Add("نام بیمه گذار", "InsurerName");
                keyValuePairs.Add("نام بیمه شده", "InsuredName");
                keyValuePairs.Add("کد بازاریاب", "MarketerCode");
                keyValuePairs.Add("نوع تعهد", "CommitmentType");
                keyValuePairs.Add("مبلغ تعهد", "CommitmentValue");
                keyValuePairs.Add("مبلغ کارمزد", "CommissionValue");
                keyValuePairs.Add("تاریخ تعهد", "CommitmentDate");
                keyValuePairs.Add("تاریخ انجام تعهد", "CommitmentDoDate");
            }
            return keyValuePairs;

        }
        public static string GetMounthShamsiName(this int ShamsiMounth)
        {
            string mName = string.Empty;
            switch (ShamsiMounth)
            {
                case 1:
                    {
                        mName = "فروردین";
                        break;
                    }
                case 2:
                    {
                        mName = "اردیبهشت";
                        break;
                    }
                case 3:
                    {
                        mName = "خرداد";
                        break;
                    }
                case 4:
                    {
                        mName = "تیر";
                        break;
                    }
                case 5:
                    {
                        mName = "مرداد";
                        break;
                    }
                case 6:
                    {
                        mName = "شهریور";
                        break;
                    }
                case 7:
                    {
                        mName = "مهر";
                        break;
                    }
                case 8:
                    {
                        mName = "آبان";
                        break;
                    }
                case 9:
                    {
                        mName = "آذر";
                        break;
                    }
                case 10:
                    {
                        mName = "دی";
                        break;
                    }
                case 11:
                    {
                        mName = "بهمن";
                        break;
                    }
                case 12:
                    {
                        mName = "اسفند";
                        break;
                    }
                default:
                    break;
            }
            return mName;
        }


    }
}

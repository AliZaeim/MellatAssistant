using Core.Utility;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.CarBodyIns
{
    public class CarBodyInsStep6VM
    {
        /// <summary>
        /// عکس اول از خوردگی
        /// </summary>
        [Display(Name = "عکس اول از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison1Image { get; set; }
        public string StrCorrison1Image { get; set; }
        /// <summary>
        /// عکس دوم از خوردگی
        /// </summary>
        [Display(Name = "عکس دوم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison2Image { get; set; }
        public string StrCorrison2Image { get; set; }
        /// <summary>
        /// عکس سوم از خوردگی
        /// </summary>
        [Display(Name = "عکس سوم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison3Image { get; set; }
        public string StrCorrison3Image { get; set; }
        /// <summary>
        /// عکس چهارم از خوردگی
        /// </summary>
        [Display(Name = "عکس چهارم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison4Image { get; set; }
        public string StrCorrison4Image { get; set; }
        /// <summary>
        /// عکس پنجم از خوردگی
        /// </summary>
        [Display(Name = "عکس پنجم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison5Image { get; set; }
        public string StrCorrison5Image { get; set; }
        /// <summary>
        /// عکس ششم از خوردگی
        /// </summary>
        [Display(Name = "عکس ششم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison6Image { get; set; }
        public string StrCorrison6Image { get; set; }
        /// <summary>
        /// عکس هفتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هفتم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison7Image { get; set; }
        public string StrCorrison7Image { get; set; }
        /// <summary>
        /// عکس هشتم از خوردگی
        /// </summary>
        [Display(Name = "عکس هشنم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison8Image { get; set; }
        public string StrCorrison8Image { get; set; }
        /// <summary>
        /// عکس نهم از خوردگی
        /// </summary>
        [Display(Name = "عکس نهم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison9Image { get; set; }
        public string StrCorrison9Image { get; set; }
        /// <summary>
        /// عکس دهم از خوردگی
        /// </summary>
        [Display(Name = "عکس دهم از خوردگی و لک بدنه")]
        
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile Corrison10Image { get; set; }
        public string StrCorrison10Image { get; set; }

        public string TrCode { get; set; }
        public int? Premium { get; set; }
    }
}

using Core.Utility;
using DataLayer.Entities.InsPolicy.Life;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class LifeInsuranceStep2
    {
        public string TrCode { get; set; }
        [Display(Name = "تصویر صفحه اول پرسشنامه")]
        [RequiredIf(nameof(StrQuestionnairePage1Image), ErrorMessage = "لطفا تصویر صفحه اول پرسشنامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile QuestionnairePage1Image { get; set; }
        public string StrQuestionnairePage1Image { get; set; }
        [Display(Name = "تصویر صفحه دوم پرسشنامه")]
        [RequiredIf(nameof(StrQuestionnairePage2Image), ErrorMessage = "لطفا تصویر صفحه دوم پرسشنامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile QuestionnairePage2Image { get; set; }
        public string StrQuestionnairePage2Image { get; set; }
        [Display(Name = "تصویر صفحه سوم پرسشنامه")]
        [RequiredIf(nameof(StrQuestionnairePage3Image), ErrorMessage = "لطفا تصویر صفحه سوم پرسشنامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile QuestionnairePage3Image { get; set; }
        public string StrQuestionnairePage3Image { get; set; }
        [Display(Name = "تصویر صفحه چهارم پرسشنامه")]
        [RequiredIf(nameof(StrQuestionnairePage4Image), ErrorMessage = "لطفا تصویر صفحه چهارم پرسشنامه را انتخاب کنید !", Dataval = true)]
        [Filesize(1, false, ErrorMessage = "حجم فایل انتخابی بیشتر از حد مجاز است !")]
        [AllowExtensions(false, "png,jpg,jpeg,gif,PNG,JPEG,JPG,GIF", ErrorMessage = "پسوند تصویر مجاز نمی باشد !")]
        public IFormFile QuestionnairePage4Image { get; set; }
        public string StrQuestionnairePage4Image { get; set; }

        [Display(Name = "طرح")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? PlanId { get; set; }

        [Display(Name = "روش پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? PeymentMethodId { get; set; }

        public List<Plan> Plans { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}

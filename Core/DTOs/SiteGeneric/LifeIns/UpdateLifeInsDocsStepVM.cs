using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteGeneric.LifeIns
{
    public class UpdateLifeInsDocsStepVM
    {
        public Guid guid { get; set; }
        [Display(Name = "تصویر صفحه اول پرسشنامه")]
        public IFormFile QuestionnairePage1Image { get; set; }
        public string StrQuestionnairePage1Image { get; set; }
        [Display(Name = "تصویر صفحه دوم پرسشنامه")]
        public IFormFile QuestionnairePage2Image { get; set; }
        public string StrQuestionnairePage2Image { get; set; }
        [Display(Name = "تصویر صفحه سوم پرسشنامه")]
        public IFormFile QuestionnairePage3Image { get; set; }
        public string StrQuestionnairePage3Image { get; set; }
        [Display(Name = "تصویر صفحه چهارم پرسشنامه")]
        public IFormFile QuestionnairePage4Image { get; set; }
        public string StrQuestionnairePage4Image { get; set; }
    }
}

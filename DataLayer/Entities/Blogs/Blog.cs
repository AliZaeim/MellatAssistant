
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataLayer.Entities.Blogs
{
    public class Blog
    {
        public Blog()
        {
            BlogComments = new HashSet<BlogComment>();
        }
        [Key]
        public Guid BlogId { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogTitle { get; set; }
        [Display(Name = "تاریخ خبر")]
        public DateTime? BlogDate { get; set; }
        [Display(Name = "تصویر  صفحه بلاگ")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogImageInBlog { get; set; }
        [Display(Name = "تصویر صفحه جزئیات")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string BlogImageInBlogDetails { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string BlogText { get; set; }
        [Display(Name = "عنوان سئو صفحه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string BlogPageTitle { get; set; }
        [Display(Name = "تگ description  صفحه خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(160, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string BlogPageDescription { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "خلاصه خبر")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogSummary { get; set; }
        [Display(Name = "تگ ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogTags { get; set; }
        [Display(Name = "دارای ویدئو")]
        public bool HasVideo { get; set; }
        [Display(Name = "دارای صدا")]
        public bool HasVoice { get; set; }
        [Display(Name = "فعال / غیرفعال")]
        public bool BlogIsActive { get; set; }
        [Display(Name = "گروه خبر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public int? BlogGroupId { get; set; }
        [Display(Name = "آدرس خبر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogUrl { get; set; }
        [Display(Name = "لینک بیرونی خبر")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} باشد!")]
        public string BlogRefferalLink { get; set; }
        [Display(Name = "متن لینک")]
        
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string BlogLinkText { get; set; }
        [Display(Name = "تعداد مشاهده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? BlogViewsCount { get; set; } = 0;
        [Display(Name = "نویسنده")]
        [StringLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]        
        public string Author { get; set; }
        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? AuthorId { get; set; }
        [NotMapped]
        public IList<string> TagsList
        {
            get { return (BlogTags ?? string.Empty).Split("-"); }
        }
        
        #region Relations
        [ForeignKey(nameof(BlogGroupId))]
        [Display(Name = "گروه خبر")]   
        public BlogGroup BlogGroup { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
        
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.General
{
    public class MemberProcutCommentVM
    {
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد!")]
        public string Comment { get; set; }
    }
}
